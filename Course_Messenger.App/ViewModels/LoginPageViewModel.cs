using CommunityToolkit.Mvvm.Input;
using Course_Messenger.App.Views;

namespace Course_Messenger.App.ViewModels;

internal class LoginPageViewModel : LoginPasswordViewModel
{
    public RelayCommand SignInCommand { get; }
    public RelayCommand SignUpCommand { get; }

    public LoginPageViewModel()
    {
        SignInCommand = new RelayCommand(SignIn);
        SignUpCommand = new RelayCommand(SignUp);

        this.Login = App.CurrentUser?.Email;
        this.Password = App.CurrentUser?.Password;
    }

    private void SignIn()
    {

    }

    private void SignUp()
    {
        App.Current.MainPage = new SignUpPage();
    }
}
