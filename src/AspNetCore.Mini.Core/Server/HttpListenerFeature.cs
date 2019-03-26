using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace AspNetCore.Mini.Core
{
    /// <summary>
    /// 基于HttpListener的Feature
    /// </summary>
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        private readonly HttpListenerContext _context;

        public HttpListenerFeature(HttpListenerContext context) => _context = context;

        Uri IHttpRequestFeature.Url => _context.Request.Url;

        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;

        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;

        Stream IHttpRequestFeature.Body => _context.Request.InputStream;

        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;

        int IHttpResponseFeature.StatusCode
        {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }
    }
}
