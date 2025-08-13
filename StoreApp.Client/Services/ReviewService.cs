using System.Net.Http.Json;
using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public class ReviewService(HttpClient httpClient) : IReviewService
{
    public async Task<List<ReviewModel>> GetProductReviewsAsync(int productId)
    {
        var apiReviews = await httpClient.GetFromJsonAsync<List<ApiReview>>($"review/by-product/{productId}") ?? new();
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

    private sealed class ApiReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}