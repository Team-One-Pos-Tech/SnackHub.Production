namespace SnackHub.Production.Domain.Entities;

public record Product
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Edit(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    private Product() { }
    
    public static Product Create(Guid id, string name, string description)
        => new Product
        {
            Id = id,
            Name = name,
            Description = description,
        };
}