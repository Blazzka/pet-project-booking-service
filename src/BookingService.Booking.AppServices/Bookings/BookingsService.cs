using BookingService.Booking.Api.Contracts.Bookings.DTOs;
using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain;
using BookingService.Booking.Domain.Bookings;

namespace BookingService.Booking.AppServices.Bookings;

public class BookingsService : IBookingsService
{
	private readonly ICurrentDateTimeProvider _dateTimeProvider;
	private readonly IUnitOfWork _unitOfWork;

	public BookingsService(IUnitOfWork unitOfWork, ICurrentDateTimeProvider dateTimeProvider)
	{
		_unitOfWork = unitOfWork;
		_dateTimeProvider = dateTimeProvider;
	}

	public async Task<long> Create(long userId, long resourceId, DateOnly startDate, DateOnly endDate,
		CancellationToken cancellationToken = default)
	{
		var booking = BookingAggregate.Initialize(userId, resourceId, startDate, endDate, _dateTimeProvider.UtcNow);
		_unitOfWork.BookingsRepository.Create(booking);
		await _unitOfWork.CommitAsync(cancellationToken);
		return booking.Id;
	}

	public async Task<BookingData?> GetById(long id, CancellationToken cancellationToken = default)
	{
		var booking = await GetBookingById(id, cancellationToken);
		return new BookingData
		{
			Id = booking.Id,
			BookingStatus = booking.Status,
			UserId = booking.UserId,
			ResourceId = booking.ResourceId,
			BookedFrom = booking.BookedFrom,
			BookedTo = booking.BookedTo,
			CreatedAt = booking.CreatedAt
		};
	}

	public async Task Cancel(long id, CancellationToken cancellationToken = default)
	{
		var booking = await GetBookingById(id, cancellationToken);
		if (booking == null) throw new ValidationException($"Бронирование с указанным id: '{id}' не найдено.");
		booking.Cancel(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.DateTime));
		_unitOfWork.BookingsRepository.Update(booking);
		await _unitOfWork.CommitAsync(cancellationToken);
	}

	private async Task<BookingAggregate> GetBookingById(long id, CancellationToken cancellationToken)
	{
		var booking = await _unitOfWork.BookingsRepository.GetById(id, cancellationToken);
		if (booking == null) throw new ValidationException($"Бронирование с указанным id: '{id}' не найдено.");
		return booking;
	}
}