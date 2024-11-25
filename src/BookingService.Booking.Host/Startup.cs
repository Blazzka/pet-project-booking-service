using BookingService.Booking.AppServices.Bookings;
using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain.Exceptions;
using BookingService.Booking.Persistence;
using BookingService.Catalog.Async.Api.Contracts.Events;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;

namespace BookingService.Booking.Host;

public class Startup
{
	private IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		var connectionString = Configuration.GetConnectionString("BookingsContext");

		services.AddControllers();
		services.AddPersistence(connectionString!);
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddAppServices(Configuration);
		services.AddRebus((builder, ctx) =>
			builder.Transport(t =>
					t.UseRabbitMq(ctx.GetRequiredService<IOptions<RebusRabbitMqOptions>>().Value.ConnectionString,
							"booking-service-bookings-queue")
						.DefaultQueueOptions(queue => queue.SetDurable(true))
						.ExchangeNames("booking-service-booking-direct", "booking-service-topics"))
				.Serialization(s => s.UseSystemTextJson())
				.Logging(l => l.Serilog())
				.Routing(r => r.TypeBased()));
		services.Configure<RebusRabbitMqOptions>(Configuration.GetSection("RebusRabbitMqOptions"));
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
		app.ApplicationServices.GetRequiredService<IBus>().Subscribe<BookingJobConfirmed>();
		app.ApplicationServices.GetRequiredService<IBus>().Subscribe<BookingJobDenied>();
		app.UseEndpoints(builder => builder.MapControllers());
	}
}