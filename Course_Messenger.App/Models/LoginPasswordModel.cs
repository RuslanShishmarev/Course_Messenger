using System.Text;

namespace Course_Messenger.App.Models;

public class LoginPasswordModel
{
    public string Login { get; }

    public string Password { get; }

    public LoginPasswordModel(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string GetEncoded()
    {
        return Convert.ToBase64String(
            Encoding.Latin1.GetBytes($"{Login}:{Password}")
        );
    }
}
