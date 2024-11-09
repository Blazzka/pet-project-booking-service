using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.AppServices.EventHandlers;
using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace BookingService.Booking.AppServices.Bookings;

public static class ServiceCollectionExtensions
{
	public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IBookingsService, BookingsService>();
		services.AddSingleton<ICurrentDateTimeProvider, DefaultCurrentDateTimeProvider>();
		services.AddScoped<IBookingsBackgroundQueries, BookingsBackgroundQueries>();
		// Регистрация обработчиков Rebus
		services.AddRebusHandler<BookingJobConfirmedEventHandler>();
		services.AddRebusHandler<BookingJobDeniedEventHandler>();
	}
}