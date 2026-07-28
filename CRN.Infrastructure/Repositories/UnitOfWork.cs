using CRN.Infrastructure.Data;

namespace CRN.Infrastructure.Repositories;

public class UnitOfWork
{
    private readonly ApplicationDbContext _context;

    public ProductRepository Products { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
