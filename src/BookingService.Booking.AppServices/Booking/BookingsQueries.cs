using BookingService.Booking.Api.Contracts.Booking.Dtos;
using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.AppServices.Booking;

public class BookingsQueries : IBookingsQueries
{
	public Task<BookingData[]> GetByFilter(long? userId = null, long? resourceId = null, int pageNumber = 1,
		int pageSize = 25, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<BookingStatus> GetStatusById(long id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}