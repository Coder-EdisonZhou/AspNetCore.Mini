using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// WebHost应用宿主
    /// </summary>
    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        /// <summary>
        /// 调用Server的启动方法进行启动
        /// </summary>
        /// <returns></returns>
        public Task Run() => _server.RunAsync(_handler);
    }
}
