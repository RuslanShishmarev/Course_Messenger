using Course_Messenger.App.Models;

using Newtonsoft.Json;

namespace Course_Messenger.App.Services
{
    internal class UserRequestService : CommonRequestService
    {
        public async Task<User?> Create(User user)
        {
            var (content, httpCode) = await SendActionToServer(
                url: Host + "Users/",
                httpMethod: HttpMethod.Post,
                data: user);

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                var newUser = JsonConvert.DeserializeObject<User>(content);
                return newUser;
            }
            return null;
        }
    }
}
