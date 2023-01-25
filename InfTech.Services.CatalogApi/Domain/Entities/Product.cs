using System.ComponentModel.DataAnnotations;

namespace InfTech.Services.CatalogApi.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        //public string Cost { get; set; }
        public string Image { get; set; }
    }
}
