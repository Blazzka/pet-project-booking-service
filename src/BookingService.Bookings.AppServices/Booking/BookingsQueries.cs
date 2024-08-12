using BookingService.Bookings.Api.Contracts.Booking;
using BookingService.Bookings.Api.Contracts.Booking.Dtos;

namespace BookingService.Bookings.AppServices.Booking;

public class BookingsQueries : IBookingsQueries
{
	public Task<BookingData[]> GetByFilter()
	{
		throw new NotImplementedException();
	}
	public Task<BookingStatus> GetStatusById()
	{
		throw new NotImplementedException();
	}
}