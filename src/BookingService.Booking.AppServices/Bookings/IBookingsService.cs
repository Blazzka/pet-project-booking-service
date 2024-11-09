using BookingService.Booking.Api.Contracts.Bookings.DTOs;

namespace BookingService.Booking.AppServices.Bookings;

public interface IBookingsService
{
	public Task<long> Create(long userId, long resourceId, DateOnly bookedFrom, DateOnly bookedTo,
		CancellationToken cancellationToken = default);

	public Task<BookingData?> GetById(long id, CancellationToken cancellationToken = default);
	public Task Cancel(long id, CancellationToken cancellationToken = default);
}