using BookingService.Booking.Host;

var host = new HostBuilderFactory<Startup>()
  .CreateHostBuilder(args)
  .Build();

await host.RunAsync();