using Course_Messenger.App.Models;
using Newtonsoft.Json;

namespace Course_Messenger.App.Services;

public class ChatRequestService : CommonRequestService
{
    public async Task<IEnumerable<Chat>> GetChats(int userId, AuthToken token)
    {
        var url = Constants.HOST + $"Chats/{userId}";

        return await GetObjects<Chat>(url, userId, token);
    }

    public async Task<Chat?> GetChatForUsers(int userTo, AuthToken token)
    {
        var url = Constants.HOST + $"Chats/byuser/{userTo}";
        var (content, httpCode) = await SendActionToServer(
            url: url,
            httpMethod: HttpMethod.Get,
            token: token);

        if (httpCode == System.Net.HttpStatusCode.OK)
        {
            var obj = JsonConvert.DeserializeObject<Chat>(content);
            return obj;
        }

        return null;
    }

    public async Task<IEnumerable<Message>> GetMessages(int chatId, AuthToken token)
    {
        var url = Constants.HOST + $"Chats/messages/{chatId}";

        return await GetObjects<Message>(url, chatId, token);
    }

    private async Task<IEnumerable<T>> GetObjects<T>(string url, int id, AuthToken token)
    {
        var (content, httpCode) = await SendActionToServer(
            url: url,
            httpMethod: HttpMethod.Get,
            token: token);

        if (httpCode == System.Net.HttpStatusCode.OK)
        {
            var objs = JsonConvert.DeserializeObject<T[]>(content);
            return objs;
        }
        return Array.Empty<T>();
    }
}
