# ACM Business Layer (C#/.NET) — Portfolio Project

A C# business-layer class library built as part of a Pluralsight-style ACM sample application. It focuses on **domain modeling**, **validation rules**, and the **Repository pattern**, backed by **MSTest** unit tests.

If you’re reviewing this for hiring, the goal is to demonstrate fundamentals: clean C# design, testable logic, and clear separation of concerns.

## What’s In This Repo

- **`ACM.BL`**: .NET Standard business-layer class library (`netstandard2.0`)
- **`Tests/ACM.BLTest`**: MSTest unit test project targeting .NET Framework 4.7.2

## Technical Highlights

### Skills Demonstrated

- **C# / OOP**: constructors, encapsulation, computed properties, nullable types
- **.NET**: .NET Standard 2.0 class library + .NET Framework 4.7.2 unit test project
- **Design patterns**: Repository pattern, repository composition
- **Testing**: MSTest unit tests validating both behavior and data mapping
- **Clean code practices**: separation of concerns, readable and deterministic tests

### Domain Modeling (POCOs)
The `ACM.BL` project contains plain C# domain entities designed for clarity and testability:

- **Customer**: identity, contact info, computed `FullName`, and validation
- **Address**: address fields + basic validation
- **Product**: name/description/pricing + validation
- **Order / OrderItem**: order date, line items, and validation

Design notes:

- **Computed property**: `Customer.FullName` composes display-friendly names while handling missing parts.
- **Encapsulation**: `Customer.CustomerID` / `Product.ProductID` are set via constructors (ID is not freely mutable).
- **Nullable value usage**: prices and dates use nullable types (`decimal?`, `DateTimeOffset?`) to represent “unknown/not set” states cleanly.

### Validation Logic
Each entity exposes a `Validate()` method to keep basic business rules close to the data:

- `Customer.Validate()` requires `LastName` and `EmailAddress`
- `Product.Validate()` requires `ProductName` and `CurrentPrice`
- `Order.Validate()` requires `OrderDate`
- `OrderItem.Validate()` checks positive quantity/product ID and requires `PurchasePrice`
- `Address.Validate()` requires `PostalCode`

These validations are intentionally lightweight and demonstrate how to structure business rules without UI dependencies.

### Repository Pattern (with Composition)
Repositories encapsulate retrieval logic and act as a seam for later data-access replacement:

- `CustomerRepository.Retrieve(int)` returns a populated customer (sample hard-coded data)
- `ProductRepository.Retrieve(int)` returns a populated product (sample hard-coded data)
- `OrderRepository.Retrieve(int)` returns an order with a stable date pattern
- `AddressRepository.RetrieveByCustomerID(int)` returns multiple addresses for a customer

A nice example of composition:

- `CustomerRepository` **depends on** `AddressRepository` to populate `Customer.AddressList`, demonstrating a repository collaborating with another data source.

### Unit Testing (MSTest)
The test suite validates both business logic and repository results:

- **Computed property tests**: `Customer.FullName` (normal case + missing first/last name)
- **Validation tests**: `Customer.Validate()` including negative cases
- **Repository tests**: `Retrieve(...)` tests for `CustomerRepository`, `ProductRepository`, and `OrderRepository`

The tests are deterministic and designed to demonstrate confidence in core behaviors.

## Getting Started

### Prerequisites
- Visual Studio 2022 (or VS 2019) with .NET desktop build tools
- OR the .NET SDK + Visual Studio Build Tools (for CLI builds)

### Build (Visual Studio)
1. Open `2020-Pluralsight-ACMProject.sln`
2. Build the solution

### Run Tests (Visual Studio)
- Use **Test Explorer** → **Run All**

### Build & Test (CLI)
From the repo root:

```powershell
# Build the business-layer library
dotnet build .\ACM.BL\ACM.BL.csproj -c Release

# Run unit tests
dotnet test .\Tests\ACM.BLTest\ACM.BLTest.csproj -c Release
```

> Note: the unit test project targets .NET Framework 4.7.2. In some setups, `dotnet test` may require the .NET Framework targeting pack / VS build tools installed.

### Run (CLI)
This repository is a **class library** + **unit tests** (there’s no console/web executable to “run”). The intended way to execute the code paths is to run the test suite.

```powershell
# Run all tests
dotnet test .\Tests\ACM.BLTest\ACM.BLTest.csproj

# Run a single test class (substring match)
dotnet test .\Tests\ACM.BLTest\ACM.BLTest.csproj --filter FullyQualifiedName~CustomerTest

# Run a single test method (example)
dotnet test .\Tests\ACM.BLTest\ACM.BLTest.csproj --filter FullyQualifiedName~CustomerTest.FullNameTestValid
```

## Project Structure

```text
ACM.BL/
  Customer.cs
  Address.cs
  Product.cs
  Order.cs
  OrderItem.cs
  CustomerRepository.cs
  AddressRepository.cs
  ProductRepository.cs
  OrderRepository.cs

Tests/ACM.BLTest/
  CustomerTest.cs
  CustomerRepositoryTest.cs
  ProductRepositoryTest.cs
  OrderRepositoryTest.cs
```

## Portfolio Notes (What This Demonstrates)

- Object-oriented design: constructors, encapsulation, and computed properties
- Business rule validation patterns
- Repository pattern and repository composition
- Unit testing fundamentals with MSTest
- Separation of concerns: business logic isolated from UI/data store specifics

## Potential Next Improvements
(If you decide to extend this repo later)

- Replace hard-coded repository data with a data access layer (EF Core/SQL/API)
- Expand validation with richer rules and error reporting (e.g., returning a list of validation errors)
- Add `Save(...)` test coverage and implement persistence