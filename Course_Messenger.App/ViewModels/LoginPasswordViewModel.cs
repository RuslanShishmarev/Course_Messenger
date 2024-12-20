namespace Course_Messenger.App.ViewModels;

internal class LoginPasswordViewModel : BindableObject
{
    private string _login = string.Empty;
    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
}
