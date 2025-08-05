using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StoreApp.Shared.Dtos.Review;

public record CreateReview
{
    [JsonPropertyName("productId")]
    [Required]
    public int ProductId;

    [JsonPropertyName("comment")]
    public string? Comment;

    [JsonPropertyName("rating")]
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating;
}