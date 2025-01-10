using Course_Messenger.App.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace Course_Messenger.App.Services;

public class HubChatService : HubConnectionBuilder
{
    private HubConnection _connection;

    private string _channel = $"{Constants.CHAT_HOST}chat";

    private const string RECIEVE = "Recieve";
    private const string SEND_MESSAGE = "SendMessage";
    private const string READ_CHAT = "ReadChat";
    private const string USER_IN_CHAT = "UserInChat";

    public HubChatService()
    {
        var hadlerHelper = new HttpsClientHandlerService();
        _connection = new HubConnectionBuilder()
            .WithUrl(_channel, 
            options =>
            {
                options.HttpMessageHandlerFactory = handler => hadlerHelper.GetHandlerByPlatform();
                options.AccessTokenProvider = () => Task.FromResult(App.Token.AccessToken);
            })
            .Build();
    }

    public void RegisterRecive(Action<MessageDTO> getMessage)
    {
        _connection.On(RECIEVE, getMessage);
    }

    public void RegisterUserInChat(Action<int> userInChat)
    {
        _connection.On(USER_IN_CHAT, userInChat);
    }

    public async Task Start()
    {
        await _connection.StartAsync();
    }

    public async Task Stop()
    {
        await _connection.StopAsync();
    }

    public async Task SendMessage(MessageDTO message)
    {
        await _connection.InvokeAsync(SEND_MESSAGE, message);
    }

    public async Task ReadChat(int chatId)
    {
        await _connection.InvokeAsync(READ_CHAT, chatId);
    }
}
