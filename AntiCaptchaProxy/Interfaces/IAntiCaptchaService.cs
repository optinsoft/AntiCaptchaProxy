using AntiCaptchaProxy.Models;

namespace AntiCaptchaProxy.Interfaces
{
    public interface IAntiCaptchaService
    {
        string GetServiceInfo();
        ProxyStats GetProxyStats();
        BalanceStats? GetLastBalance();
        void IncCreateTaskCount();
        void IncCreateTaskSucceeded();
        void IncCreateTaskFailed();
        void IncCreateTaskErrors(); 
        void IncGetTaskResultCount();
        void IncGetTaskResultSucceeded();
        void IncGetTaskResultFailed();
        void IncGetTaskResultErrors();
        void UpdateLastBalance(double balance);
    }
}
