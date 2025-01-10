using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Models;
using Course_Messenger.App.Views;

using System.Collections.ObjectModel;

namespace Course_Messenger.App.ViewModels;

public class UsersChatsViewModel<T> : BindableObject
{
    private string? _namePattern = null;
    public string? NamePattern
    {
        get => _namePattern;
        set
        {
            _namePattern = value;
            OnPropertyChanged(nameof(NamePattern));
            LoadDataAction?.Invoke();
        }
    }
    public ObservableCollection<T> Elements { get; set; } = new ObservableCollection<T>();

    public RelayCommand<T> OpenChatCommand { get; }

    protected Action LoadDataAction { get; set; }

    private ChatUserPageViewModel _lastOpenedChat;

    public UsersChatsViewModel()
    {
        OpenChatCommand = new RelayCommand<T>(OpenChat);

        App.HubChatService.RegisterRecive(RecieveMessage);
        App.HubChatService.RegisterUserInChat(userId => _lastOpenedChat?.UserInChat(userId));

        App.ConnectHub();
    }

    private async void OpenChat(T? element)
    {
        if (element is UserShort selectedContact)
        {
            _lastOpenedChat = new ChatUserPageViewModel(selectedContact);
        }
        else if (element is Chat chat)
        {
            _lastOpenedChat = new ChatUserPageViewModel(chat);
        }
        else _lastOpenedChat = null;

        if (_lastOpenedChat is null) return;

        await App.Current.MainPage.Navigation.PushModalAsync(new ChatUserPage
        {
            BindingContext = _lastOpenedChat
        });
    }

    private void RecieveMessage(MessageDTO messageDTO)
    {
        if (this.Elements is ObservableCollection<Chat> chats)
        {
            var existedChat = chats.FirstOrDefault(x => x.Id == messageDTO.ChatId);

            if (existedChat != null)
            {
                existedChat.LastMessage = messageDTO.Text;
            }
            else
            {
                LoadDataAction?.Invoke();
                existedChat = chats.FirstOrDefault(x => x.Id == messageDTO.ChatId);
            }

            if (existedChat is null) return;

            existedChat.LastMessage = messageDTO.Text;
        }

        if (_lastOpenedChat?.Chat?.Id != messageDTO.ChatId) return;

        _lastOpenedChat.RecieveMessage(messageDTO);
    }
}
