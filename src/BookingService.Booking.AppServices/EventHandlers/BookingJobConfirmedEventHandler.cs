using BookingService.Booking.Domain.Bookings;
using BookingService.Catalog.Async.Api.Contracts.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace BookingService.Booking.AppServices.EventHandlers;

public class BookingJobConfirmedEventHandler : IHandleMessages<BookingJobConfirmed>
{
	private readonly IBookingsBackgroundQueries _bookingBackgroundQueries;
	private readonly ILogger<BookingJobConfirmedEventHandler> _logger;

	public BookingJobConfirmedEventHandler(IBookingsBackgroundQueries bookingBackgroundQueries,
		ILogger<BookingJobConfirmedEventHandler> logger)
	{
		_bookingBackgroundQueries = bookingBackgroundQueries;
		_logger = logger;
	}

	public async Task Handle(BookingJobConfirmed message)
	{
		_logger.LogInformation("Обработка события BookingJobConfirmed: {RequestId}", message.RequestId);
		var booking = await _bookingBackgroundQueries.GetBookingByRequestId(message.RequestId);
		if (booking == null)
		{
			_logger.LogWarning($"Бронирование с RequestId {message.RequestId} не найдено.");
			return;
		}

		_logger.LogInformation("Бронирование найдено: {BookingId}", booking.Id);
		booking.Confirm();
		_logger.LogInformation($"Бронирование с RequestId {message.RequestId} было подтверждено.");
	}
}

