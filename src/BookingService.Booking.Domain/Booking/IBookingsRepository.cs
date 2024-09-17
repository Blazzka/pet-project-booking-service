namespace BookingService.Booking.Domain.Booking;

public interface IBookingsRepository
{
	public void Create(BookingAggregate bookingAggregate);

	public ValueTask<BookingAggregate?> GetById(long id, CancellationToken cancellationToken = default);

	public void Update(BookingAggregate aggregate);
}