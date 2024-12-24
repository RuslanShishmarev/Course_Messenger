using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Services;
using Course_Messenger.App.Views;

namespace Course_Messenger.App.ViewModels;

internal class LoginPageViewModel : LoginPasswordViewModel
{
    public RelayCommand SignInCommand { get; }
    public RelayCommand SignUpCommand { get; }

    private UserRequestService _userRequestService;

    public LoginPageViewModel()
    {
        SignInCommand = new RelayCommand(SignIn);
        SignUpCommand = new RelayCommand(SignUp);

        _userRequestService = new UserRequestService();
        this.Login = App.CurrentUser?.Email ?? Preferences.Get(nameof(this.Login), string.Empty);
        this.Password = App.CurrentUser?.Password ?? Preferences.Get(nameof(this.Password), string.Empty);
    }

    private async void SignIn()
    {
        if (!CanSignIn) return;

        var token = await _userRequestService.GetToken(this.Login, this.Password);

        if (token is null) return;
        App.Token = token;

        Preferences.Set(nameof(this.Login), this.Login);
        Preferences.Set(nameof(this.Password), this.Password);

        App.Current.MainPage = new MainPage();
    }

    private void SignUp()
    {
        App.Current.MainPage = new SignUpPage();
    }
}
