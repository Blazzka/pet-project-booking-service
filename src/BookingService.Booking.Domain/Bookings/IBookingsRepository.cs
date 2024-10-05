namespace BookingService.Booking.Domain.Bookings;

public interface IBookingsRepository
{
	public void Create(BookingAggregate bookingAggregate);

	public ValueTask<BookingAggregate?> GetById(long id, CancellationToken cancellationToken = default);

	public void Update(BookingAggregate aggregate);
}