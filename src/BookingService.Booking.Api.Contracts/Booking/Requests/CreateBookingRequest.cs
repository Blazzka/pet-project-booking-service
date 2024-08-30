namespace BookingService.Booking.Api.Contracts.Booking.Requests;

public record CreateBookingRequest(long Id, long ResourceId, DateOnly StartDate, DateOnly EndDate);