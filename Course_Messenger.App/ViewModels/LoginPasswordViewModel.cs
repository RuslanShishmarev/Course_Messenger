namespace Course_Messenger.App.ViewModels;

internal class LoginPasswordViewModel : BindableObject
{
    private string _login;
    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(CanSignIn));
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(CanSignIn));
        }
    }

    public bool CanSignIn => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
}
