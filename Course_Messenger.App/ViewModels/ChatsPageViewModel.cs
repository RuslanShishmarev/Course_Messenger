using Course_Messenger.App.Models;
using Course_Messenger.App.Services;

namespace Course_Messenger.App.ViewModels;

public class ChatsPageViewModel : UsersChatsViewModel<Chat>
{
    private ChatRequestService _chatRequestService;

    public ChatsPageViewModel()
    {
        _chatRequestService = new ChatRequestService();
        LoadDataAction = LoadChats;
        LoadChats();
    }

    private async void LoadChats()
    {
        var result = await _chatRequestService.GetChats(App.CurrentUser.Id, App.Token);
        Elements.Clear();

        if (result is null) return;
        foreach (var user in result)
        {
            Elements.Add(user);
        }
    }
}
