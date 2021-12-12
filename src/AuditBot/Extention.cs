using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditBot
{
    public static class Extention
    {
        public static IServiceCollection AddAuditBot(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }


        public static IApplicationBuilder UseAuditBot(this IApplicationBuilder app, IConfiguration configuration)
        {
            configuration.GetSection("");

            return app;
        }

    }
}
