using BookingService.Booking.Domain;
using BookingService.Booking.Domain.Bookings;
using BookingService.Catalog.Async.Api.Contracts.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace BookingService.Booking.AppServices.EventHandlers;

public class BookingJobConfirmedEventHandler : IHandleMessages<BookingJobConfirmed>
{
	private readonly IBookingsBackgroundQueries _bookingBackgroundQueries;
	private readonly ILogger<BookingJobConfirmedEventHandler> _logger;
	private readonly IUnitOfWork _unitOfWork;

	public BookingJobConfirmedEventHandler(IBookingsBackgroundQueries bookingBackgroundQueries,
		ILogger<BookingJobConfirmedEventHandler> logger, IUnitOfWork unitOfWork)
	{
		_bookingBackgroundQueries = bookingBackgroundQueries;
		_logger = logger;
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(BookingJobConfirmed message)
	{
		_logger.LogDebug("Обработка события BookingJobConfirmed: {RequestId}", message.RequestId);
		var booking = await _bookingBackgroundQueries.GetBookingByRequestId(message.RequestId);
		if (booking == null)
		{
			_logger.LogWarning("Бронирование с RequestId {RequestId} не найдено.", message.RequestId);
			return;
		}

		_logger.LogInformation("Бронирование найдено: {BookingId}", booking.Id);
		booking.Confirm();

		_unitOfWork.BookingsRepository.Update(booking);
		await _unitOfWork.CommitAsync();
		_logger.LogDebug("Бронирование с RequestId {RequestId} было подтверждено.", message.RequestId);
	}
}