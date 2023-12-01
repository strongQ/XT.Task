using FreeRedis;
using FreeScheduler;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Config;
using XT.FeSql;
using XT.FeSql.Models;

namespace XT.Task.Extensions
{
    public static class XTTaskSetup
    {
       
        public static IServiceCollection AddXTTaskSetup(this IServiceCollection services,bool useStorage=false, XTDbConfig xtconfig = null)
        {
            Enum.GetValues(typeof(TaskInterval));
            Enum.GetValues(typeof(FreeScheduler.TaskStatus));
          
            if (xtconfig == null && useStorage)
            {
                xtconfig = AppSettings.GetObjData<XTDbConfig>();
               
               
            }



          



            var scheduler = new FreeSchedulerBuilder().OnExecuting(info =>
            {
                // info数据通知给外部
                TaskAction.Notify(info);
               
            });

            if (useStorage)
            {
               var fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(xtconfig.Dbs[0].DbType, xtconfig.Dbs[0].ConnectionString)
                .UseAutoSyncStructure(true)
                .UseNoneCommandParameter(true)
                .Build();
                scheduler=scheduler.UseStorage(fsql);
            }
           var build= scheduler.Build();

            services.AddSingleton(build);

            return services;


        }
    }
}
