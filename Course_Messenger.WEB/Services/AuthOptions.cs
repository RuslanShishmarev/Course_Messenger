using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Course_Messenger.WEB.Services;

public class AuthOptions
{
    public const string ISSUER = "MyValidIssuer";

    public const string AUDIENCE = "MyValidAudience";

    const string KEY = "mysupersecret_secretkey!987";

    public const int LIFETIME = 10;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
