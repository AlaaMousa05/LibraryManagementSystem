using LibraryManagementSystem.Data;
using LibraryManagementSystem.Entities;
using LibraryManagementSystem.Enums;
using Microsoft.EntityFrameworkCore;

public class BorrowStatusBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BorrowStatusBackgroundService> _logger;

    public BorrowStatusBackgroundService(IServiceScopeFactory scopeFactory,
                                         ILogger<BorrowStatusBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

           
            var nextRun = DateTime.Today.AddHours(12);

            if (nextRun < now)
                nextRun = nextRun.AddDays(1);

            var delay = nextRun - now;
            await Task.Delay(delay, stoppingToken);

            await UpdateBorrowStatusesAsync();
        }
    }

    private async Task UpdateBorrowStatusesAsync()
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

            var borrows = await context.Borrows
                .Where(b => b.Status == BorrowStatus.Borrowed.ToString()
                         && b.DueAt < DateTime.Now)
                .ToListAsync();

            foreach (var borrow in borrows)
            {
                borrow.Status = BorrowStatus.Overdue.ToString();
            }

            if (borrows.Count > 0)
                await context.SaveChangesAsync();

            _logger.LogInformation($"Updated {borrows.Count} overdue borrow records.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running BorrowBackgroundService");
        }
    }
}
