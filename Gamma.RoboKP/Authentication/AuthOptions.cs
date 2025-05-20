using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gamma.RoboKP.Authentication;

public class AuthOptions
{
    public const string Auth = "Auth";

    public string ISSUER { get; set; }
    public string AUDIENCE { get; set; }
    public string KEY { get; set; }
    public SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}