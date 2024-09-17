﻿namespace BookingService.Booking.Domain.Exceptions;

public class DomainException : Exception
{
  public DomainException(string message, Exception inner) : base(message, inner)
  {
  }

  public DomainException(string message) : base(message)
  {
  }
}