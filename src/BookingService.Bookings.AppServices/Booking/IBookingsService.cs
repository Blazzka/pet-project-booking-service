using BookingService.Bookings.Api.Contracts.Booking.Dtos;
using BookingService.Bookings.Api.Contracts.Booking.Requests;

namespace BookingService.Bookings.AppServices.Booking;

public interface IBookingsService
{
	public Task<long> Create(long BookingId, long ResourseId, DateOnly StartDate, DateOnly EndDate);
	public Task<BookingData> GetById(long BookingId);
	public Task Cancel(long BookingId);
}