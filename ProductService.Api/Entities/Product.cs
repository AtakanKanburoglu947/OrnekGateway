
namespace ProductService.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
  
    }
}
