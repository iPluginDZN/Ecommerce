using Ecommerce.Application.Categories.DTOs;
using Ecommerce.Domain;

namespace Ecommerce.Application.Products.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int Quantity { get; set; }
    public bool Active { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string ShopId { get; set; } = "";
    public string ShopName { get; set; } = "";
    public string ShopImageUrl { get; set; } = "";

    public ICollection<SubcategoryDto> Subcategories { get; set; } = [];
    public ICollection<ProductPhoto> Photos { get; set; } = [];
}
