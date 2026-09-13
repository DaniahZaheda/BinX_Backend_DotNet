using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TaskProjectManagement.Api.Tests;

public class QueryCountingInterceptor : DbCommandInterceptor
{
    private int _queryCount;

    public int QueryCount => _queryCount;

    public void Reset()
    {
        _queryCount = 0;
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        if (command.CommandText.TrimStart()
            .StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            Interlocked.Increment(ref _queryCount);
        }

        return base.ReaderExecutedAsync(
            command,
            eventData,
            result,
            cancellationToken);
    }
}