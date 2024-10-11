using BookingService.Booking.Api.Contracts.Bookings.DTOs;
using BookingService.Booking.AppServices.Contracts.Bookings;
using BookingService.Booking.Domain.Contracts.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Persistence;

public class BookingsQueries : IBookingsQueries
{
	private readonly BookingsContext _db;

	public BookingsQueries(BookingsContext db)
	{
		_db = db;
	}

	public async Task<BookingData[]> GetByFilter(long? userId = null, long? resourceId = null, int pageNumber = 1,
		int pageSize = 25, CancellationToken cancellationToken = default)
	{
		var aggregate = _db.Bookings.AsNoTracking();
		if (userId != null) aggregate = aggregate.Where(b => b.UserId == userId);
	
		if (resourceId != null) aggregate = aggregate.Where(b => b.ResourceId == resourceId);
	
		aggregate = aggregate.Skip((pageNumber - 1) * pageSize).Take(pageSize);
	
		return await aggregate.Select(b => new BookingData
		{
			Id = b.Id,
			BookingStatus = b.Status,
			UserId = b.UserId,
			ResourceId = b.ResourceId,
			BookedFrom = b.BookedFrom,
			BookedTo = b.BookedTo,
			CreatedAt = b.CreatedAt
		}).ToArrayAsync(cancellationToken);
	}

	public async Task<BookingStatus?> GetStatusById(long id, CancellationToken cancellationToken = default)
	{
		var aggregate = await _db.Bookings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		return aggregate?.Status;
	}
}