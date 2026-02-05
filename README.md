# 🪶 Lithe

[![NuGet](https://img.shields.io/nuget/v/Lithe.svg)](https://www.nuget.org/packages/Lithe/)

Micro-ORM built on top of Dapper, currently focused on a `GetAsync<T>` extension for `IDbConnection`.

## Install (dotnet CLI)

```bash
dotnet add package Lithe
```

## Usage: `GetAsync<T>`

`GetAsync<T>` builds a `SELECT` statement and queries by primary key. It follows these conventions:

- Table name: `typeof(T).Name`
- Key column: `{TypeName}Id` (case-insensitive)
- Columns: all public readable instance properties (uses `[Column("Name")]` when present)

Example:

```csharp
using System.Data;
using Dapper;
using Lithe;

public sealed class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public static async Task<User?> LoadUserAsync(IDbConnection connection, int id)
{
    // Make sure the connection is open if your provider requires it.
    return await connection.GetAsync<User>(id);
}
```

If your key property does not follow the `{TypeName}Id` convention, `GetAsync<T>` will throw an `InvalidOperationException`.

You can specify a custom name for the column mapping

```csharp
public sealed class User
{
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("full_name")]
    public string Name { get; set; } = string.Empty;
}
```

## Usage: `GetManyAsync<T>`

`GetManyAsync<T>` builds the same `SELECT` statement as `GetAsync<T>`, but returns a list.

```csharp
public sealed class Service
{
    public Guid ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public static async Task<IEnumerable<Service>> LoadServicesAsync(IDbConnection connection, Guid serviceId)
{
    // Returns zero or more rows.
    return await connection.GetManyAsync<Service>(serviceId);
}
```

## Usage: `InsertAsync<T>`

`InsertAsync<T>` builds an `INSERT` with all public readable properties and executes it.

```csharp
public sealed class Service
{
    public Guid ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public static async Task<int> SaveServiceAsync(IDbConnection connection, Service service)
{
    // Returns the number of affected rows.
    return await connection.InsertAsync<Service>(service);
}
```
