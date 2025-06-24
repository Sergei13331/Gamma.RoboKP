namespace Gamma.RoboKP.Domain.Entities;

public class DiscountEntity
{
    public DiscountEntity() {}
    public long Percent { get; private set; }

    public double GetMultiplier()
    {
        return 1d - (Percent / 100d);
    }
}