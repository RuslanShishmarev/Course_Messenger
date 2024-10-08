﻿using System.Security.Claims;

namespace Course_Messenger.WEB.Models.Interfaces
{
    public interface IUserService
    {
        UserModel Get(string email, string password);

        UserModel Get(string email);

        UserModel Create(UserModel model);

        UserModel Update(UserModel model);

        void Delete(int id);

        (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request);

        ClaimsIdentity GetIdentity(string email, string password, out int userId);
    }
}
