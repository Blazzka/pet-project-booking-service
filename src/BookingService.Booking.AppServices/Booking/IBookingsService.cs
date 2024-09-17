using BookingService.Booking.Api.Contracts.Booking.Dtos;

namespace BookingService.Booking.AppServices.Booking;

public interface IBookingsService
{
    public Task<long> Create(long id, long resourceId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken = default);

    public Task<BookingData> GetById(long id, CancellationToken cancellationToken = default);
    public Task Cancel(long id, CancellationToken cancellationToken = default);
}