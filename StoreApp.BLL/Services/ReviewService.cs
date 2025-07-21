using AutoMapper;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Repositories.Interfaces;
using StoreApp.Shared.Interfaces.Services;
using StoreApp.Shared.Models;

namespace StoreApp.BLL.Services;

public class ReviewService(IReviewRepository repository, IMapper mapper) : IReviewService
{
    public async Task<IEnumerable<ReviewModel>> GetReviewsByProductIdAsync(int productId)
    {
        var reviews = await repository.GetReviewsByProductIdAsync(productId);
        return mapper.Map<IEnumerable<ReviewModel>>(reviews);
    }

    public async Task<ReviewModel?> GetReviewByIdAsync(int reviewId)
    {
        var review = await repository.GetByIdAsync(reviewId);
        return review == null ? null : mapper.Map<ReviewModel>(review);
    }

    public async Task AddReviewAsync(ReviewModel reviewModel)
    {
        var reviewEntity = mapper.Map<ReviewEntity>(reviewModel);
        await repository.AddAsync(reviewEntity);
    }

    public async Task UpdateReviewAsync(ReviewModel reviewModel)
    {
        var reviewEntity = mapper.Map<ReviewEntity>(reviewModel);
        await repository.UpdateAsync(reviewEntity);
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        await repository.DeleteAsync(reviewId);
    }

    public async Task<bool> UserHasReviewedProductAsync(int userId, int productId)
    {
        var review = await repository.GetReviewByUserIdAndProductIdAsync(userId, productId);
        return review != null;
    }
}