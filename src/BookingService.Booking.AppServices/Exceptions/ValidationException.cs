namespace BookingService.Booking.AppServices.Exceptions;

public class ValidationException : Exception
{
	public ValidationException(string message, Exception inner) : base(message, inner) 
	{
	}
	public ValidationException(string message) : base(message)
	{
	}
}