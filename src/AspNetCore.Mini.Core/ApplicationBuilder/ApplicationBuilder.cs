using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// Mini ASP.NET Core请求处理构造器
    /// </summary>
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 构建请求处理管道
        /// </summary>
        /// <returns>RequestDelegate</returns>
        public RequestDelegate Build()
        {
            _middlewares.Reverse(); // 与注册中间件相反的顺序去执行中间件
            return httpContext =>
            {
                // 注册默认中间件 => 返回404响应
                RequestDelegate next = _ => {
                    _.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };

                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }

                return next(httpContext);
            };
        }

        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="middleware">中间件</param>
        /// <returns>ApplicationBuilder</returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
