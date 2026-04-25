````md
# Account Management System API

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)
![C#](https://img.shields.io/badge/C%23-Programming-blue)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET-Core-green)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-ORM-success)
![SQLite](https://img.shields.io/badge/SQLite-Database-lightgrey)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-brightgreen)

## Description

This project is a Minimal API built with ASP.NET Core and Entity Framework Core.

It allows customers to manage their account data with the following features:

- Register a new account
- Get all accounts
- Get account by ID
- Get account by Email
- Update account information

The project uses SQLite database and Swagger/OpenAPI for testing.

---

## Technologies Used

- ASP.NET Core Minimal API
- C#
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

---

## Account Model

Each account contains:

- Id
- FirstName
- LastName
- Email

---

## API Endpoints

### Register New Account

```http
POST /accounts
````

### Get All Accounts

```http
GET /accounts
```

### Get Account By ID

```http
GET /accounts/{id}
```

### Get Account By Email

```http
GET /accounts/by-email/{email}
```

### Update Account

```http
PUT /accounts/{id}
```

---

## Example JSON (POST)

```json
{
  "firstName": "Jude",
  "lastName": "Cruz",
  "email": "Jude@email.com"
}
```

---

## Example JSON (PUT)

```json
{
  "id": 1,
  "firstName": "Jude",
  "lastName": "Cruz",
  "email": "Jude@email.com"
}
```

---

## How to Run

1. Open project in Visual Studio
2. Restore NuGet packages
3. Run migrations:

```powershell
Add-Migration InitialCreate
Update-Database
```

4. Run project:

```powershell
dotnet run
```

5. Open Swagger:

```text
https://localhost:xxxx/swagger
```

---

## Notes

* Data is stored in SQLite database (`accounts.db`)
* Swagger is used to test all endpoints
* Entity Framework Core handles database operations

---

Author

FRANS
