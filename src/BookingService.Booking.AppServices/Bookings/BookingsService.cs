using BookingService.Booking.Api.Contracts.Bookings.DTOs;
using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain;
using BookingService.Booking.Domain.Bookings;
using BookingService.Catalog.Async.Api.Contracts.Requests;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace BookingService.Booking.AppServices.Bookings;

public class BookingsService : IBookingsService
{
	private readonly IBus _bus;
	private readonly ICurrentDateTimeProvider _dateTimeProvider;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ILogger<BookingsService> _logger;

	public BookingsService(IBus bus, IUnitOfWork unitOfWork, ICurrentDateTimeProvider dateTimeProvider, ILogger<BookingsService> logger)
	{
		_bus = bus;
		_unitOfWork = unitOfWork;
		_dateTimeProvider = dateTimeProvider;
		_logger = logger;
	}

	public async Task<long> Create(long userId, long resourceId, DateOnly bookedFrom, DateOnly bookedTo,
		CancellationToken cancellationToken = default)
	{
		var booking = BookingAggregate.Initialize(userId, resourceId, bookedFrom, bookedTo, _dateTimeProvider.UtcNow);
		var requestId = Guid.NewGuid();
		booking.SetCatalogRequestId(requestId);
		_unitOfWork.BookingsRepository.Create(booking);
		await _unitOfWork.CommitAsync(cancellationToken);

		var request = new CreateBookingJobRequest
		{
			EventId = Guid.NewGuid(),
			RequestId = requestId,
			ResourceId = booking.ResourceId,
			StartDate = booking.BookedFrom,
			EndDate = booking.BookedTo
		};
		var headers = new Dictionary<string, string>
		{
			{ "rbs2-content-type", "application/json" }
		};
		
		_logger.LogDebug("Отправка сообщения CreateBookingJobRequest с заголовками: {Headers}", headers);
		await _bus.Publish(request);
				
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
		if (booking.CatalogRequestId != null)
		{
			var request = new CancelBookingJobByRequestIdRequest
			{
				EventId = Guid.NewGuid(),
				RequestId = booking.CatalogRequestId.Value
			};
			await _bus.Publish(request);
		}

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