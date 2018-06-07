using Quartz;
using Quartz.Impl;
using Topshelf;

namespace DesignPatternDemo.quartz
{
    public class QuartzInit
    {
        //public static void Main()
        //{
        //    IScheduler scheduler = SetupTestScheduler();

        //    Action<IAppBuilder> startup = app => app.UseCrystalQuartz(scheduler);

        //    using (WebApp.Start($"http://localhost:{Settings.ListenPoint}/", startup))
        //    {
        //        Console.WriteLine("Server is started");
        //        Console.WriteLine();
        //        Console.WriteLine($"Check http://localhost:{Settings.ListenPoint}/quartz to see jobs information");
        //        Console.WriteLine();

        //        Console.WriteLine("Starting scheduler...");
        //        scheduler.Start();

        //        Console.WriteLine("Scheduler is started");
        //        Console.WriteLine();
        //        Console.WriteLine("Press [ENTER] to close");
        //        Console.WriteLine();
        //        Console.WriteLine();
        //        ConsoleKeyInfo key = Console.ReadKey();
        //        if (key.Key == ConsoleKey.Enter)
        //        {
        //            scheduler.Shutdown(false);
        //            Environment.Exit(0);
        //        }
        //    }
        //}

        //使用TopSelf
        //public static void Main(string[] args)
        //{
        //    HostFactory.New(x =>
        //    {
        //        x.StartAutomatically();
        //        x.UseAssemblyInfoForServiceInfo();
        //        x.Service<QuartzServiceRunner>(s =>
        //        {
        //            s.ConstructUsing(name => new QuartzServiceRunner());
        //            s.WhenStarted(qs => qs.Start());
        //            s.WhenStopped(qs => qs.Stop());
        //        });

        //        x.SetServiceName("Quartz任务启动");
        //        x.SetDisplayName("Quartz任务启动");
        //        x.SetDescription("Quartz任务启动");
        //        x.EnableServiceRecovery(sr =>
        //        {
        //            sr.RestartService(1);
        //            sr.RestartService(2);
        //        });
        //    }).Run();
        //}

        /// <summary>
        ///     测试定时任务启动
        /// </summary>
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