using BookingService.Booking.Api.Contracts;
using BookingService.Booking.Api.Contracts.Bookings.DTOs;
using BookingService.Booking.Api.Contracts.Bookings.Requests;
using BookingService.Booking.AppServices.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.AppServices.Contracts.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Booking.Host.Controllers;

[ApiController]
[Route(WebRoutes.Booking.Path)]
public class BookingController : ControllerBase
{
  private readonly IBookingsService _bookingsService;
  private readonly IBookingsQueries _bookingsQueries;

  public BookingController(IBookingsService bookingsService, IBookingsQueries bookingsQueries)
  {
    _bookingsService = bookingsService;
    _bookingsQueries = bookingsQueries;
  }

  [HttpPost]
  public Task<long> Create([FromBody] CreateBookingRequest request, CancellationToken cancellationToken = default)
  {
    return _bookingsService.Create(request.Id, request.UserId, request.ResourceId, request.StartDate, request.EndDate,
      cancellationToken);
  }

  [HttpGet(WebRoutes.Booking.GetById)]
  public Task<BookingData> GetBooking([FromRoute] long id, CancellationToken cancellationToken = default)
  {
    return _bookingsService.GetById(id, cancellationToken);
  }

  [HttpPost(WebRoutes.Booking.GetByFilter)]
  public Task<BookingData[]> GetBookings([FromBody] GetBookingsByFilterRequest request,
    CancellationToken cancellationToken = default)
  {
    return _bookingsQueries.GetByFilter(request.UserId, request.ResourceId, request.PageNumber, request.PageSize,
      cancellationToken);
  }

  [HttpGet(WebRoutes.Booking.GetStatusById)]
  public Task<BookingStatus?> GetStatus([FromRoute] long id, CancellationToken cancellationToken = default)
  {
    return _bookingsQueries.GetStatusById(id, cancellationToken);
  }

  [HttpPost($"{WebRoutes.Booking.Cancel}")]
  public Task Cancel(long id, CancellationToken cancellationToken = default)
  {
    return _bookingsService.Cancel(id, cancellationToken);
  }
}