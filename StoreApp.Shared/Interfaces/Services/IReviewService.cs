using StoreApp.Shared.Models;

namespace StoreApp.Shared.Interfaces.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewModel>> GetReviewsByProductIdAsync(int productId);
    
    Task<ReviewModel?> GetReviewByIdAsync(int reviewId);
    
    Task AddReviewAsync(ReviewModel reviewModel);
    
    Task UpdateReviewAsync(ReviewModel reviewModel);
    
    Task DeleteReviewAsync(int reviewId);
    
    Task<bool> UserHasReviewedProductAsync(int userId, int productId);
}