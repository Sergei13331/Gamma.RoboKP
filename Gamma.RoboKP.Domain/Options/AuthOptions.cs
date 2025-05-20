namespace Gamma.RoboKP.Domain.Options;

public class AuthOptions
{
    public required string TokenPrivateKey { get; set; }
    public int ExpireMinutes { get; set; }
}