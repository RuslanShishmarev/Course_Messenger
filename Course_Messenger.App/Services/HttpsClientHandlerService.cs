namespace Course_Messenger.App.Services;

internal class HttpsClientHandlerService
{
    public HttpMessageHandler GetHandlerByPlatform()
    {
#if ANDROID
        var handler = new Xamarin.Android.Net.AndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert != null || cert.Issuer.Equals("CN=localhost")) return true;

            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = (sender, url, trust) => url.StartsWith("https://localhost")
        };
        return handler;
#else
        throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
    }
}
