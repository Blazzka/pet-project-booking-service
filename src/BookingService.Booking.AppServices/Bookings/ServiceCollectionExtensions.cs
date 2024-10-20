using BookingService.Booking.AppServices.Bookings.Jobs;
using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.AppServices.Options;
using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Persistence;
using BookingService.Catalog.Api.Contracts.BookingJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestEase;

namespace BookingService.Booking.AppServices.Bookings;

public static class ServiceCollectionExtensions
{
  public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IBookingsService, BookingsService>();
    services.AddSingleton<ICurrentDateTimeProvider, DefaultCurrentDateTimeProvider>();
    services.Configure<BookingCatalogRestOptions>(configuration.GetSection(nameof(BookingCatalogRestOptions)));
    services.Configure<BaseAddressAttribute>(configuration.GetSection("BaseAddress"));
    services.AddHttpClient(nameof(BookingCatalogRestOptions),
      (ctx, client) =>
      {
        var options = ctx.GetRequiredService<IOptions<BookingCatalogRestOptions>>().Value;
        client.BaseAddress = new Uri(options.BaseAddress);
        client.Timeout = TimeSpan.FromSeconds(90);
      });
    services.AddScoped<IBookingJobsController>(ctx =>
      RestClient.For<IBookingJobsController>(ctx.GetRequiredService<IHttpClientFactory>()
        .CreateClient(nameof(BookingCatalogRestOptions))));
    services.AddHostedService<BookingsBackgroundService>();
    services.AddScoped<IBookingsBackgroundQueries, BookingsBackgroundQueries>();
    services.AddScoped<IBookingsBackgroundServiceHandler, BookingsBackgroundServiceHandler>();
  }
}