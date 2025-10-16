namespace ProductService.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Private constructor for EF Core
        private Product() { }

        public Product(string name, string description, decimal price, int stock)
        {
            ValidateProduct(name, price, stock);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description, decimal price, int stock)
        {
            ValidateProduct(name, price, stock);

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            UpdatedAt = DateTime.UtcNow;
        }

        // Business rule validation
        private void ValidateProduct(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del producto es requerido.");

            if (name.Length > 120)
                throw new ArgumentException("El nombre del producto no puede exceder los 120 caracteres.");

            if (price <= 0)
                throw new ArgumentException("El precio debe de ser mayor a cero.");

            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
        }

        public void ReduceStock(int quality)
        {
            if (quality > Stock)
                throw new ArgumentException("Stock insuficiente.");

            Stock -= quality;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncreaseStock(int quality)
        {
            if (quality <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            Stock += quality;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
