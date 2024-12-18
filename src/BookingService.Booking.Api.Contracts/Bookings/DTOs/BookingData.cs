﻿using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Api.Contracts.Bookings.DTOs;

public class BookingData
{
  public long Id { get; set; }
  public BookingStatus BookingStatus { get; set; }
  public long UserId { get; set; }
  public long ResourceId { get; set; }
  public DateOnly BookedFrom { get; set; }
  public DateOnly BookedTo { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
}