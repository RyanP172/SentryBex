using Microsoft.EntityFrameworkCore;
using SentryBex.Database;
using SentryBex.Models.UsrSchemes;

namespace SentryBex.Services.Logger
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly AppDbContext _context;
        public LoggerRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<bool> RecordLog(string userUuid, string actionType, string logDetail, string actionStatus)
        {
            UsrActivityLog log = new UsrActivityLog
            {
                LogId = Guid.NewGuid().ToString(),
                UserUuid = userUuid,
                LoggedDate = DateTime.Now,
                LogActivityType = actionType,
                LogActivityStatus = actionStatus,
                LogDetail = logDetail
            };
            _context.UsrActivityLogs.Add(log);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
