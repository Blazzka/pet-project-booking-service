using BookingService.Booking.Api.Contracts.Booking.Dtos;
using BookingService.Booking.Domain;
using BookingService.Booking.AppServices.Exceptions;

namespace BookingService.Booking.AppServices.Booking;

public class BookingsService : IBookingsService
{
	public Task<long> Create(long id, long resourseId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken = default)
	{
		throw new ValidationException("Произошла ошибка валидации");
	}

	public Task<BookingData> GetById(long Id, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task Cancel(long Id, CancellationToken cancellationToken = default)
	{
		throw new DomainException("Произошла ошибка бизнес логики");
	}
}