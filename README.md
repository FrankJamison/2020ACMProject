# 2020ACMProject — ACM Business Layer (C#/.NET)

This repository is a small, intentionally focused **business-layer class library** with **unit tests**.

It’s not a full application (no UI, no database, no API). That’s not missing work—it’s scoped work.
This code is the kind of “quiet middle layer” you want in a real system: readable, testable, and hard to break by accident.

If you’re an employer or recruiter: the best use of your time is skimming the tests, then reading the `CustomerRepository`/`Customer` code.

---

## Quick facts (for busy humans)

- **Language**: C#
- **Solution**: `2020ACMProject.sln`
- **Projects**:
  - `ACM.BL` → business-layer library (**.NET Standard 2.0**, `netstandard2.0`)
  - `Tests/ACM.BLTest` → unit tests (**MSTest**, **.NET Framework 4.7.2**, `net472`)
- **Patterns**: Repository pattern (with a small example of repository composition)
- **Testing focus**: computed behavior, validation rules, and repository retrieval
- **Philosophy**: small surface area, high signal-to-noise

---

## Who this README is for

- **Employers / recruiters**: the “what skills does this prove?” view
- **Developers**: the “how does this work, how do I run it, where do I look?” view

---

## What this demonstrates (employer-friendly)

- **OOP fundamentals**: POCO entities, constructors, encapsulation, and computed properties
- **Business-rule placement**: basic validation lives in the domain model, not UI glue
- **Testability**: behavior is deterministic and covered by unit tests
- **Separation of concerns**: repositories provide a seam for persistence without contaminating the domain
- **Maintainer empathy**: code is written for humans, not for “clever points”

---

## Repository map

```text
2020ACMProject.sln
ACM.BL/                 # Business-layer library (netstandard2.0)
  Address.cs
  Customer.cs
  Order.cs
  OrderItem.cs
  Product.cs
  AddressRepository.cs
  CustomerRepository.cs
  OrderRepository.cs
  ProductRepository.cs

Tests/ACM.BLTest/       # MSTest unit tests (net472)
  CustomerTest.cs
  CustomerRepositoryTest.cs
  ProductRepositoryTest.cs
  OrderRepositoryTest.cs

.vscode/tasks.json      # VS Code build/test tasks
packages/               # MSTest adapter/framework packages (packages.config style)
```

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

The repositories currently return **hard-coded sample data**. That’s deliberate:

- Keeps the project about design and behavior (not about wiring)
- Keeps tests fast, deterministic, and “no setup required”
- Creates a clean seam where a real data access layer could be plugged in later

---

## Design & development elements (the detailed tour)

### 1) Domain entities (POCOs with “just enough” behavior)

The `ACM.BL` project contains the domain model:

- `Customer` — identity, contact info, computed display name, validation, plus an address list
- `Address` — address fields + basic validation
- `Product` — product info + price + validation
- `Order` / `OrderItem` — order metadata, line items, and validation

Design choices (and why they exist):

- **Computed property**: `Customer.FullName` builds a display-friendly value and handles missing name parts.
- **Constructor invariants**:
  - `Customer(int customerId)` sets `CustomerID` and initializes `AddressList`.
  - `Order(int orderId)` initializes `OrderItems`.
- **Nullable types** (`decimal?`, `DateTimeOffset?`) model “not set yet” without magic values.

### 2) Validation approach

Each entity has a `Validate()` method implementing straightforward rules:

- `Customer.Validate()` → requires `LastName` and `EmailAddress`
- `Address.Validate()` → requires `PostalCode`
- `Product.Validate()` → requires `ProductName` and `CurrentPrice`
- `Order.Validate()` → requires `OrderDate`
- `OrderItem.Validate()` → requires `Quantity > 0`, `ProductID > 0`, and `PurchasePrice`

What this is (and isn’t):

- ✅ Simple, UI-agnostic, unit-testable rules
- ❌ Not a full validation framework (no error lists, no localization, no async checks)

### 3) Repository pattern (with a small example of composition)

Repositories provide a seam where persistence could be swapped in later:

- `CustomerRepository.Retrieve(int)` returns a populated `Customer` for a known ID
- `AddressRepository.RetrieveByCustomerID(int)` returns a stable list of addresses
- `ProductRepository.Retrieve(int)` returns a populated `Product` for a known ID
- `OrderRepository.Retrieve(int)` returns a populated `Order` for a known ID

Composition example:

- `CustomerRepository` uses `AddressRepository` to populate `Customer.AddressList`.

One small “real world” touch:

- `OrderRepository` uses the **current year** when seeding sample data so it stays relevant as time passes, while still being deterministic for that year.

### 4) Testing strategy

Tests live in `Tests/ACM.BLTest` and use **MSTest**.

The suite intentionally covers three categories:

- **Computed behavior**: `Customer.FullName` variations
- **Business rules**: `Validate()` positive and negative cases
- **Repository retrieval**: returned objects are populated correctly (including address lists)

The tests follow Arrange/Act/Assert and prioritize readability.

### 5) Build + dependency model

This repo intentionally demonstrates a common enterprise mix:

- Library: `netstandard2.0`
- Tests: `net472`

The test project uses the older `packages.config` style and a checked-in `packages/` folder for:

- `MSTest.TestAdapter` (1.3.2)
- `MSTest.TestFramework` (1.3.2)

That’s not the newest approach, but it’s representative of legacy code you’ll maintain in the real world.

### 6) VS Code tooling

The repo includes VS Code tasks:

- Build: `.NET: Build (Release)`
- Tests: `.NET: VSTest (Release)`

These are defined in `.vscode/tasks.json` and are wired to the renamed solution file.

### 7) Source control hygiene

The `.gitignore` is intentionally small and focused:

- ignores build artifacts (`bin/`, `obj/`)
- ignores Visual Studio cache (`.vs/`)
- ignores test output (`TestResults/`)

This keeps commits reviewable and keeps the repo from becoming a storage unit.

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

## Code tour (start here)

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

## FAQ / troubleshooting

<details>
<summary><strong>Why doesn’t this “run” as an application?</strong></summary>

This repo is a class library + tests. The “executable” is the test suite—run the tests to exercise the code paths.

</details>

<details>
<summary><strong>My CLI test run fails because of .NET Framework targeting packs</strong></summary>

The test project targets .NET Framework 4.7.2 (`net472`). Some environments require Visual Studio Build Tools / targeting packs installed for `dotnet test`.
If you hit that, running tests from Visual Studio typically works out of the box.

</details>

---

## If I extended this next (roadmap)

- Introduce repository interfaces and constructor injection
- Replace hard-coded data with persistence (EF Core / Dapper / API client)
- Upgrade validation to return a list of errors (instead of `bool`) and add richer rules
- Implement real `Save(...)` behavior and add test coverage
- Add CI to run build + tests on every push

---

## Closing note

This project is intentionally modest in scope: it’s a clean room to demonstrate code quality, design fundamentals, and tests.

It’s the kind of code you want in the middle of a larger system—quietly correct, easy to read, and hard to break by accident.
