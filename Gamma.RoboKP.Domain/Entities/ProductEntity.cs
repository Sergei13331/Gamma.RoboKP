namespace Gamma.RoboKP.Domain.Entities;

public class ProductEntity : BaseEntity<long>
{ 
    public ProductEntity()
    {
    }
    
    private ProductEntity(string name, string description, decimal price, string image)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = image;
    }
    
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal? Price { get; private set; }
    public string? ImageUrl { get; private set; }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void ChangePrice(decimal price)
    {
        Price = price;
    }

    public void ChangeImage(string image)
    {
        ImageUrl = image;
    }

    public ProductEntity Create(string name, string description, decimal price, string image)
    {
        return new ProductEntity(name, description, price, image);
    }
}