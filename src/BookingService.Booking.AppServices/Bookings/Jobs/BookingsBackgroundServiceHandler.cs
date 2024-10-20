using BookingService.Booking.AppServices.Dates;
using BookingService.Booking.Domain;
using BookingService.Booking.Domain.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Catalog.Api.Contracts.BookingJobs;
using BookingService.Catalog.Api.Contracts.BookingJobs.Queries;
using Microsoft.Extensions.Logging;

namespace BookingService.Booking.AppServices.Bookings.Jobs;

public class BookingsBackgroundServiceHandler : IBookingsBackgroundServiceHandler
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IBookingsBackgroundQueries _bookingsBackgroundQueries;
	private readonly IBookingJobsController _bookingJobsController;
	private readonly ILogger<BookingsBackgroundServiceHandler> _logger;
	private readonly ICurrentDateTimeProvider _dateTimeProvider;

	public BookingsBackgroundServiceHandler(
		IUnitOfWork unitOfWork,
		IBookingsBackgroundQueries bookingsBackgroundQueries,
		IBookingJobsController bookingJobsController,
		ILogger<BookingsBackgroundServiceHandler> logger,
		ICurrentDateTimeProvider dateTimeProvider
	)
	{
		_unitOfWork = unitOfWork;
		_bookingsBackgroundQueries = bookingsBackgroundQueries;
		_bookingJobsController = bookingJobsController;
		_logger = logger;
		_dateTimeProvider = dateTimeProvider;
	}

	public async Task Handle(CancellationToken cancellationToken = default)
	{
		var bookings = _bookingsBackgroundQueries.GetConfirmationAwaitingBookings();
		foreach (var booking in bookings)
		{
			if (booking.Status != (BookingStatus)1)
			{
				_logger.LogWarning($"Некорректное состояние агрегата BookingId: {booking.Id}");
				continue;
			}

			var query = new GetBookingJobStatusByRequestIdQuery
			{
				RequestId = booking.CatalogRequestId.Value
			};
			var bookingJobStatus = await _bookingJobsController.GetBookingJobStatusByRequestId(query, cancellationToken);

			switch (bookingJobStatus)
			{
				case BookingJobStatus.Confirmed:
					booking.Confirm();
					break;

				case BookingJobStatus.Cancelled:
					booking.Cancel(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.DateTime));
					break;

				default:
					_logger.LogWarning(
						$"Бронирование с Id: {booking.Id} имеет статус необработанного бронирования: {bookingJobStatus}.");
					break;
			}

			_unitOfWork.BookingsRepository.Update(booking);
			await _unitOfWork.CommitAsync(cancellationToken);
		}
	}
}