using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// 基于HttpListener的HttpServer
    /// *.HttpListener基于HTTP.sys
    /// </summary>
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;
        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            // 绑定默认监听地址（默认端口为5000）
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task RunAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            if (!_httpListener.IsListening)
            {
                // 启动HttpListener
                _httpListener.Start();
            }
            Console.WriteLine("[Info]: Server started and is listening on: {0}", string.Join(";", _urls));

            while (true)
            {
                // 等待传入的请求，该方法将阻塞进程（这里使用了await），直到收到请求
                var listenerContext = await _httpListener.GetContextAsync();
                // 打印状态行: 请求方法, URL, 协议版本
                Console.WriteLine("{0} {1} HTTP/{2}", 
                    listenerContext.Request.HttpMethod, 
                    listenerContext.Request.RawUrl, 
                    listenerContext.Request.ProtocolVersion);
                // 获取抽象封装后的HttpListenerFeature
                var feature = new HttpListenerFeature(listenerContext);
                // 获取封装后的Feature集合
                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);
                // 创建HttpContext
                var httpContext = new HttpContext(features);
                Console.WriteLine("[Info]: Server process one HTTP request start.");
                // 开始依次执行中间件
                await handler(httpContext);
                Console.WriteLine("[Info]: Server process one HTTP request end.");
                // 关闭响应
                listenerContext.Response.Close();
            }
        }
    }

    /// <summary>
    /// IWebHostBuilder扩展：使用基于HttpListener的Server
    /// </summary>
    public static partial class Extensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
            => builder.UseServer(new HttpListenerServer(urls));
    }
}
