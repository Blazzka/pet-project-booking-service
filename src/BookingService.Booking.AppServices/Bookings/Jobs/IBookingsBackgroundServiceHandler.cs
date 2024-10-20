namespace BookingService.Booking.AppServices.Bookings.Jobs;

public interface IBookingsBackgroundServiceHandler
{
	Task Handle(CancellationToken cancellationToken);
}