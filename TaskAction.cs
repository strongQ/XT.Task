using FreeScheduler;
using XT.Task.Enums;

namespace XT.Task
{
    public static class TaskAction
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        public static event EventHandler<TaskInfo> ExecuteEvent;
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="info"></param>
        internal static void Notify(TaskInfo info)
        {
            ExecuteEvent?.Invoke(null,info);
        }
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="topic"></param>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="time">时间格式 -1:20:00:00 每天20点 1:20:00:00 1号20点</param>
        /// <param name="round"></param>
        /// <returns></returns>
        public static string AddTaskAction(this Scheduler scheduler,string topic,string body,TaskTypeEnum type,string time,int round = -1)
        {
            string id = string.Empty;
            switch (type)
            {
                case TaskTypeEnum.Second:
                    int.TryParse(time, out int second);
                    id= scheduler.AddTask(topic, body, round, second);
                    break;
                case TaskTypeEnum.Day:
                    id = scheduler.AddTaskRunOnDay(topic, body, round, time);
                    break;
                case TaskTypeEnum.Week:
                    id = scheduler.AddTaskRunOnWeek(topic, body, round, time);
                    break;
                case TaskTypeEnum.Month:
                    id = scheduler.AddTaskRunOnMonth(topic, body, round, time);
                    break;
                default:
                    break;
            }
            return id;
        }
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="scheduler"></param>
        /// <param name="topic"></param>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string AddTaskAction(this Scheduler scheduler, string topic, string body, TaskTypeEnum type, int[] times)
        {
           return scheduler.AddTask(topic, body, times);
        }
        /// <summary>
        /// 添加默认任务
        /// </summary>
        /// <param name="scheduler"></param>
        public static void AddDefaultTask(this Scheduler scheduler)
        {
            if (Datafeed.GetPage(scheduler, null, null, null, null).Total == 0)
            {
                scheduler.AddTask("[系统预留]清理任务数据", "86400", -1, 3600);

                scheduler.AddTaskAction("测试任务", "OVER MY BODY", TaskTypeEnum.Second, "10");
            }
        }
    }
}
