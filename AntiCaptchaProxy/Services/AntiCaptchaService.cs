using AntiCaptchaProxy.Interfaces;
using AntiCaptchaProxy.Models;
using Microsoft.EntityFrameworkCore;

namespace AntiCaptchaProxy.Services
{
    public class AntiCaptchaService : IAntiCaptchaService
    {
        private static readonly Guid GUID_ProxyStats = new("82afec2663784eab35240b75059f56b1");

        private bool _dbSynchronized = false;

        private readonly string _serviceInfo;
        private readonly ProxyStats _proxyStats = new() {
            Id = GUID_ProxyStats
        };

        private readonly object _statsLock = new();

        public AntiCaptchaService()
        {
            _serviceInfo = $"Service created at {DateTime.Now}";
        }

        public string GetServiceInfo()
        {
            return _serviceInfo;
        }

        private async Task<ProxyStats> GetDbProxyStats(ProxyStatsDb db)
        {
            var dbProxyStats = await db.AntiCaptchaStats.FindAsync(GUID_ProxyStats);
            if (dbProxyStats == null)
            {
                dbProxyStats = new();
                lock (_statsLock)
                {
                    dbProxyStats.Assign(_proxyStats);
                }
                await db.AntiCaptchaStats.AddAsync(dbProxyStats);
                await db.SaveChangesAsync();
            }
            else
            {
                if (!_dbSynchronized)
                {
                    lock (_statsLock)
                    {
                        if (!_dbSynchronized)
                        {
                            _proxyStats.Assign(dbProxyStats);
                            _dbSynchronized = true;
                        }
                    }
                }

            }
            return dbProxyStats;
        }

        private static async Task UpdateProxyStatsDb(ProxyStatsDb db, ProxyStats proxyStats)
        {
            db.Entry(proxyStats).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task<ProxyStats> GetProxyStats(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                proxyStats.Assign(_proxyStats);
            }
            return proxyStats;
        }

        public async Task IncCreateTaskCount(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.CreateTaskCount++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncCreateTaskSucceeded(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.CreateTaskSucceeded++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncCreateTaskFailed(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock ( _statsLock)
            { 
                _proxyStats.CreateTaskFailed++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncCreateTaskErrors(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock ( _statsLock)
            {
                _proxyStats.CreateTaskErrors++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncGetTaskResultCount(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.GetTaskResultCount++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncGetTaskResultSucceeded(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.GetTaskResultSucceeded++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncGetTaskResultFailed(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.GetTaskResultFailed++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task IncGetTaskResultErrors(ProxyStatsDb db)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.GetTaskResultErrors++;
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }

        public async Task UpdateLastBalance(ProxyStatsDb db, double balance)
        {
            var proxyStats = await GetDbProxyStats(db);
            lock (_statsLock)
            {
                _proxyStats.LastBalance = balance;
                _proxyStats.LastBalanceTime = $"{DateTime.Now}";
                proxyStats.Assign(_proxyStats);
            }
            await UpdateProxyStatsDb(db, proxyStats);
        }
    }
}
