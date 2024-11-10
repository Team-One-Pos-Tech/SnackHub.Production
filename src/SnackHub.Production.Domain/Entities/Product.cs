using SnackHub.Domain.Base;

namespace SnackHub.Domain.Entities
{
    public class Product : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public List<string> Images { get; private set; }

        public Product(string name, Category category, decimal price, string description, List<string> images, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            Images = images ?? new List<string>();
        }

        public void Edit(string name, Category category, decimal price, string description, List<string> images)
        {
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            Images = images ?? new List<string>();
        }
    }
}
