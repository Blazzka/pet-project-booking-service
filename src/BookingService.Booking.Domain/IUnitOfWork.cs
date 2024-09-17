using BookingService.Booking.Domain.Booking;

namespace BookingService.Booking.Domain;

public interface IUnitOfWork
{
	public IBookingsRepository BookingsRepository { get; }
	Task CommitAsync(CancellationToken cancellationToken = default);
}