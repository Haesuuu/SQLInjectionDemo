# SQL Injection and XSS Demonstration

This is a local-only ASP.NET Core Razor Pages application for demonstrating:

- SQL injection against the intentionally unsafe `Account/Login` page.
- The safer parameterized query in `Account/ParameterizedLogin`.
- Stored XSS through intentionally unencoded member bios.

Do not deploy this application to a public server. It uses demonstration-only plain-text passwords and deliberately unsafe rendering.

## Requirements

- .NET 10 SDK
- SQL Server LocalDB, SQL Server Express, or another SQL Server instance
- Visual Studio 2022 or the .NET CLI (optional)

## Run after cloning

```powershell
git clone <your-repository-url>
cd SQLInjectionDemo
dotnet restore
dotnet run --project .\SQLInjectionDemo --launch-profile http
```

Open `http://localhost:5187`.

The application automatically applies migrations and creates two demo accounts on first run:

| Account | Username | Password |
| --- | --- | --- |
| Administrator | `admin` | `admin123` |
| Member | `member` | `member123` |

## SQL Server configuration

The default connection uses Windows LocalDB:

```text
Server=(localdb)\MSSQLLocalDB;Database=SQLInjectionDemo;Trusted_Connection=True;TrustServerCertificate=True;
```

If LocalDB is not installed, configure another SQL Server instance with user secrets:

```powershell
dotnet user-secrets init --project .\SQLInjectionDemo\SQLInjectionDemo.csproj
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=SQLInjectionDemo;Trusted_Connection=True;TrustServerCertificate=True;" --project .\SQLInjectionDemo\SQLInjectionDemo.csproj
```

Alternatively, set the connection string for the current shell without editing tracked files:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=YOUR_SERVER;Database=SQLInjectionDemo;Trusted_Connection=True;TrustServerCertificate=True;"
```

`SQLInjectionDemo/appsettings.example.json` is also included as a reference. Do not commit passwords or machine-specific connection strings.

## Demonstrations

Use the **Vulnerable login** link to demonstrate the raw SQL query. Use **Safe login** to compare it with the parameterized query.

After signing in, open a member profile. Members can edit their own bio. To demonstrate stored XSS locally, save a payload such as:

```html
<script>alert('XSS demo')</script>
```

Then view the profile again. The bio is intentionally rendered as HTML for this demonstration; it must be HTML-encoded in any real application.
