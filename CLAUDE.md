# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

NHibernate.AspNetCore.Identity is an ASP.NET Core Identity Provider implemented with NHibernate. It provides UserStore and RoleStore implementations that use NHibernate instead of Entity Framework Core. Supports PostgreSQL, SQL Server, MySQL, and SQLite.

Versioning: the package major/minor version matches the .NET version (e.g., 10.0.x for .NET 10.0).

## Solution Structure

- `src/NHibernate.AspNetCore.Identity/` — main library (NuGet package)
- `test/UnitTest/` — NUnit test project
- `test/WebTest/` — sample ASP.NET Core MVC app that demonstrates usage

The solution uses `.slnx` format (new VS 2022 format). Package versions are centrally managed in `Directory.Packages.props`.

## Key Architecture

### Main Library (`src/NHibernate.AspNetCore.Identity/`)

- **UserStore.cs** / **UserOnlyStore.cs** — implement `IUserStore<>` and related Identity interfaces using NHibernate sessions
- **RoleStore.cs** — implements `IRoleStore<>` using NHibernate sessions
- **Entities/** — Identity entity classes (IdentityUser, IdentityRole, IdentityUserRole, IdentityUserLogin, IdentityUserClaim, IdentityRoleClaim, IdentityUserToken)
- **Mappings/** — XML-based NHibernate mappings (`*.hbm.xml`) for the Identity entities
- **ConfigurationExtensions.cs** — `cfg.AddIdentityMappings()` extension that auto-detects dialect and adds the correct XML mappings
- **ConfigurationHelper.cs** — helper for dialect detection and mapping assembly selection
- **IdentityBuilderExtensions.cs** — `AddHibernateStores()` extensions for `IdentityBuilder`

### Mapping Support

The library supports all three NHibernate mapping styles:
1. **XML mapping** (`*.hbm.xml`) — built-in Identity mappings use this
2. **Mapping by Code** (`ModelMapper`) — shown in WebTest `AppRoleMapping`, `AppUserMapping`
3. **Mapping by Attributes** (`HbmSerializer`) — also demonstrated in tests

## Build and Test

```sh
# Build
dotnet build

# Run all tests
dotnet test

# Run a specific test
dotnet test --filter "FullyQualifiedName~IdentityTest._01_CanExtendByCodeWithByCode"

# Run tests in a specific test project
dotnet test test/UnitTest/UnitTest.csproj
```

Tests use SQLite via `test/UnitTest/hibernate.config` and require a running PostgreSQL database for WebTest-related tests.

## Running WebTest

WebTest is a sample MVC app with PostgreSQL. Requires:
1. Configure connection string in user secrets or `hibernate.config`
2. Run the appropriate SQL script from `database/postgres/` to create Identity tables
3. `dotnet run --project test/WebTest/WebTest.csproj`

## Packaging

Release metadata lives in `release.env` (sourced by both the local script and CI).
Publishing uses nuget.org Trusted Publishing via `.github/workflows/publish-nuget.yml`,
triggered by pushing a version tag — see `docs/publish-nuget.md`.

```sh
# 1. Update CHANGELOG.md and release.env (PACKAGE_VERSION, PACKAGE_RELEASE_NOTES)
# 2. Pack locally to verify (output in nupkgs/, not pushed)
./push-package.sh

# 3. Commit, tag and push; the tag triggers the publish workflow
git tag 10.0.5
git push origin master 10.0.5
```

## Database Scripts

`database/` contains SQL scripts for each supported database:
- `mssql/` — SQL Server
- `mysql/` — MySQL
- `postgres/` — PostgreSQL
- `sqlite/` — SQLite

## Key Dependencies

- `NHibernate` — ORM (version 5.6.1)
- `Microsoft.Extensions.Identity.Core` — ASP.NET Core Identity core
- `Microsoft.Extensions.Identity.Stores` — Identity store abstractions
- Package versions are centrally managed in `Directory.Packages.props`
