namespace BookingService.Booking.Api.Contracts.Booking.Requests;

public record GetBookingsByFilterRequest(long? UserId = null, long? RecourceId = null, int PageNumber = 1, int PageSize = 25);
