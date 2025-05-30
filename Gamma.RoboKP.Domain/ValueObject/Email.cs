namespace Gamma.RoboKP.Domain.ValueObject;
//Валидацию добавлю попозже
public class Email : ValueObject
{
    public string Value { get; } = string.Empty;
    public string LocalPart => Value.Split('@')[0];
    public string DomainPart => Value.Split('@')[1];
    
    public Email(string value) => Value = value;
    
    public static Email Create(string email) => new Email(email);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
    
    public static implicit operator string(Email email) => email.Value;
    
    private Email(){}
}