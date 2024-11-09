using BookingService.Booking.Domain.Contracts.Bookings;
using BookingService.Booking.Domain.Exceptions;

namespace BookingService.Booking.Domain.Bookings;

public class BookingAggregate
{
	public Guid? CatalogRequestId { get; private set; }
	public long Id { get; set; }
	public BookingStatus Status { get; private set; }
	public long UserId { get; }
	public long ResourceId { get; }
	public DateOnly BookedFrom { get; }
	public DateOnly BookedTo { get; }
	public DateTimeOffset CreatedAt { get; }

	private BookingAggregate(long userId, long resourceId, DateOnly bookedFrom, DateOnly bookedTo,
		DateTimeOffset createdAt)
	{
		Status = BookingStatus.AwaitConfirmation;
		UserId = userId;
		ResourceId = resourceId;
		BookedFrom = bookedFrom;
		BookedTo = bookedTo;
		CreatedAt = createdAt;
	}

	public void SetCatalogRequestId(Guid catalogRequestId)
	{
		if (CatalogRequestId != null) throw new DomainException($"CatalogRequestId уже имеет значение: {CatalogRequestId}");

		if (catalogRequestId == default) throw new DomainException($"CatalogRequestId не инициилизирован: {CatalogRequestId}");
		CatalogRequestId = catalogRequestId;
	}
	
	public static BookingAggregate Initialize(long userId, long resourceId, DateOnly bookedFrom,
		DateOnly bookedTo, DateTimeOffset createdAt)
	{
		if (userId <= 0)
			throw new DomainException($"Некорректный идентификатор пользователя {userId}");
		if (resourceId <= 0)
			throw new DomainException($"Некорректный идентификатор ресурса {userId}");
		if (bookedFrom <= DateOnly.FromDateTime(createdAt.Date))
			throw new DomainException("Дата начала бронирования должна быть больше текущей даты");
		if (bookedTo < bookedFrom)
			throw new DomainException("Выбранная дата окончания бронирования раньше даты начала бронирования");
		if (createdAt.Date != DateTimeOffset.UtcNow.Date)
			throw new DomainException("Дата создания бронирования должна быть равна текущему времени");

		return new BookingAggregate(userId, resourceId, bookedFrom, bookedTo, createdAt);
	}

	public void Confirm()
	{
		if (Status != BookingStatus.AwaitConfirmation)
			throw new DomainException(
				$"Статус заявки некорректен, заявка должна быть в статусе {BookingStatus.AwaitConfirmation}");

		Status = BookingStatus.Confirmed;
	}

	public void Cancel(DateOnly currentDate)
	{
		switch (Status)
		{
			case BookingStatus.AwaitConfirmation:
				Status = BookingStatus.Cancelled;
				return;
			case BookingStatus.Confirmed when currentDate < BookedFrom:
				Status = BookingStatus.Cancelled;
				return;
			case BookingStatus.Confirmed:
				throw new DomainException("Невозможно отменить начавшееся бронирование");
			case BookingStatus.None:
			case BookingStatus.Cancelled:
			default:
				throw new DomainException("Некорректный статус для отмены");
		}
	}
}