using Microsoft.AspNetCore.Mvc;
using BookingService.Bookings.AppServices.Booking;
using BookingService.Bookings.Api.Contracts.Booking.Dtos;
using Microsoft.AspNetCore.SignalR;
using BookingService.Bookings.Api.Contracts.Booking.Requests;
using BookingService.Bookings.Api.Contracts.Booking;

namespace BookingService.Bookings.Host.Conrollers;

[ApiController]
public class BookingController : ControllerBase
{
	private IBookingsService _bookingsService;
	public IBookingsQueries _bookingsQueries;

	public BookingController(IBookingsService bookingsService, IBookingsQueries bookingsQueries)
	{
		_bookingsService = bookingsService;
		_bookingsQueries = bookingsQueries;
	}

	[HttpPost($"{WebRoutes.Requests.Create}")]
	public Task<long> Create([FromBody] CreateBookingRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsService.Create(request.BookingId, request.ResourceId, request.StartDate, request.EndDate);
	}

	[HttpGet($"{WebRoutes.Requests.GetById}")]
	public Task<BookingData> GetBooking(GetBookingsByFilterRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsService.GetById(request.BookingId);
	}

	[HttpGet($"{WebRoutes.BasePath}")]
	public Task<BookingData[]> GetBookings(GetBookingsByFilterRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsQueries.GetByFilter();
	}

	[HttpGet($"{WebRoutes.Requests.GetStatusById}")]
	public Task<BookingStatus> GetStatus(GetBookingsByFilterRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsQueries.GetStatusById();
	}

	[HttpPost($"{WebRoutes.Requests.Cancel}")]
	public Task Cancel(long userId)
	{
		return _bookingsService.Cancel(userId);
	}
}





