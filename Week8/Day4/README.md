## Day 4 – Database Indexing & Performance Profiling

### Objective

Improve query performance by identifying suitable indexes and comparing performance before and after indexing.

### Indexing

The project query frequently filters by `OwnerId` and sorts by `CreatedAt`, so a composite covering index was added:

```csharp
builder.Entity<Project>()
    .HasIndex(p => new { p.OwnerId, p.CreatedAt })
    .IncludeProperties(p => new
    {
        p.Name,
        p.Description
    });
```

Migration:

```bash
dotnet ef migrations add ImproveProjectPerformanceIndex
dotnet ef database update
```

### Performance Profiling

Testing was performed with **10,056 projects** using SQL Server Actual Execution Plan and `STATISTICS IO/TIME`.

|                |                  Before |          After |
| -------------- | ----------------------: | -------------: |
| Logical Reads  |                      54 |          **9** |
| CPU Time       |                    0 ms |       **0 ms** |
| Elapsed Time   |                   51 ms |      **37 ms** |
| Execution Plan | Index Seek + Key Lookup | **Index Seek** |

The covering index eliminated the `Key Lookup` and reduced logical reads by approximately **83%**.

### Summary

Implemented and validated a composite covering index using EF Core Fluent API and SQL Server profiling, resulting in improved query efficiency.
