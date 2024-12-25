using Course_Messenger.App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Messenger.App.Services;

public class ChatRequestService : CommonRequestService
{
    public async Task<IEnumerable<Chat>> GetChats(int userId, AuthToken token)
    {
        var url = Host + $"Chats/{userId}";

        return await GetObjects<Chat>(url, userId, token);
    }

    public async Task<IEnumerable<Message>> GetMessages(int chatId, AuthToken token)
    {
        var url = Host + $"Chats/messages/{chatId}";

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
