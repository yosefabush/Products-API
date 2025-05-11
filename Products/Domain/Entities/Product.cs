namespace Products.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// The current stock level for the product
        /// </summary>
        public int Stock { get; set; }
    }
}
