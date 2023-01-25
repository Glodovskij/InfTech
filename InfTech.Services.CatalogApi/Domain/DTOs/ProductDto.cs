namespace InfTech.Services.CatalogApi.Domain.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
