using System.ComponentModel.DataAnnotations;

namespace StoreApp.Shared.Models;

public class ProductModel
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(400)]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public required decimal Price { get; set; }

    [MaxLength(4 * 1024 * 1024, ErrorMessage = "Image size should not exceed 4MB")]
    public byte[]? ImageData { get; set; }

    public string? ImageUrl { get; set; }
}
