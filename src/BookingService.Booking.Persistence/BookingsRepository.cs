using BookingService.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Booking.Persistence;

public class BookingsRepository : IBookingsRepository
{
	private DbSet<BookingAggregate> _dbSet;

	public BookingsRepository(BookingsContext context)
	{
		_dbSet = context.Bookings;
	}

	public void Create(BookingAggregate aggregate)
	{
		_dbSet.Add(aggregate);
	}

	public ValueTask<BookingAggregate?> GetById(long id, CancellationToken cancellationToken = default)
	{
		return _dbSet.FindAsync(new object[] { id }, cancellationToken);
	}

	public void Update(BookingAggregate aggregate)
	{
		_dbSet.Attach(aggregate);
		_dbSet.Entry(aggregate).State = EntityState.Modified;
	}
}