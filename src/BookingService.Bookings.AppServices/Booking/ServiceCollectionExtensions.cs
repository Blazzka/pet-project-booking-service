namespace BookingService.Bookings.AppServices.Booking;
using Microsoft.Extensions.DependencyInjection;


public static class  ServiceCollectionExtensions
{
	public static void AddAppServices(this IServiceCollection ServiceCollection)
	{
		ServiceCollection.AddScoped<BookingsService>();
		ServiceCollection.AddScoped<BookingsQueries>();
	}
}

