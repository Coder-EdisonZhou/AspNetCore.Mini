using System;
using System.Collections.Generic;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// WebHost应用宿主构造器
    /// </summary>
    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer _server;
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="configure">中间件</param>
        /// <returns>IWebHostBuilder</returns>
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        /// <summary>
        /// 指定要使用的具体Server
        /// </summary>
        /// <param name="server">具体Server</param>
        /// <returns>IWebHostBuilder</returns>
        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }

        /// <summary>
        /// 构造具体WebHost应用宿主
        /// </summary>
        /// <returns>IWebHost</returns>
        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var configure in _configures)
            {
                configure(builder);
            }

            return new WebHost(_server, builder.Build());
        }
    }
}
