using Microsoft.EntityFrameworkCore;
using TransportService.Domain;

namespace TransportService.Infrastructure;

public interface ITransportRepository
{
    Task AddAsync(Transport t);
    Task<Transport?> GetAsync(Guid id);
}

public class TransportRepository : ITransportRepository
{
    private readonly AppDbContext _db;
    public TransportRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Transport t) => await _db.Transports.AddAsync(t);

    public async Task<Transport?> GetAsync(Guid id) => await _db.Transports.FirstOrDefaultAsync(x => x.Id == id);
}

public interface IUnitOfWork { Task SaveChangesAsync(); }
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db) => _db = db;
    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}
