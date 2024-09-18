using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;
using System.Security.Claims;
using System.Text;

namespace Course_Messenger.WEB.Services
{
    public class UserService : IUserService
    {
        public UserModel Get(string email, string password)
        {
            using (var db = new CourseAppContext())
            {
                // тут можно сделать шифрацию пароля по тому же
                // алгоритму, что и в создании пользователя
                // и порверять уже шифрованный пароль
                var existed = db.Users.First(u => u.Email == email && u.Password == password);
                return existed;
            }
        }

        public UserModel Get(string email)
        {
            using (var db = new CourseAppContext())
            {
                var existed = db.Users.First(u => u.Email == email);
                return existed;
            }
        }

        public UserModel Create(UserModel model)
        {
            using (var db = new CourseAppContext())
            {
                // проверяем, есть ли в базе пользователь с таким email
                var existed = db.Users.Any(u => u.Email == model.Email);
                if (existed)
                {
                    // если есть, то вызываем ошибку
                    throw new Exception($"Пользователь с Email {model.Email} уже сущуствует");
                }

                // тут рекомендуется сделать шифрацию пароля перед сохранением
                db.Users.Add(model);
                db.SaveChanges();
            }
            return model;
        }

        public void Delete(int id)
        {
            using (var db = new CourseAppContext())
            {
                // ищем по id пользователя для удаления
                // если нашли, то удаляем. если нет, ничего не делаем
                var modelToDelete = db.Users.Find(id);
                if (modelToDelete != null)
                {
                    db.Users.Remove(modelToDelete);
                    db.SaveChanges();
                }
            }
        }

        public UserModel Update(UserModel model)
        {
            using (var db = new CourseAppContext())
            {
                // ищем пользователя по id. если не находим, то вызываем ошибку
                var modelToUpdate = db.Users.Find(model.Id);
                if (modelToUpdate is null)
                {
                    throw new Exception("Пользователь не найден");
                }

                // меняем почту и фотку
                modelToUpdate.Email = model.Email;
                modelToUpdate.Photo = model.Photo;

                db.Users.Update(modelToUpdate);
                db.SaveChanges();
                return modelToUpdate;
            }
        }

        public (string login, string password) GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName = "";
            string userPass = "";
            string authHeader = request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePass = authHeader.Replace("Basic ", "");
                var encoding = Encoding.GetEncoding("iso-8859-1");

                string[] namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).Split(':');
                userName = namePassArray[0];
                userPass = namePassArray[1];
            }
            return (userName, userPass);
        }

        public ClaimsIdentity GetIdentity(string email, string password, out int userId)
        {
            var currentUser = Get(email, password);
            userId = currentUser?.Id ?? -1;

            if (currentUser == null || !VerifyHashedPassword(currentUser.Password, password)) return null;

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "User"),
                };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private bool VerifyHashedPassword(string password1, string password2)
        {
            return password1 == password2;
        }
    }
}
