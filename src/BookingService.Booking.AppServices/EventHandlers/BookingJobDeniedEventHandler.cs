using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.Domain;
using BookingService.Booking.Domain.Bookings;
using BookingService.Catalog.Async.Api.Contracts.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace BookingService.Booking.AppServices.EventHandlers
{
	public class BookingJobDeniedEventHandler : IHandleMessages<BookingJobDenied>
	{
		private readonly IBookingsBackgroundQueries _bookingBackgroundQueries;
		private readonly ILogger<BookingJobDeniedEventHandler> _logger;
		private readonly ICurrentDateTimeProvider _dateTimeProvider;
		private readonly IUnitOfWork _unitOfWork;

		public BookingJobDeniedEventHandler(IBookingsBackgroundQueries bookingBackgroundQueries,
			ILogger<BookingJobDeniedEventHandler> logger, ICurrentDateTimeProvider dateTimeProvider, IUnitOfWork unitOfWork)
		{
			_bookingBackgroundQueries = bookingBackgroundQueries;
			_logger = logger;
			_dateTimeProvider = dateTimeProvider;
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(BookingJobDenied message)
		{
			var booking = await _bookingBackgroundQueries.GetBookingByRequestId(message.RequestId);
			if (booking == null)
			{
				_logger.LogWarning("Бронирование с RequestId {RequestId} не найдено.", message.RequestId);
				return;
			}

			booking.Cancel(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.DateTime));

			_unitOfWork.BookingsRepository.Update(booking);
			await _unitOfWork.CommitAsync();
			_logger.LogInformation("Бронирование с RequestId {RequestId} было отменено.", message.RequestId);
		}
	}
}