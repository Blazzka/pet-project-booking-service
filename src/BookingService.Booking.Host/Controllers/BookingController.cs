using Microsoft.AspNetCore.Mvc;
using BookingService.Booking.AppServices.Booking;
using BookingService.Booking.Api.Contracts.Booking;
using BookingService.Booking.Api.Contracts.Booking.Requests;
using BookingService.Booking.Api.Contracts.Booking.Dtos;
using BookingService.Booking.Api.Contracts;

namespace BookingService.Booking.Host.Controllers;

[ApiController]
[Route(WebRoutes.Booking.Path)]
public class BookingController : ControllerBase
{
	private IBookingsService _bookingsService;
	private IBookingsQueries _bookingsQueries;

	public BookingController(IBookingsService bookingsService, IBookingsQueries bookingsQueries)
	{
		_bookingsService = bookingsService;
		_bookingsQueries = bookingsQueries;
	}

	[HttpPost]
	public Task<long> Create([FromBody] CreateBookingRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsService.Create(request.Id, request.ResourceId, request.StartDate, request.EndDate, cancellationToken);
	}

	[HttpGet(WebRoutes.Booking.GetById)]
	public Task<BookingData> GetBooking([FromRoute] long id, CancellationToken cancellationToken = default)
	{
		return _bookingsService.GetById(id, cancellationToken);
	}

	[HttpPost(WebRoutes.Booking.GetByFilter)]
	public Task<BookingData[]> GetBookings([FromBody] GetBookingsByFilterRequest request, CancellationToken cancellationToken = default)
	{
		return _bookingsQueries.GetByFilter(request.UserId, request.RecourceId, request.PageNumber, request.PageSize, cancellationToken);
	}

	[HttpGet(WebRoutes.Booking.GetStatusById)]
	public Task<BookingStatus> GetStatus([FromRoute] long id, CancellationToken cancellationToken = default)
	{
		return _bookingsQueries.GetStatusById(id, cancellationToken);
	}

	[HttpPost($"{WebRoutes.Booking.Cancel}")]
	public Task Cancel(long id, CancellationToken cancellationToken = default)
	{
		return _bookingsService.Cancel(id, cancellationToken);
	}
}





