# 2020ACMProject — ACM Business Layer (C#/.NET)

This repo is a small, intentionally focused **business-layer class library** with **unit tests**.

It’s not a full app (no UI, no database, no API). That’s on purpose: the goal is to demonstrate clean C# fundamentals—domain modeling, lightweight business rules, and a testable seam where persistence would go.

If you’re an employer or recruiter: this is a “read-the-code” project. The interesting parts are the design choices and tests, not the number of screens.

---

## Quick facts (for busy humans)

- **Language**: C#
- **Solution**: `2020ACMProject.sln`
- **Projects**:
  - `ACM.BL` → business-layer library (**.NET Standard 2.0**, `netstandard2.0`)
  - `Tests/ACM.BLTest` → unit tests (**MSTest**, **.NET Framework 4.7.2**, `net472`)
- **Patterns**: Repository pattern (with a small example of repository composition)
- **Testing focus**: computed behavior, validation rules, and repository retrieval

---

## Who this README is for

- **Employers / recruiters**: a quick map of what skills and design thinking this code demonstrates.
- **Developers**: exactly where to look, how to build/test, and why the code is structured this way.

---

## What this demonstrates (hiring-focused)

- **Object-oriented design**: POCO entities, constructors that establish invariants, and encapsulation where appropriate.
- **Business-rule placement**: basic validation lives close to the domain model (not in UI, not in repositories).
- **Testability**: behavior is deterministic and verified via unit tests.
- **Separation of concerns**: repositories act as a seam for persistence without polluting domain logic.
- **Pragmatism**: intentionally small scope, intentionally readable code.

---

## Architecture at 10,000 ft

This is a “core business layer” slice:

```text
┌────────────────────┐
│      Tests         │  MSTest (net472)
│  ACM.BLTest        │
└─────────┬──────────┘
          │ references
┌─────────▼──────────┐
│     ACM.BL         │  Business layer (netstandard2.0)
│  Entities + Repos  │
└────────────────────┘
```

The repositories currently return **hard-coded sample data**. That’s deliberate: it keeps the project focused on structure and behavior, and it keeps tests fast and stable.

---

## Design & development details

### Domain entities (POCOs with “just enough” behavior)

The `ACM.BL` project includes:

- `Customer` — identity, contact info, computed display name, validation, plus an address list
- `Address` — address fields + basic validation
- `Product` — product info + pricing + validation
- `Order` / `OrderItem` — order metadata, line items, and validation

Notable design choices:

- **Computed property**: `Customer.FullName` builds a display-friendly value and gracefully handles missing name parts.
- **Constructor invariants**:
  - `Customer(int customerId)` sets `CustomerID` and initializes `AddressList`.
  - `Order(int orderId)` initializes `OrderItems`.
- **Nullable types** (`decimal?`, `DateTimeOffset?`) model “not set yet” without magic values.

### Validation

Each entity has a `Validate()` method implementing straightforward business rules:

- `Customer.Validate()` → requires `LastName` and `EmailAddress`
- `Address.Validate()` → requires `PostalCode`
- `Product.Validate()` → requires `ProductName` and `CurrentPrice`
- `Order.Validate()` → requires `OrderDate`
- `OrderItem.Validate()` → requires `Quantity > 0`, `ProductID > 0`, and `PurchasePrice`

This is intentionally not a full validation framework (no error list, no localization, no async rules). It’s a clean baseline that’s easy to test and easy to extend.

### Repository pattern (and a small example of composition)

Repositories are the seam where a real data-access layer could be plugged in later.

- `CustomerRepository.Retrieve(int)` returns a populated `Customer` for a known ID
- `AddressRepository.RetrieveByCustomerID(int)` returns a stable list of addresses
- `ProductRepository.Retrieve(int)` returns a populated `Product` for a known ID
- `OrderRepository.Retrieve(int)` returns a populated `Order` for a known ID

Composition example:

- `CustomerRepository` uses `AddressRepository` to populate `Customer.AddressList`.

Also worth noting: `OrderRepository` seeds an order date using the **current year** (to keep sample data relevant across calendar years) while preserving determinism within a given year.

---

## Testing strategy

Tests live in `Tests/ACM.BLTest` and use **MSTest**.

Coverage focuses on:

- **Computed behavior**: `Customer.FullName` variants (first/last missing)
- **Business rules**: `Customer.Validate()` positive and negative cases
- **Repository retrieval**: verifying returned objects are correctly populated, including address lists

Tests follow an Arrange/Act/Assert style and are written to be readable by humans (including future humans).

---

## Getting started

### Prerequisites

- Visual Studio 2019/2022 with .NET desktop build tools
- Or the .NET SDK + build tools capable of compiling a .NET Framework 4.7.2 test project

### Build (Visual Studio)

1. Open `2020ACMProject.sln`
2. Build the solution

### Run tests (Visual Studio)

- Test Explorer → Run All

### Build & test (CLI)

From the repo root:

```powershell
dotnet build .\2020ACMProject.sln -c Release
dotnet test  .\Tests\ACM.BLTest\ACM.BLTest.csproj -c Release
```

If you prefer `vstest` directly:

```powershell
dotnet vstest .\Tests\ACM.BLTest\bin\Release\ACM.BLTest.dll --TestAdapterPath:.\packages\MSTest.TestAdapter.1.3.2\build\_common
```

---

## Code tour (where to look)

- `ACM.BL/Customer.cs` — computed `FullName`, validation, constructor invariants
- `ACM.BL/CustomerRepository.cs` — repository composition via `AddressRepository`
- `ACM.BL/AddressRepository.cs` — stable list retrieval for tests
- `Tests/ACM.BLTest/CustomerTest.cs` — computed property + validation tests
- `Tests/ACM.BLTest/CustomerRepositoryTest.cs` — retrieval + address list assertions

---

## Tradeoffs (a.k.a. “things I did on purpose”)

- **Hard-coded repository data**: keeps examples and unit tests stable and fast.
- **Lightweight validation**: avoids over-engineering; shows where richer validation could evolve.
- **No DI container**: the surface area is small enough to keep construction explicit.
- **Two target frameworks** (`netstandard2.0` + `net472`): demonstrates compatibility constraints you’ll see in real codebases.

---

## If I extended this next

- Add repository interfaces and introduce constructor injection
- Replace hard-coded data with persistence (EF Core / Dapper / API client)
- Upgrade validation to return a list of errors (instead of `bool`) and add richer rules
- Add real `Save(...)` behavior and test coverage
- Add CI (GitHub Actions/Azure DevOps) to run build + tests on every push

---

## Closing note

This project is intentionally modest in scope: it’s a clean room to demonstrate code quality, design fundamentals, and tests.

It’s the kind of code you want in the middle of a larger system—quietly correct, easy to read, and hard to break by accident.
