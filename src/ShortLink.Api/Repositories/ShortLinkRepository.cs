using Microsoft.EntityFrameworkCore;

namespace ShortLink.Api.Repositories;

public class ShortLinkRepository(ApplicationDbContext context)
{
    public async Task AddAsync(UrlMapper model)
    {
        await context.UrlMappers.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<UrlMapper?> GetByGuidAsync(string guid)
    {
        return await context.UrlMappers.SingleOrDefaultAsync(e => e.Guid == guid);
    }

    public async Task<IEnumerable<UrlMapper>> GetAllAsync(int offset = 0, int limit = 9)
    {
        return await context.UrlMappers.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task UpdateAsync(UrlMapper model)
    {
        context.UrlMappers.Update(model);
        await context.SaveChangesAsync();
    }
}
