using CommunityToolkit.Mvvm.Input;

using Course_Messenger.App.Models;
using Course_Messenger.App.Services;

using System.Collections.ObjectModel;

namespace Course_Messenger.App.ViewModels;

public class ChatUserPageViewModel : BindableObject
{
    public UserShort UserTo { get; }

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

    public RelayCommand SendNewMessageCommand {  get; }

    private int? _chatId = null;

    public ChatUserPageViewModel(UserShort userTo)
    {
        UserTo = userTo;
        _chatRequestService = new ChatRequestService();

        var listMessages = new MessageViewModel[]
        {
            new MessageViewModel(new Message
            {
                Id = 1,
                Text = "Hi! How are you?",
                Created = DateTime.Now,
                From = userTo.Id,
                To = App.CurrentUser.Id,
                ChatId = 0
            }, true),
            new MessageViewModel(new Message
            {
                Id = 2,
                Text = "My code: public ObservableCollection<MessageViewModel> Messages { get; }",
                Created = DateTime.Now,
                From = userTo.Id,
                To = App.CurrentUser.Id,
                ChatId = 0
            }, true),
            new MessageViewModel(new Message
            {
                Id = 3,
                Text = "Hi, fine!",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
            new MessageViewModel(new Message
            {
                Id = 4,
                Text = "public class ChatUserPageViewModel : BindableObject",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
            new MessageViewModel(new Message
            {
                Id = 2,
                Text = "My code: public ObservableCollection<MessageViewModel> Messages { get; }",
                Created = DateTime.Now,
                From = userTo.Id,
                To = App.CurrentUser.Id,
                ChatId = 0
            }, true),
            new MessageViewModel(new Message
            {
                Id = 3,
                Text = "Hi, fine!",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
            new MessageViewModel(new Message
            {
                Id = 4,
                Text = "public class ChatUserPageViewModel : BindableObject",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
            new MessageViewModel(new Message
            {
                Id = 2,
                Text = "My code: public ObservableCollection<MessageViewModel> Messages { get; }",
                Created = DateTime.Now,
                From = userTo.Id,
                To = App.CurrentUser.Id,
                ChatId = 0
            }, true),
            new MessageViewModel(new Message
            {
                Id = 3,
                Text = "Hi, fine!",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
            new MessageViewModel(new Message
            {
                Id = 4,
                Text = "public class ChatUserPageViewModel : BindableObject",
                Created = DateTime.Now,
                From = App.CurrentUser.Id,
                To = userTo.Id,
                ChatId = 0
            }, false),
        };

        Messages = new ObservableCollection<MessageViewModel>(listMessages);

        SendNewMessageCommand = new RelayCommand(SendNewMessage);
    }

    private ChatRequestService _chatRequestService;

    public ChatUserPageViewModel(Chat chat)
    {
        UserTo = chat.UserTo;
        _chatRequestService = new ChatRequestService();

        SendNewMessageCommand = new RelayCommand(SendNewMessage);
        Task.Run(async () =>
        {
            var messages = await _chatRequestService.GetMessages(chat.Id, App.Token);
            Messages = new ObservableCollection<MessageViewModel>(
                messages.Select(x => new MessageViewModel(x, x.To == App.CurrentUser.Id)));
        });
    }

    private void SendNewMessage()
    {
        var message = new Message
        {
            Id = Messages.Count,
            Text = this.NewMessage,
            Created = DateTime.Now,
            From = App.CurrentUser.Id,
            To = UserTo.Id,
            ChatId = _chatId ?? 0
        };

        Messages.Add(new MessageViewModel(message, false));
    }
}
