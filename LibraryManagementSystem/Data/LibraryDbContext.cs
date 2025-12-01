using LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagementSystem.Data
    {
        public class LibraryDbContext : IdentityDbContext<AppUser>
        {
            public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Borrow> Borrows { get; set; }
            public DbSet<BookAuthor> BookAuthors { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                
                builder.Entity<BookAuthor>()
                    .HasKey(ba => new { ba.BookId, ba.AuthorId });

                builder.Entity<BookAuthor>()
                    .HasOne(ba => ba.Book)
                    .WithMany(b => b.BookAuthors)
                    .HasForeignKey(ba => ba.BookId);

                builder.Entity<BookAuthor>()
                    .HasOne(ba => ba.Author)
                    .WithMany(a => a.BookAuthors)
                    .HasForeignKey(ba => ba.AuthorId);

                
                builder.Entity<Book>()
                    .HasIndex(b => b.Isbn)
                    .IsUnique();

                builder.Entity<Category>()
                    .HasIndex(c => c.Name)
                    .IsUnique();

                
                builder.Entity<Borrow>()
                    .Property(b => b.Status)
                    .HasMaxLength(20)
                    .IsRequired();

                
                builder.Entity<Book>()
                    .Property(b => b.Title)
                    .HasMaxLength(256)
                    .IsRequired();

                builder.Entity<Author>()
                    .Property(a => a.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                builder.Entity<Category>()
                    .Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            }
        }
    }


