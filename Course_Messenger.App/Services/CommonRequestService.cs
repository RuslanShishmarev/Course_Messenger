using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Course_Messenger.App.Services;

internal class CommonRequestService
{
    public async Task<string> SendActionToServer(
        string url,
        HttpMethod httpMethod,
        string login,
        string password,
        object data = null)
    {
        var handler = new HttpsClientHandlerService();
        HttpClient client = new HttpClient(handler.GetHandlerByPlatform());

        string encoded = Convert.ToBase64String(
            Encoding.GetEncoding("ISO-8859-1").GetBytes($"{login}:{password}"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(encoded);

        using var request = new HttpRequestMessage(httpMethod, url);

        if (data != null ) request.Content = JsonContent.Create(data);

        try
        {
            using var response = await client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (Exception ex)
        {
            await App.Current.MainPage?.DisplayAlert("Error", "Server is not working", "Ok");
            return string.Empty;
        }
    }
}
