using Course_Messenger.App.Services;

namespace Course_Messenger.App.ViewModels;

public class MainPageViewModel : BindableObject
{
    private UserRequestService _userRequestService;

    public MainPageViewModel()
    {
        _userRequestService = new UserRequestService();

        Task.Run(async() =>
        {
            var currentUser = await _userRequestService.Get(App.Token);
            if (currentUser == null)
                App.Current.MainPage?.DisplayAlert("Error", "Can't get user data", "Ok");
            else 
                App.CurrentUser = currentUser;
        });
    }
}
