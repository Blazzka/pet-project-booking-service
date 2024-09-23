using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Api.Contracts.Booking.Dtos;


namespace BookingService.Booking.AppServices.Booking;

public interface IBookingsQueries
{
  public Task<BookingData[]> GetByFilter(long? userId = null, long? resourceId = null, int pageNumber = 1,
    int pageSize = 25, CancellationToken cancellationToken = default);

  public Task<BookingStatus> GetStatusById(long id, CancellationToken cancellationToken = default);
}