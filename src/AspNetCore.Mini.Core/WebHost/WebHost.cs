using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public class WebHost : IWebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;

        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        public Task Run() => _server.StartAsync(_handler);
    }
}
