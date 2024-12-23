using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Services;
using Course_Messenger.App.Views;

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

    private UserRequestService _userService;
    public SignUpPageViewModel()
    {
        CreateCommand = new RelayCommand(Create);
        _userService = new UserRequestService();
    }

    private async void Create()
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

        var newUser = await _userService.Create(
            new Models.User
            {
                Id = 0,
                Email = Login,
                Password = Password,
                Name = string.Empty,
            }
        );

        if (newUser is null) return;

        //App.CurrentUser = newUser;
        App.Current.MainPage = new LoginPage();
    }
}
