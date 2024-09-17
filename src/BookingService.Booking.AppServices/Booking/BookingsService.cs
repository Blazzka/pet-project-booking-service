using BookingService.Booking.Api.Contracts.Booking.Dtos;
using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain.Exceptions;

namespace BookingService.Booking.AppServices.Booking;

public class BookingsService : IBookingsService
{
	public Task<long> Create(long id, long resourceId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
	{
		throw new ValidationException("Произошла ошибка валидации");
	}

	public Task<BookingData> GetById(long id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task Cancel(long id, CancellationToken cancellationToken = default)
	{
		throw new DomainException("Произошла ошибка бизнес логики");
	}
}