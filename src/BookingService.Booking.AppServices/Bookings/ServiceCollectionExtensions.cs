using BookingService.Booking.AppServices.Dates;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Booking.AppServices.Bookings;

public static class ServiceCollectionExtensions
{
  public static void AddAppServices(this IServiceCollection services)
  {
    services.AddScoped<IBookingsService, BookingsService>();
    services.AddSingleton<ICurrentDateTimeProvider, DefaultCurrentDateTimeProvider>();
  }
}