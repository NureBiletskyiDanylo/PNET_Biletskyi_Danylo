using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers
{
    public class ServerController(ILogRepository repository) : BaseApiController
    {
        [HttpGet("logs")]
        public async Task<ActionResult> GetLogs()
        {
            List<Log> logs = await repository.GetLogsAsync();
            return Ok(logs);
        }
    }
}
