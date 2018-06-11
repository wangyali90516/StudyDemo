using System;
using CrystalQuartz.Owin;
using Microsoft.Owin.Hosting;
using Owin;
using Quartz;
using Quartz.Impl;

namespace DesignPatternDemo.quartz
{
    internal class QuartzServiceRunner
    {
        public static IScheduler quartzScheduler;

        /// <summary>
        ///     The web application
        /// </summary>
        private IDisposable webApp;

        public QuartzServiceRunner()
        {
            quartzScheduler = SetupTestScheduler();
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            Action<IAppBuilder> startup = app => app.UseCrystalQuartz(quartzScheduler);

            this.webApp = WebApp.Start($"http://localhost:{Settings.ListenPoint}/", startup);

            Console.WriteLine("Server is started");
            Console.WriteLine();
            Console.WriteLine($"Check http://localhost:{Settings.ListenPoint}/quartz to see jobs information");
            Console.WriteLine();

            Console.WriteLine("Starting scheduler...");
            quartzScheduler.Start();

            Console.WriteLine("Scheduler is started");
            Console.WriteLine();
            Console.WriteLine("Press [ENTER] to close");
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        public void Stop()
        {
            quartzScheduler.Clear();
            quartzScheduler.Shutdown(false);
            this.webApp.Dispose();
        }

        private static IScheduler SetupTestScheduler()
        {
            string time = Settings.ExcuteTime;
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();

            IJobDetail pendingTestJob = JobBuilder.Create<TestJob>().WithIdentity("测试定时任务启动", "测试定时任务启动").Build();
            ITrigger pendingAllocateAssetJobtrigger = TriggerBuilder.Create().WithIdentity("测试定时任务启动", "测试定时任务启动").StartNow().WithCronSchedule(time).Build();
            scheduler.ScheduleJob(pendingTestJob, pendingAllocateAssetJobtrigger);

            return scheduler;
        }
    }
}