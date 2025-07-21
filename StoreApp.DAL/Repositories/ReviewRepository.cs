using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.Data;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Repositories.Interfaces;

namespace StoreApp.DAL.Repositories;

public class ReviewRepository(AppDbContext context) : CrudRepository<ReviewEntity>(context), IReviewRepository
{
    public async Task<IEnumerable<ReviewEntity>> GetReviewsByProductIdAsync(int productId)
    {
        return await GetQueryable()
            .Where(r => r.ProductId == productId)
            .ToListAsync();
    }
    
    protected override IQueryable<ReviewEntity> GetQueryable()
    {
        return Context.Reviews
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Product);
    }
}