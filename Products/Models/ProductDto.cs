namespace Products.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }

        public class ProductInsertDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
