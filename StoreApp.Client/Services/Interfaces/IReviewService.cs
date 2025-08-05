using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public interface IReviewService
{
    Task<List<ReviewModel>> GetProductReviewsAsync(int productId);

    Task AddProductReviewAsync(int productId, ReviewModel review);
}