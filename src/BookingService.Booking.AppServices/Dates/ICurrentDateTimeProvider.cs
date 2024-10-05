namespace BookingService.Booking.AppServices.Dates;

public interface ICurrentDateTimeProvider
{
	/// <summary>
	///   Локальное время
	/// </summary>
	public DateTimeOffset Now { get; }

	/// <summary>
	///   Время в UTC
	/// </summary>
	public DateTimeOffset UtcNow { get; }
}