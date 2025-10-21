namespace WpfAppAPI_1.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Category Category { get; set; } = new();
    public Shop Shop { get; set; } = new();
}


