using AntiCaptchaProxy.Models;

namespace AntiCaptchaProxy.Interfaces
{
    public interface IAntiCaptchaService
    {
        string GetServiceInfo();
        Task<ProxyStats> GetProxyStats(ProxyStatsDb db);
        Task IncCreateTaskCount(ProxyStatsDb db);
        Task IncCreateTaskSucceeded(ProxyStatsDb db);
        Task IncCreateTaskFailed(ProxyStatsDb db);
        Task IncCreateTaskErrors(ProxyStatsDb db);
        Task IncGetTaskResultCount(ProxyStatsDb db);
        Task IncGetTaskResultSucceeded(ProxyStatsDb db);
        Task IncGetTaskResultFailed(ProxyStatsDb db);
        Task IncGetTaskResultErrors(ProxyStatsDb db);
        Task UpdateLastBalance(ProxyStatsDb db, double balance);
    }
}
