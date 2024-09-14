﻿using BookingService.Booking.Domain.Booking;
using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Domain.Exceptions;
using Xunit;

namespace BookingService.Booking.Domain.UnitTests;


public class BookingAggregateTests
{
	private readonly long Id = 1;
	private readonly BookingStatus _status;
	private readonly long UserId = 1;
	private const long ResourceId = 1;
	private readonly DateOnly _bookedFrom = new(2024, 10, 10);
	private readonly DateOnly _bookedTo = new(2024, 10, 12);
	private readonly DateTimeOffset _createdAt = DateTimeOffset.Now;
	
	[Fact]
	public void Initialize_valid_parameters_creates_inctance_with_correct_properties()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);

		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.AwaitConfirmation, bookingAggregate.Status);
		Assert.Equal(UserId, bookingAggregate.UserId);
		Assert.Equal(ResourceId, bookingAggregate.ResourceId);
		Assert.Equal(_bookedFrom, bookingAggregate.BookedFrom);
		Assert.Equal(_bookedTo, bookingAggregate.BookedTo);
		Assert.Equal(_createdAt, bookingAggregate.CreatedAt);
	}

	[Fact]
	public void Initialize_invalid_parameters_should_throw_DE()
	{
		var _bookedFrom = new DateOnly(2024, 10, 12);
		var _bookedTo = new DateOnly(2024, 10, 10);
		Assert.Throws<DomainException>(() => BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt));
	}

	[Fact]
	public void Confirm_valid_parameters_change_status_of_BookingAggregate_to_confirmed()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Confirm();
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Confirmed, bookingAggregate.Status);
	}

	[Fact]
	public void Cancel_valid_parameters_change_status_of_BookingAggregate_to_cancel()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Cancel(DateOnly.FromDateTime(DateTime.Now));
		Assert.NotNull(bookingAggregate);
		Assert.Equal(BookingStatus.Cancelled, bookingAggregate.Status);
	}
	[Fact]
	public void Confirm_invalid_parameters_should_throw_DE()
	{
		var bookingAggregate = BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Cancel(DateOnly.FromDateTime(DateTime.Now));

		Assert.Throws<DomainException>(() => bookingAggregate.Confirm());
	}
	[Fact]
	public void Cancel_invalid_parameters_should_throw_DE()
	{
		var currentDate = new DateOnly(2024, 11, 12);
		var bookingAggregate = BookingAggregate.Initialize(Id, UserId, ResourceId, _bookedFrom, _bookedTo, _createdAt);
		bookingAggregate.Confirm();

		Assert.Throws<DomainException>(() => bookingAggregate.Cancel(currentDate));
	}
}