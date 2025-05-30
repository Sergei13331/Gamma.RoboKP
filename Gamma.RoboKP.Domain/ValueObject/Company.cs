namespace Gamma.RoboKP.Domain.ValueObject;

public class Company : ValueObject
{
    public Company(string companyName, string kpp, string inn, string ogrn, string address)
    {
        CompanyName = companyName;
        Kpp = kpp;
        Inn = inn;
        Ogrn = ogrn;
        Address = address;
    }
    
    public string CompanyName { get; private set; }
    public string Kpp { get; private set; }
    public string Inn { get; private set; }
    public string Ogrn { get; private set; }
    public string Address { get; private set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CompanyName.ToLowerInvariant();
        yield return Kpp;
        yield return Inn;
        yield return Ogrn;
        yield return Address.ToLowerInvariant();
    }
}