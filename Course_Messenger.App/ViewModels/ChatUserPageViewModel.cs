using CommunityToolkit.Mvvm.Input;
using Course_Messenger.App.Models;

using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace Course_Messenger.App.ViewModels;

public class ChatUserPageViewModel : BindableObject
{
    private UserShort UserTo { get; }

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


    public ObservableCollection<MessageViewModel> Messages { get; }

    public RelayCommand SendNewMessageCommand {  get; }

    private int? _chatId;

    public ChatUserPageViewModel(UserShort userTo)
    {
        UserTo = userTo;

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
