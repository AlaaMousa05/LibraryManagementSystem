# Library Management System (ASP.NET Core Web API + EF Core + SQL Server)

**Final Backend Training Project** developed as part of **ENG4YOU** program.  
This Web API application is built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**, following **OOP principles** and **clean architecture** using the **Controller-Service-Repository pattern** for maintainability and scalability.

## Project Overview

The system manages a library’s core operations — **users, books, categories, authors, and borrowing/returning processes** — all through RESTful APIs.  
It also implements **role-based access**, **JWT authentication**, **FluentValidation**, and a **global exception handler**.

## Database Schema (SQL Server)

| Table | Columns | Description |
|-------|---------|-------------|
| **Book** | Id (PK, INT), Title (NVARCHAR(256)), Isbn (NVARCHAR(20), Unique), CategoryId (FK), PublicationYear (INT), Description (NVARCHAR(MAX), nullable), CoverImageUrl (NVARCHAR(512), nullable) | Stores book details |
| **Category** | Id (PK, INT), Name (NVARCHAR(100), Unique) | Book categories |
| **Author** | Id (PK, INT), Name (NVARCHAR(200)) | Authors of books |
| **BookAuthors** | BookId (FK), AuthorId (FK) | Many-to-many relationship between books and authors |
| **Borrow** | Id (PK, INT), UserId (FK), BookId (FK), BorrowedAt (DATETIME2), DueAt (DATETIME2), ReturnedAt (DATETIME2, nullable), Status (NVARCHAR(20) — Borrowed/Returned/Overdue) | Tracks borrowing history |

## Entity Relationships

- **Book & Author:** Many-to-Many  
- **Book & Category:** Many-to-One  
- **User & Borrow:** One-to-Many  
- **Book & Borrow:** Many-to-One  

## API Endpoints

### User Management

| Method | Endpoint | Description | Roles |
|--------|---------|-------------|-------|
| POST | /api/users/register-admin | Register a new admin | ADMIN |
| POST | /api/users/register-member | Register a new member | Public |
| POST | /api/users/register-librarian | Register a new librarian | ADMIN |
| POST | /api/users/login | Authenticate a user | Public |
| GET | /api/users/profile | Get user profile | MEMBER, ADMIN, LIBRARIAN |
| PUT | /api/users/profile | Update user profile | MEMBER, ADMIN, LIBRARIAN |
| POST | /api/users/profile-image | Set user profile image | MEMBER, ADMIN, LIBRARIAN |

### Book Management

| Method | Endpoint | Description | Roles |
|--------|---------|-------------|-------|
| GET | /api/books | Get all books | MEMBER, ADMIN, LIBRARIAN |
| GET | /api/books/{id} | Get book by ID | MEMBER, ADMIN, LIBRARIAN |
| POST | /api/books | Create a new book | ADMIN, LIBRARIAN |
| PUT | /api/books/{id} | Update a book | ADMIN, LIBRARIAN |
| DELETE | /api/books/{id} | Delete a book | ADMIN, LIBRARIAN |
| POST | /api/books/{id}/images | Set book images | ADMIN, LIBRARIAN |

### Category Management

| Method | Endpoint | Description | Roles |
|--------|---------|-------------|-------|
| GET | /api/categories | Get all categories | MEMBER, ADMIN, LIBRARIAN |
| GET | /api/categories/{id} | Get category by ID | MEMBER, ADMIN, LIBRARIAN |
| POST | /api/categories | Create a new category | ADMIN, LIBRARIAN |
| PUT | /api/categories/{id} | Update a category | ADMIN, LIBRARIAN |
| DELETE | /api/categories/{id} | Delete a category | ADMIN, LIBRARIAN |

### Author Management

| Method | Endpoint | Description | Roles |
|--------|---------|-------------|-------|
| GET | /api/authors | Get all authors | MEMBER, ADMIN, LIBRARIAN |
| GET | /api/authors/{id} | Get author by ID | MEMBER, ADMIN, LIBRARIAN |
| POST | /api/authors | Create a new author | ADMIN, LIBRARIAN |
| PUT | /api/authors/{id} | Update an author | ADMIN, LIBRARIAN |
| DELETE | /api/authors/{id} | Delete an author | ADMIN, LIBRARIAN |

### Borrow Management

| Method | Endpoint | Description | Roles |
|--------|---------|-------------|-------|
| POST | /api/borrows | Borrow a book | MEMBER |
| GET | /api/borrows | Get all borrows | ADMIN, LIBRARIAN |
| GET | /api/borrows/{id} | Get borrow by ID | ADMIN, LIBRARIAN |
| GET | /api/borrows/user/{userId} | Get all borrows for a specific user | ADMIN, LIBRARIAN |
| PATCH | /api/borrows/{id} | Update borrow status | ADMIN, LIBRARIAN |
| DELETE | /api/borrows/{id} | Delete a borrow | ADMIN, LIBRARIAN |

## Features & Tools

- Fluent Validation for input checking  
- Controller-Service-Repository pattern for clean architecture  
- DTOs for Create, Update, and Return operations  
- JWT for authentication  
- EF Core for data handling and relational mapping  
- SQL Server as database  
- Global Exception Handler for robust API responses  

## Technologies Used

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- JWT Authentication  
- FluentValidation  
- **OOP (Object-Oriented Programming)**  

This project demonstrates professional **backend development skills**, clean API design, effective handling of relational data, and practical application of **OOP principles**, reflecting the hands-on experience gained **as part of ENG4YOU training**.

