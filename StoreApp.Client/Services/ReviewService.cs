using System.Net.Http.Json;
using StoreApp.Client.Models;
using SharedReviewModel = StoreApp.Shared.Models.ReviewModel;

namespace StoreApp.Client.Services;

public class ReviewService(HttpClient httpClient) : IReviewService
{
    public async Task<List<ReviewModel>> GetProductReviewsAsync(int productId)
    {
        var apiReviews = await httpClient.GetFromJsonAsync<List<SharedReviewModel>>($"review/by-product/{productId}") ?? new();
        return apiReviews.Select(r => new ReviewModel
        {
            Rating = r.Rating,
            Comment = r.Comment ?? string.Empty,
            UserName = r.UserName,
            CreatedAt = r.CreatedAt
        }).ToList();
    }

    public async Task AddProductReviewAsync(int productId, ReviewModel review)
    {
        await httpClient.PostAsJsonAsync("review", new
        {
            productId = productId,
            comment = review.Comment,
            rating = review.Rating
        });
    }
}