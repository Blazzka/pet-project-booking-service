using BookingService.Bookings.Api.Contracts.Booking.Dtos;
using BookingService.Bookings.Api.Contracts.Booking.Requests;
using BookingService.Bookings.Domain;
using BookingService.Bookings.AppServices.Exceptions;

namespace BookingService.Bookings.AppServices.Booking;

public class BookingsService : IBookingsService
{
	public Task<long> Create(long BookingId, long ResourseId, DateOnly StartDate, DateOnly EndDate)
	{
		throw new ValidationException();
	}
	public Task<BookingData> GetById(long BookingId)
	{
		throw new NotImplementedException();
	}
	public Task Cancel(long BookingId)
	{
		throw new DomainException();
	}
}