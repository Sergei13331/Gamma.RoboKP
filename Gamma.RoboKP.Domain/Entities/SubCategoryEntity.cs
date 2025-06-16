namespace Gamma.RoboKP.Domain.Entities;

public class SubCategoryEntity : BaseEntity<long>
{
    public SubCategoryEntity()
    {
    }
    private SubCategoryEntity(string name, long parentCategoryId)
    {
        Name = name;
        ParentCategoryId = parentCategoryId;
    }
    
    public string Name { get; private set; }
    public long ParentCategoryId { get; private set; }

    public static SubCategoryEntity Create(string name, long parentCategoryId)
    {
        return new SubCategoryEntity(name, parentCategoryId);
    }
    
}