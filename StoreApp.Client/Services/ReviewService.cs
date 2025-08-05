using StoreApp.Client.Models;

namespace StoreApp.Client.Services;

public class ReviewService : IReviewService
{
    public async Task<List<ReviewModel>> GetProductReviewsAsync(int productId)
    {
        // Simulating a delay for the mock data
        return Task.FromResult(Mocks.Reviews).Result;

        // return await httpClient.GetFromJsonAsync<List<ReviewModel>>($"products/{productId}/reviews");
    }

    public async Task AddProductReviewAsync(int productId, ReviewModel review)
    {
        // Simulating a delay for the mock data
        Mocks.Reviews.Add(review);

        // In a real application, you would send the review to the server here
        // await httpClient.PostAsJsonAsync($"products/{productId}/reviews", review);
    }
}