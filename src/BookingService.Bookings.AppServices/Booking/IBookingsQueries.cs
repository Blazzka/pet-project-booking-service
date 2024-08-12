using BookingService.Bookings.Api.Contracts.Booking;
using BookingService.Bookings.Api.Contracts.Booking.Dtos;

namespace BookingService.Bookings.AppServices.Booking;
public interface IBookingsQueries
{
	public Task<BookingData[]> GetByFilter();
	public Task<BookingStatus> GetStatusById();
}