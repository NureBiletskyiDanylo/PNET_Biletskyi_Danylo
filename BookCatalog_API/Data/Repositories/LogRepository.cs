using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog_API.Data.Repositories;

public class LogRepository(DataContext context) : ILogRepository
{
    public async Task<List<Log>> GetLogsAsync()
    {
        var logs = await context.Logs.ToListAsync();
        return logs == null ? new List<Log>() : logs;
    }
}
