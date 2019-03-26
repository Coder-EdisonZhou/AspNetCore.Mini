﻿using AspNetCore.Mini.Core;
using System;

namespace AspNetCore.Mini.AppCore
{
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

        public static RequestDelegate FooMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        public static RequestDelegate BazMiddleware(RequestDelegate next)
        => context => context.Response.WriteAsync("Baz");
    }
}