namespace StoreApp.Client.Models;

public class ReviewModel
{
    public double Rating { get; set; } = 0.0;

    public string Comment { get; set; } = String.Empty;
    
    public string UserName { get; set; } = String.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}