using BookingService.Bookings.AppServices.Booking;
using BookingService.Bookings.Domain;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

public class Startup
{
  public IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  { 
    services.AddControllers();
  
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAppServices();
		services.AddProblemDetails(options =>
		{
			// Если окружение Development, включаем подробное описание ошибки в ответ.
			options.IncludeExceptionDetails = (context, _) =>
			{
				var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
				return env.IsDevelopment();
			};

			options.Map<ValidationException>(ex => new ProblemDetails
			{
				Status = 402,
				Type = $"https://httpstatuses.com/{400}",
				Title = ex.Message,
			});
			
			options.Map<DomainException>(ex => new ProblemDetails
			{
				Status = 402,
				Type = $"https://httpstatuses.com/{402}",
				Title = ex.Message,
				Detail = ex.StackTrace
			});



		});

	}
  public void Configure(IApplicationBuilder app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();
		app.UseProblemDetails();
	} 
}