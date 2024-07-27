using Microsoft.Extensions.Configuration;

public class Startup
{
  public IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }


  public void ConfigureServices(IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
  }
  public void Configure(IApplicationBuilder app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  } 
}