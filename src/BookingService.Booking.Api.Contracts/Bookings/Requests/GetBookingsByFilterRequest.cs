﻿namespace BookingService.Booking.Api.Contracts.Bookings.Requests;

public record GetBookingsByFilterRequest(
  long? UserId = null,
  long? ResourceId = null,
  int PageNumber = 1,
  int PageSize = 25);