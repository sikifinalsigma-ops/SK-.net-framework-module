using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SK_Task
{
    public sealed class QuartzManager : IDisposable
    {
        private static readonly Lazy<QuartzManager> _instance =
            new Lazy<QuartzManager>(() => new QuartzManager());

        public static QuartzManager Instance => _instance.Value;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private IScheduler _scheduler;
        private NameValueCollection _properties;

        private QuartzManager()
        {
            _properties = CreateDefaultProperties();
        }

        #region 初始化

        public async Task InitializeAsync(NameValueCollection properties = null)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_scheduler != null && !_scheduler.IsShutdown)
                    return;

                _properties = properties ?? _properties;

                var factory = new StdSchedulerFactory(_properties);
                _scheduler = await factory.GetScheduler();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task EnsureInitializedAsync()
        {
            if (_scheduler != null)
                return;

            await InitializeAsync();
        }

        #endregion

        #region 调度器控制

        public async Task StartAsync()
        {
            await EnsureInitializedAsync();

            if (_scheduler.IsShutdown)
                throw new Exception("调度器已关闭，请重新初始化");

            if (_scheduler.InStandbyMode || !_scheduler.IsStarted)
            {
                await _scheduler.Start();
            }
        }

        public async Task StandbyAsync()
        {
            await EnsureInitializedAsync();
            await _scheduler.Standby();
        }

        public async Task ShutdownAsync(bool waitForJobsToComplete = true)
        {
            if (_scheduler != null && !_scheduler.IsShutdown)
            {
                await _scheduler.Shutdown(waitForJobsToComplete);
            }
        }

        /// <summary>
        /// 重新创建 scheduler（Shutdown 后用）
        /// </summary>
        public async Task RebuildAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_scheduler != null && !_scheduler.IsShutdown)
                {
                    await _scheduler.Shutdown(true);
                }

                var factory = new StdSchedulerFactory(_properties);
                _scheduler = await factory.GetScheduler();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        #endregion

        #region 添加任务

        public async Task AddCronJobAsync<TJob>(
            string jobName,
            string jobGroup,
            string triggerName,
            string triggerGroup,
            string cronExpression,
            JobDataMap data = null) where TJob : IJob
        {
            await EnsureInitializedAsync();

            var jobKey = new JobKey(jobName, jobGroup);

            if (await _scheduler.CheckExists(jobKey))
                throw new Exception("任务已存在");

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobKey)
                .UsingJobData(data ?? new JobDataMap())
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGroup)
                .ForJob(jobKey)
                .WithCronSchedule(cronExpression)
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        public async Task addSimplejob(string jobName,
            string jobGroup,
            string triggerName,
            string triggerGroup,
            int seconds,Dictionary<string,object> jobdataMap) 
        {
            JobDataMap jdm = new JobDataMap();
            foreach (var one in jobdataMap.Keys)
            {
                jdm.Put(one, jobdataMap[one]);
            }
            await AddSimpleJobAsync<InvokeJob>(jobName,jobGroup,triggerName,triggerGroup,seconds, jdm);

        }

 
        public async Task AddSimpleJobAsync<TJob>(
            string jobName,
            string jobGroup,
            string triggerName,
            string triggerGroup,
            int seconds,
            JobDataMap jobData = null,
            JobDataMap triggerData = null
            ) where TJob : IJob
        {
            await EnsureInitializedAsync();

            var jobKey = new JobKey(jobName, jobGroup);

            if (await _scheduler.CheckExists(jobKey))
                throw new Exception("任务已存在");

            var jobBuilder = JobBuilder.Create<TJob>()
                .WithIdentity(jobKey);

            if (jobData != null)
                jobBuilder = jobBuilder.UsingJobData(jobData);

            var job = jobBuilder.Build();

            var triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(triggerName, triggerGroup)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(seconds)
                    .RepeatForever());

            if (triggerData != null)
                triggerBuilder = triggerBuilder.UsingJobData(triggerData);

            var trigger = triggerBuilder.Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        #endregion

        #region 控制任务

        public async Task PauseJobAsync(string jobName, string jobGroup)
        {
            await EnsureInitializedAsync();
            await _scheduler.PauseJob(new JobKey(jobName, jobGroup));
        }

        public async Task ResumeJobAsync(string jobName, string jobGroup)
        {
            await EnsureInitializedAsync();
            await _scheduler.ResumeJob(new JobKey(jobName, jobGroup));
        }

        public async Task TriggerJobAsync(string jobName, string jobGroup)
        {
            await EnsureInitializedAsync();
            await _scheduler.TriggerJob(new JobKey(jobName, jobGroup));
        }

        public async Task DeleteJobAsync(string jobName, string jobGroup)
        {
            await EnsureInitializedAsync();
            await _scheduler.DeleteJob(new JobKey(jobName, jobGroup));
        }

        #endregion

        #region 查询

        public async Task<List<string>> GetAllJobsAsync()
        {
            await EnsureInitializedAsync();

            var result = new List<string>();
            var groups = await _scheduler.GetJobGroupNames();

            foreach (var group in groups)
            {
                var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(group));

                result.AddRange(jobKeys.Select(j => $"{j.Group}.{j.Name}"));
            }

            return result;
        }

        #endregion

        #region 同步封装（给 WinForms 用）

        public void Start()
        {
            StartAsync().Wait();
        }

        public void Stop()
        {
            StandbyAsync().Wait();
        }

        #endregion

        private NameValueCollection CreateDefaultProperties()
        {
            return new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "SKScheduler" },
                { "quartz.threadPool.threadCount", "20" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" }
            };
        }

        public void Dispose()
        {
            try
            {
                if (_scheduler != null && !_scheduler.IsShutdown)
                {
                    _scheduler.Shutdown(true).GetAwaiter().GetResult();
                }
            }
            catch
            {
            }

            _semaphore?.Dispose();
        }
    }
}