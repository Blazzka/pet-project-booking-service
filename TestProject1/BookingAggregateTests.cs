using BookingService.Booking.Domain.Booking;
using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Domain.Exceptions;
using Xunit;

namespace BookingService.Booking.Domain.UnitTests;


public class BookingAggregateTests
{
	private readonly long Id = 1;
	private readonly BookingStatus status;
	private readonly long UserId = 1;
	private const long ResourceId = 1;
	private readonly DateOnly _bookedFrom = new(2024, 10, 10);
	private readonly DateOnly _bookedTo = new(2024, 10, 12);
	private readonly DateTimeOffset _createdAt = DateTimeOffset.Now;

	[Fact]
	public void Initialize_new_BookingAggregate()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);

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
		var bookingAggregate = BookingAggregate.Initialize(Id, status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Confirm();
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Confirmed, bookingAggregate.Status);
	}

	[Fact]
	public void Change_status_of_BookingAggregate_to_Cancel()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Cancel();
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Cancelled, bookingAggregate.Status);
	}
	[Fact]
	public void Changing_status_of_BookingAggregate_to_confirmed_throws_DE_in_negative_case()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);

		Assert.Throws<DomainException>(() => bookingAggregate.Confirm());
	}
	[Fact]
	public void Changing_status_of_BookingAggregate_to_cancel_throws_DE_in_negative_case()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, status, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);

		Assert.Throws<DomainException>(() => bookingAggregate.Cancel());
	}
}

