using Microsoft.EntityFrameworkCore;

namespace ShortLink.Api.Repositories;

public class ShortLinkRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task AddAsync(UrlMapper model)
    {
        await _context.UrlMappers.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<UrlMapper?> GetByGuidAsync(string guid)
    {
        return await _context.UrlMappers.SingleOrDefaultAsync(e => e.Guid == guid);
    }

    public async Task<IEnumerable<UrlMapper>> GetAllAsync(int offset = 0, int limit = 9)
    {
        return await _context.UrlMappers.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task UpdateAsync(UrlMapper model)
    {
        _context.UrlMappers.Update(model);
        await _context.SaveChangesAsync();
    }
}
