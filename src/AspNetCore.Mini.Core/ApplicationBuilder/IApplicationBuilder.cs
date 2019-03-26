using System;

namespace AspNetCore.Mini.Core
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);

        RequestDelegate Build();
    }
}
