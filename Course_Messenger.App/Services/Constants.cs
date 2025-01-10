namespace Course_Messenger.App.Services;

public class Constants
{
    public static string HOST => DeviceInfo.Platform == DevicePlatform.Android ?
        "http://10.0.2.2:5000/" :
        "http://localhost:5000/";

    public static string CHAT_HOST => HOST;
}
