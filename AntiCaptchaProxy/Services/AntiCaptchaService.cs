using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Models;

namespace AntiCaptchaProxy.Services
{
    public class AntiCaptchaService : IAntiCaptchaService
    {
        private readonly string _serviceInfo;
        private int _createTaskCount = 0;
        private int _createTaskSucceeded = 0;
        private int _createTaskFailed = 0;
        private int _createTaskErrors = 0;
        private int _getTaskResultCount = 0;
        private int _getTaskResultSucceeded = 0;
        private int _getTaskResultFailed = 0;
        private int _getTaskResultErrors = 0;

        private readonly object _statsLock = new();

        public AntiCaptchaService()
        {
            _serviceInfo = $"Service created at {DateTime.Now}";
        }

        public string GetServiceInfo()
        {
            return _serviceInfo;
        }

        public ProxyStats GetProxyStats()
        {
            lock (_statsLock)
            {
                return new ProxyStats()
                {
                    CreateTaskCount = _createTaskCount,
                    CreateTaskSucceeded = _createTaskSucceeded,
                    CreateTaskFailed = _createTaskFailed,
                    CreateTaskErrors = _createTaskErrors,
                    GetTaskResultCount = _getTaskResultCount,
                    GetTaskResultSucceeded = _getTaskResultSucceeded,
                    GetTaskResultFailed = _getTaskResultFailed,
                    GetTaskResultErrors = _getTaskResultErrors
                };
            }
        }
   
        public void IncCreateTaskCount()
        {
            lock (_statsLock)
            {
                _createTaskCount++;
            }
        }

        public void IncCreateTaskSucceeded()
        {
            lock (_statsLock)
            {
                _createTaskSucceeded++;
            }
        }

        public void IncCreateTaskFailed()
        {
            lock ( _statsLock)
            { 
                _createTaskFailed++;
            }
        }

        public void IncCreateTaskErrors()
        {
            lock( _statsLock)
            {
                _createTaskErrors++;
            }
        }

        public void IncGetTaskResultCount()
        {
            lock (_statsLock)
            {
                _getTaskResultCount++;
            }
        }

        public void IncGetTaskResultSucceeded()
        {
            lock(_statsLock)
            {
                _getTaskResultSucceeded++;
            }
        }

        public void IncGetTaskResultFailed()
        {
            lock(_statsLock)
            {
                _getTaskResultFailed++;
            }
        }

        public void IncGetTaskResultErrors()
        {
            lock(_statsLock)
            {
                _getTaskResultErrors++;
            }
        }
    }
}
