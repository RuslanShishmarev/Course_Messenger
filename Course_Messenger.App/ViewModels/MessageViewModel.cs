using Course_Messenger.App.Models;

namespace Course_Messenger.App.ViewModels;

public class MessageViewModel
{
    public Message Model { get; }

    public bool ToMe { get; }

    public string DateStr { get; }

    public MessageViewModel(Message model, bool toMe)
    {
        Model = model;
        ToMe = toMe;
        DateStr = model.Created.ToString("g");
    }
}
