namespace BookingService.Booking.Api.Contracts.Bookings.Requests;

public record CreateBookingRequest(long UserId, long ResourceId, DateOnly BookedFrom, DateOnly BookedTo);