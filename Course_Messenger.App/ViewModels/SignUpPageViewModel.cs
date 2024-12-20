using CommunityToolkit.Mvvm.Input;

namespace Course_Messenger.App.ViewModels;

internal class SignUpPageViewModel : LoginPasswordViewModel
{
    private string _passwordAccept = string.Empty;
    public string PasswordAccept
    {
        get => _passwordAccept;
        set
        {
            _passwordAccept = value;
            OnPropertyChanged(nameof(PasswordAccept));
        }
    }

    public RelayCommand CreateCommand { get; }

    public SignUpPageViewModel()
    {
        CreateCommand = new RelayCommand(Create);
    }

    private void Create()
    {
        string info = string.Empty;
        if (string.IsNullOrEmpty(Login))
        {
            info = "Login is empty.\n";
        }
        if (string.IsNullOrEmpty(Password))
        {
            info += "Password is empty.\n";
        }
        if (Password != PasswordAccept)
        {
            info += "Passwords are not equal.";
        }

        if (!string.IsNullOrEmpty(info))
        {
            App.Current.MainPage.DisplayAlert("Error", info, "Ok");
            return;
        }
        App.Current.MainPage.DisplayAlert("Success", "Account ready", "Ok");
    }
}
