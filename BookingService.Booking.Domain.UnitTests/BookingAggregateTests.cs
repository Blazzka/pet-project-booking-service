using BookingService.Booking.Domain.Booking;
using BookingService.Booking.Domain.Contracts.Bookings;
using Xunit;

namespace BookingService.Booking.Domain.UnitTests;


public class BookingAggregateTests
{
	private readonly long Id = 1;
	private readonly BookingStatus _status;
	private readonly long UserId = 1;
	private const long ResourceId = 1;
	private readonly DateOnly _bookedFrom = new(2024, 9, 10);
	private readonly DateOnly _bookedTo = new(2024, 9, 12);
	private readonly DateTimeOffset _createdAt = DateTimeOffset.Now;

	[Fact]
	public void Initialize_new_BookingAggregate()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, _status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);

		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.AwaitConfirmation, bookingAggregate.Status);
		Assert.Equal(UserId, bookingAggregate.UserId);
		Assert.Equal(ResourceId, bookingAggregate.ResourceId);
		Assert.Equal(_bookedFrom, bookingAggregate.BookedFrom);
		Assert.Equal(_bookedTo, bookingAggregate.BookedTo);
		Assert.Equal(_createdAt, bookingAggregate.CreatedAt);
	}

	[Fact]
	public void Change_status_of_BookingAggregate_to_confirmed()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, _status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Confirm();
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Confirmed, bookingAggregate.Status);
	}

	[Fact]
	public void Change_status_of_BookingAggregate_to_Cancel()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, _status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Cancel();
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Cancelled, bookingAggregate.Status);
	}
}
