using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Models;
using Course_Messenger.App.Services;

using System.Collections.ObjectModel;

namespace Course_Messenger.App.ViewModels;

public class ChatUserPageViewModel : BindableObject
{
    public UserShort UserTo { get; }

    private bool _isUserOnline;

    public bool IsUserOnline
    {
        get => _isUserOnline;
        private set
        {
            _isUserOnline = value;
        }
    }


    private string _newMassage = string.Empty;
    public string NewMessage
    {
        get => _newMassage;
        set
        {
            _newMassage = value;
            OnPropertyChanged(nameof(NewMessage));
        }
    }

    public ObservableCollection<MessageViewModel> Messages { get; private set; }
        = new ObservableCollection<MessageViewModel>();

    public RelayCommand SendNewMessageCommand { get; private set; }

    public Chat? Chat { get; private set; }

    private ChatRequestService _chatRequestService = new ChatRequestService();

    public ChatUserPageViewModel(UserShort userTo)
    {
        UserTo = userTo;
        SendNewMessageCommand = new RelayCommand(SendNewMessage);
        Task.Run(async () =>
        {
            Chat = await _chatRequestService.GetChatForUsers(userTo.Id, App.Token);
            if (Chat is null) return;

            await LoadMessages();
        });
    }

    public ChatUserPageViewModel(Chat chat)
    {
        Chat = chat;
        UserTo = chat.UserTo;
        SendNewMessageCommand = new RelayCommand(SendNewMessage);
        Task.Run(LoadMessages);

        App.HubChatService.ReadChat(chat.Id);
    }

    public void UserInChat(int userId)
    {
        IsUserOnline = userId == UserTo.Id;
    }

    private async Task LoadMessages()
    {
        if (Chat is null) return;
        var messages = await _chatRequestService.GetMessages(Chat.Id, App.Token);
        foreach (var message in messages)
        {
            Messages.Add(new MessageViewModel(message, message.To == App.CurrentUser.Id));
        }
    }

    private async void SendNewMessage()
    {
        var message = new Message
        {
            Id = Messages.Count,
            Text = this.NewMessage,
            Created = DateTime.Now,
            From = App.CurrentUser.Id,
            To = UserTo.Id,
            ChatId = Chat?.Id ?? 0,
            IsViewed = false
        };

        //Messages.Add(new MessageViewModel(message, false));

        await App.HubChatService.SendMessage(
            new MessageDTO
            {
                Id = 0,
                Text = this.NewMessage,
                From = App.CurrentUser.Id,
                To = UserTo.Id,
                ChatId= Chat?.Id ?? 0,
            }
        );
    }

    public void RecieveMessage(MessageDTO messageDTO)
    {
        bool isToMe = App.CurrentUser.Id == messageDTO.To;

        var message = new Message
        {
            Id = messageDTO.Id,
            Text = messageDTO.Text,
            Created = DateTime.Now,
            From = messageDTO.From,
            To = messageDTO.To,
            ChatId = messageDTO.ChatId,
            IsViewed = isToMe == true ? true : IsUserOnline
        };

        Messages.Add(new MessageViewModel(message, isToMe));
    }
}
