using BookingService.Booking.Api.Contracts.Booking;
using BookingService.Booking.Api.Contracts.Booking.Dtos;

namespace BookingService.Booking.AppServices.Booking;

public class BookingsQueries : IBookingsQueries
{
	public Task<BookingData[]> GetByFilter(long? userId = null, long? resourseId = null, int pageNumber = 1, int pageSize = 25, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
	public Task<BookingStatus> GetStatusById(long id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}