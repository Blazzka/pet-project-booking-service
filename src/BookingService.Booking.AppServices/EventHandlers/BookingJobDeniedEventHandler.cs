using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.Domain.Bookings;
using BookingService.Catalog.Async.Api.Contracts.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace BookingService.Booking.AppServices.EventHandlers;

public class BookingJobDeniedEventHandler : IHandleMessages<BookingJobDenied>
{
	private readonly IBookingsBackgroundQueries _bookingBackgroundQueries;
	private readonly ILogger<BookingJobDeniedEventHandler> _logger;
	private readonly ICurrentDateTimeProvider _dateTimeProvider;

	public BookingJobDeniedEventHandler(IBookingsBackgroundQueries bookingBackgroundQueries,
		ILogger<BookingJobDeniedEventHandler> logger, ICurrentDateTimeProvider dateTimeProvider)
	{
		_bookingBackgroundQueries = bookingBackgroundQueries;
		_logger = logger;
		_dateTimeProvider = dateTimeProvider;
		}

	public async Task Handle(BookingJobDenied message)
	{
		var booking = await _bookingBackgroundQueries.GetBookingByRequestId(message.RequestId);
		if (booking == null)
		{
			_logger.LogWarning($"Бронирование с RequestId {message.RequestId} не найдено.");
			return;
		}

		booking.Cancel(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.DateTime));
		_logger.LogInformation($"Бронирование с RequestId {message.RequestId} было отменено.");
	}
}