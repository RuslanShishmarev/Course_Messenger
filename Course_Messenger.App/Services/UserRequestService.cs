using Course_Messenger.App.Models;

using Newtonsoft.Json;

namespace Course_Messenger.App.Services
{
    public class UserRequestService : CommonRequestService
    {
        public async Task<AuthToken?> GetToken(string login, string password)
        {
            var (content, httpCode) = await SendActionToServer(
                url: Host + "Users/token",
                httpMethod: HttpMethod.Post,
                loginPassword: new LoginPasswordModel(login, password));

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                var token = JsonConvert.DeserializeObject<AuthToken>(content);
                return token;
            }
            return null;
        }

        public async Task<User?> Get(AuthToken token)
        {
            var (content, httpCode) = await SendActionToServer(
                url: Host + "Users/",
                httpMethod: HttpMethod.Get,
                token: token);

            if (httpCode == System.Net.HttpStatusCode.OK)
            {
                var newUser = JsonConvert.DeserializeObject<User>(content);
                return newUser;
            }
            return null;
        }

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
