using Course_Messenger.App.Models;

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Course_Messenger.App.Services;

public class CommonRequestService
{
    public async Task<(string content, HttpStatusCode httpCode)> SendActionToServer(
        string url,
        HttpMethod httpMethod,
        LoginPasswordModel loginPassword = null,
        AuthToken token = null,
        object data = null)
    {
        var handler = new HttpsClientHandlerService();
        HttpClient client = new HttpClient(handler.GetHandlerByPlatform());

        using var request = new HttpRequestMessage(httpMethod, url);

        if (loginPassword != null)
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", loginPassword.GetEncoded());
        else if (token != null)
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.AccessToken);

        if (data != null)
            request.Content = JsonContent.Create(data);

        HttpStatusCode httpStatus = HttpStatusCode.BadRequest;
        try
        {
            using var response = httpMethod == HttpMethod.Get ? 
                await client.GetAsync(url) :
                await client.SendAsync(request);

            httpStatus = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return (content, httpStatus);
            }
            throw new Exception($"Web error {httpStatus}");

        }
        catch (Exception ex)
        {
            await App.Current.MainPage?.DisplayAlert("Error", ex.Message, "Ok");
            return (string.Empty, httpStatus);
        }
    }
}
