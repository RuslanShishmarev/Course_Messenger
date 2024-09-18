﻿namespace Course_Messenger.WEB.Models;

public class AuthToken
{
    public int Minutes { get; private set; }

    public string AccessToken { get; private set; }

    public string UserName { get; private set; }

    public int UserId { get; private set; }

    public AuthToken(
        int minutes,
        string accessToken,
        string userName,
        int userId)
    {
        Minutes = minutes;
        AccessToken = accessToken;
        UserName = userName;
        UserId = userId;
    }
}
