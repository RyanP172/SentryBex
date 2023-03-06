namespace SentryBex.Services.Logger
{
    public interface ILoggerRepository
    {
        Task<bool> RecordLog(string userUuid, string actionType, string logDetail, string actionStatus);
    }
}
