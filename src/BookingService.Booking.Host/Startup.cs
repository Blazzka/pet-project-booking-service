﻿using BookingService.Booking.AppServices.Booking;
using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Booking.Host;

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
				Status = 400,
				Type = $"https://httpstatuses.com/{400}",
				Title = ex.Message
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
		app.UseRouting();
		app.UseEndpoints(builder => builder.MapControllers());
	}
}