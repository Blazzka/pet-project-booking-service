using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookingService.Booking.AppServices.Bookings.Jobs;

public class BookingsBackgroundService : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<BookingsBackgroundService> _logger;

	public BookingsBackgroundService(IServiceProvider serviceProvider,
		ILogger<BookingsBackgroundService> logger)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
			using (var scope = _serviceProvider.CreateScope())
			{
				var handler = scope.ServiceProvider.GetRequiredService<IBookingsBackgroundServiceHandler>();

				try
				{
					_logger.LogInformation("Служба BookingsBackgroundService запущена.");
					await handler.Handle(stoppingToken);
					await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
					_logger.LogInformation("Служба BookingsBackgroundService остановлена.");
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Возникла ошибка при выполнении BookingsBackgroundService.");
					await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
				}
			}
	}
}