# AspNetCore.Mini
一个超简迷你版的ASP.NET Core框架，真实模拟+足够简单+可以执行，基于蒋金楠老师的分享改写成的基于.NET Standard的版本，有.NET Framework和.NET Core两个服务端的启动程序。

## 三个重要的特点
<img src="https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080821641-164935959.jpg" style="border: 1px solid #ddd; border-radius: 5px;" alt="ASP.NET Core Mini"/>
> 真实模拟+足够简单+可以执行

## 一些缺失的特性
<img src="https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080903608-382600093.jpg" alt="ASP.NET Core Mini"/>
> 依赖注入、以Startup和StartupFilter的中间件注册方式、针对多种数据源的配置系统、诊断日志系统和一系列预定义的中间件

## 运行效果展示
```C#
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args)
            .Build()
            .Run();

        Console.ReadKey();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return new WebHostBuilder()
            .UseHttpListener()
            .Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware));
    }
}    
```
> 三个自定义中间件的执行显示结果如下图所示
<img src="https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080825603-1802425781.jpg" alt="ASP.NET Core Mini"/>


## 参考博文
[蒋金楠：200行代码，7个对象——带你了解ASP.NET Core的本质](https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html)
