using System;
using System.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using HangFire.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace HangFire.App_Start
{
    public class Startup : JYMNLogger
    {
        public void Configuration(IAppBuilder app)
        {
            //配置轮询间隔
            SqlServerStorageOptions options = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(15) //队列
                //TransactionTimeout = TimeSpan.FromMinutes(30) //超时时间
            };

            //指定hangfire使用数据库存储后台任务执行
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.AppSettings["EfConnectString"], options);

            //启用hangfireserver中间件（它会自动释放）
            BackgroundJobServerOptions backgroundOptions = new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * 10 }; //设置处理的并发数
            app.UseHangfireServer(backgroundOptions);

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new IpAuthorizationFilter() }  //设置能访问的IP
            });
            //启用Hangfire的仪表盘（可以看到任务状态，进度等信息）
            app.UseHangfireDashboard();

            //设置定时的任务
            //RecurringJob.AddOrUpdate("OnTimeTimerTest", () => this.SetTestRegisterScheduleJobs(), "* */6 * * *",
            //    TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            RecurringJob.AddOrUpdate(() => this.OnTimeTestRegister("AddOrUpdate"), "* */1 * * *", TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            // 在任务被持久化到数据库之后，Hangfire服务端立即从数据库获取相关任务并装载到相应的Job Queue下，在没有异常的情况下仅处理一次，若发生异常，提供重试机制，异常及重试信息都会被记录到数据库中，通过Hangfire控制面板可以查看到这些信息
            BackgroundJob.Enqueue(() => this.OnTimeTestRegister("Enqueue"));

            //延迟（计划）任务跟队列任务相似，客户端调用时需要指定在一定时间间隔后调用：
            BackgroundJob.Schedule(() => this.OnTimeTestRegister("Schedule"), TimeSpan.FromSeconds(5));
        }

        public bool OnTimeTestRegister(string type)
        {
            this.Logger.Info("已成功处理" + type);
            return true;
        }

        /// <summary>
        /// 定时（循环）任务代表可以重复性执行多次，支持CRON表达式
        /// </summary>
        public void SetTestRegisterAddOrUpdateJobs()
        {
            RecurringJob.AddOrUpdate(() => this.OnTimeTestRegister("AddOrUpdate"), "* */6 * * *");
        }

        /// <summary>
        /// 在任务被持久化到数据库之后，Hangfire服务端立即从数据库获取相关任务并装载到相应的Job Queue下，在没有异常的情况下仅处理一次，若发生异常，提供重试机制，异常及重试信息都会被记录到数据库中，通过Hangfire控制面板可以查看到这些信息
        /// </summary>
        public void SetTestRegisterEnqueueJobs()
        {
            BackgroundJob.Enqueue(() => this.OnTimeTestRegister("Enqueue"));
        }

        /// <summary>
        /// 延迟（计划）任务跟队列任务相似，客户端调用时需要指定在一定时间间隔后调用：
        /// </summary>
        public void SetTestRegisterScheduleJobs()
        {
            BackgroundJob.Schedule(() => this.OnTimeTestRegister("Schedule"), TimeSpan.FromSeconds(5));
        }
    }
}