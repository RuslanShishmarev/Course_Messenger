using Course_Messenger.App.Models;
using Course_Messenger.App.Services;

namespace Course_Messenger.App.ViewModels;

internal class ContactsPageViewModel : UsersChatsViewModel<UserShort>
{
    private UserRequestService _userRequestService;

    public ContactsPageViewModel()
    {
        _userRequestService = new UserRequestService();
        this.LoadDataAction = FillDataUsers;
    }

    private async void FillDataUsers()
    {
        var result = await _userRequestService.GetAll(App.Token, NamePattern);
        Elements.Clear();

        if (result is null) return;
        foreach (var user in result)
        {
            Elements.Add(user);
        }
    }
}
