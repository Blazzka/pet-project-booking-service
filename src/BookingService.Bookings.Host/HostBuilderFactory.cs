﻿using Serilog;

public class HostBuilderFactory<TStartup> where TStartup : class
{
    public IHostBuilder CreateHostBuilder(string[] args, string? baseDirectory = null)
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, configurationBuilder) =>
            {
                if (!string.IsNullOrWhiteSpace(baseDirectory))
                    configurationBuilder.SetBasePath(baseDirectory);
            })
            .ConfigureWebHostDefaults(host => { host.UseStartup<TStartup>(); })
            .UseSerilog((ctx, logBuilder) =>
            {
                logBuilder.ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.FromLogContext();
            });

        return builder;
    }
}