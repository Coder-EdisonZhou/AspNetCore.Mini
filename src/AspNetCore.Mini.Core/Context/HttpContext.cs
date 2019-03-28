using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// 自定义Http上下文
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// Http请求对象
        /// </summary>
        public HttpRequest Request { get; }

        /// <summary>
        /// Http响应对象
        /// </summary>
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }

    /// <summary>
    /// 自定义Http请求类
    /// </summary>
    public class HttpRequest
    {
        private readonly IHttpRequestFeature _feature;
        public Uri Url => _feature.Url;
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
    }

    /// <summary>
    /// 自定义Http响应类
    /// </summary>
    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;
        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
    }

    public static partial class Extensions
    {
        /// <summary>
        /// 为HttpResponse对象扩展响应输出方法
        /// </summary>
        /// <param name="response">HttpResponse</param>
        /// <param name="contents">输出内容</param>
        /// <returns>Task</returns>
        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
