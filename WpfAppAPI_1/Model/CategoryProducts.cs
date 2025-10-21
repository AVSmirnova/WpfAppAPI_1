namespace WpfAppAPI_1.Models;

public class CategoryProducts
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<int> ProductsIds { get; set; } = new();
}


