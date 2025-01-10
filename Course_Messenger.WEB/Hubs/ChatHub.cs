using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Course_Messenger.WEB.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private const string RECIEVE = "Recieve";
    private const string USER_IN_CHAT = "UserInChat";
    private IUserService _userService;
    private IChatService _chatService;
    public ChatHub(IUserService userService, IChatService chatService)
    {
        _userService = userService;
        _chatService = chatService;
    }

    public override async Task OnConnectedAsync()
    {
        var allUserChats = GetUserChats();

        foreach (var chat in allUserChats)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString());
        }

        base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var allUserChats = GetUserChats();

        foreach (var chat in allUserChats)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat.Id.ToString());
        }

        base.OnDisconnectedAsync(exception);
    }

    public async Task ReadChat(int chatId)
    {
        var userName = Context.User.Identity.Name;
        var user = _userService.Get(userName);

        await Clients.Group(chatId.ToString()).SendAsync(USER_IN_CHAT, user.Id);

        _chatService.SetViewedMessages(chatId, user.Id);
    }

    public async Task SendMessage(MessageDTO message)
    {
        var newMessageModel = _chatService.CreateMessage(
            chatId: message.ChatId,
            from: message.From,
            to: message.To,
            text: message.Text);

        message.Id = newMessageModel.Id;

        await Clients.Group(message.ChatId.ToString()).SendAsync(RECIEVE, message);
    }

    private List<ChatUserMessage> GetUserChats()
    {
        var userName = Context.User.Identity.Name;
        var user = _userService.Get(userName);

        return _chatService.GetChats(user.Id);
    }
}
