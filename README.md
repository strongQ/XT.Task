# XT.Task任务调度
扩展 FreeScheduler,便于使用

# 使用方式
```
1. 注入服务
builder.Services.AddXTTaskSetup(true,config);

2. aot发布必须添加
Enum.GetValues(typeof(TaskInterval));
Enum.GetValues(typeof(FreeScheduler.TaskStatus));

3. 添加默认任务
var scheduler=app.Services.GetService<Scheduler>();
scheduler.AddDefaultTask();

4. 添加Web UI界面
app.UseFreeSchedulerUI("/task/");

5. 使用扩展方法
scheduler.AddTaskAction("测试任务", "OVER MY BODY", TaskTypeEnum.Second, "10");

6. 监听执行事件
TaskAction.ExecuteEvent += TaskAction_ExecuteEvent;

```