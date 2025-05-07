using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface ILogRepository
{
    Task<List<Log>> GetLogsAsync();
}
