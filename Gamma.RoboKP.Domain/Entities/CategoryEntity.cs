namespace Gamma.RoboKP.Domain.Entities;

public class CategoryEntity : BaseEntity<long>
{
    public CategoryEntity()
    {
    }
    
    private CategoryEntity(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    public List<SubCategoryEntity> SubCategories { get; private set; }

    public static CategoryEntity Create(string name)
    {
        return new CategoryEntity(name);
    }
    
}