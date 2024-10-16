﻿namespace BookingService.Booking.Api.Contracts.Bookings.Requests;

public record CreateBookingRequest(long UserId, long ResourceId, DateOnly StartDate, DateOnly EndDate);