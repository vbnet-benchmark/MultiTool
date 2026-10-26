# MultiTool

A single ASP.NET WebForms platform hosting several independent business tools behind one shared
account system and data layer.

**Stack** — VB.NET · ASP.NET WebForms · Entity Framework · SQL Server · ASP.NET Web API


## Status

*Verified 28 Aug 2026 — builds under MSBuild with 0 errors; every page below served HTTP 200 under IIS Express.*

**Working**
- Account pages — register, login, password reset, email verification, settings
- Calculator suite
- Resume / portfolio generator (PDF output)
- Portfolio page

**Needs setup**
- The AdventureWorks dashboard renders, but stays empty until you restore Microsoft's
  **AdventureWorks2022** sample database — see below. The application's own tables come from
  `Database/schema.sql`.

**Not built**
- Email delivery is not wired up. Verification and reset tokens are generated and stored correctly,
  but nothing sends them — you'd read the token from the database to complete either flow.

Built to exercise WebForms, Entity Framework, and reporting against a real schema, not to ship.

## What's in it

- **Account system**, built from scratch — registration, login, email verification, password reset, and user settings
- **AdventureWorks dashboard** — parameterized reporting queries and aggregations, with an embedded RDLC report (Total Sales by Product Category)
- **Resume &amp; portfolio generator** — collects structured input and renders a formatted PDF server-side
- **Calculator suite** — business and financial calculations
- Shared `MultiToolContext` and master-page layouts so each tool inherits auth and styling

## Reporting database

The dashboard reads **AdventureWorks2022**, Microsoft's public sample database.
Download it from [Microsoft's sample database page](https://learn.microsoft.com/en-us/sql/samples/adventureworks-install-configure)
and restore it to LocalDB. The application's own tables come from `Database/schema.sql`.

## Running it locally

**Requires** Visual Studio 2022, .NET Framework 4.x, and SQL Server LocalDB (ships with Visual Studio).

1. Create the database and schema:
   ```
   sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE TinyToolsIdea"
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d TinyToolsIdea -I -i Database/schema.sql
   ```
2. Open the solution in Visual Studio and press F5.

The connection string in `Web.config` points at LocalDB using Integrated Security — there are no
credentials in this repository, and none are required.
