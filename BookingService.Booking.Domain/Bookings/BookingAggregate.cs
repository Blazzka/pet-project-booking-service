using BookingService.Booking.AppServices.Exceptions;
using BookingService.Booking.Domain.Contracts.Bookings;

namespace BookingService.Booking.Domain.Booking;


public class BookingAggregate
{
	public readonly IAsyncEnumerable<object>? CreatedAtDateTime;

	public long Id { get; set; }
	public BookingStatus Status { get; set;}
	public long UserId { get; }
	public long ResourceId { get; }
	public DateOnly BookedFrom { get; }
	public DateOnly BookedTo { get; }
	public DateTimeOffset CreatedAt { get; }

	public BookingAggregate(long id, BookingStatus status,long userId, long resourceId, DateOnly bookedFrom, DateOnly bookedTo, DateTimeOffset createdAt)
	{
		Id = id;
		Status = status;
		UserId = userId;
		ResourceId = resourceId;
		BookedFrom = bookedFrom;
		BookedTo = bookedTo;
		CreatedAt = createdAt;
	}
	public static BookingAggregate Initialize(long id,BookingStatus status, long userId, long recourceId, DateOnly bookedFrom, DateOnly bookedTo, DateTimeOffset createdAt)
	{
		if (id > 0)
		{
			throw new ValidationException($"Некорректный идентификатор {id}");
		}
		if (userId <= 0)
		{
			throw new ValidationException($"Некорректный идентификатор пользователя {userId}");
		}
		if (recourceId <= 0)
		{
			throw new ValidationException($"Некорректный идентификатор ресурса {userId}");
		}
		if (bookedFrom <= DateOnly.FromDateTime(DateTime.Now))
		{
			throw new ValidationException($"Дата начала бронирования должна быть больше текущей даты");
		}
		if (bookedTo < bookedFrom)
		{
			throw new ValidationException("Выбранная дата окончания бронирования раньше даты начала бронирования");
		}
		if (createdAt != DateTimeOffset.Now)
		{
			throw new ValidationException("Текущее время отличается от времени бронирования");
		}

		return new BookingAggregate(id, status, userId, recourceId, bookedFrom, bookedTo, createdAt);
	}
	public void Confirm()
	{
		if (Status != BookingStatus.AwaitConfirmation)
		{
			throw new ValidationException($"Статус заявки некорректен, заявка должна быть в статусе {BookingStatus.AwaitConfirmation}");
		}

		Status = BookingStatus.Confirmed;
	}

	public void Cancel()
	{
		if (Status != BookingStatus.AwaitConfirmation)
		{
			throw new ValidationException($"Статус заявки некорректен, заявка должна быть в статусе {BookingStatus.AwaitConfirmation}");
		}
		Status = BookingStatus.Cancelled;
	}
}