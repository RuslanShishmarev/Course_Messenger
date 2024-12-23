using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

using System.Security.Claims;
using System.Text;

namespace Course_Messenger.WEB.Services
{
    public class UserService : IUserService
    {
        private CourseAppContext _dbContext;
        public UserService(CourseAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel Get(string email, string password)
        {
            // тут можно сделать шифрацию пароля по тому же
            // алгоритму, что и в создании пользователя
            // и порверять уже шифрованный пароль
            var existed = _dbContext.Users.First(u => u.Email == email && u.Password == password);
            return existed;
        }

        public IEnumerable<UserShortModel> GetAll()
        {
            return _dbContext.Users.Cast<UserShortModel>();
        }

        public IEnumerable<UserShortModel> GetAll(string namePattern)
        {
            return _dbContext.Users
                .Where(x => x.Name.Contains(namePattern) || 
                            x.Email.Contains(namePattern))
                .Cast<UserShortModel>();
        }

        public UserModel Get(string email)
        {
            var existed = _dbContext.Users.First(u => u.Email == email);
            return existed;
        }

        public UserModel Create(UserModel model)
        {
            // проверяем, есть ли в базе пользователь с таким email
            var existed = _dbContext.Users.Any(u => u.Email == model.Email);
            if (existed)
            {
                // если есть, то вызываем ошибку
                throw new Exception($"Пользователь с Email {model.Email} уже сущуствует");
            }
            // тут рекомендуется сделать шифрацию пароля перед сохранением
            _dbContext.Users.Add(model);
            _dbContext.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            // ищем по id пользователя для удаления
            // если нашли, то удаляем. если нет, ничего не делаем
            var modelToDelete = _dbContext.Users.Find(id);
            if (modelToDelete != null)
            {
                _dbContext.Users.Remove(modelToDelete);
                _dbContext.SaveChanges();
            }
        }

        public UserModel Update(UserModel model)
        {
            // ищем пользователя по id. если не находим, то вызываем ошибку
            var modelToUpdate = _dbContext.Users.Find(model.Id);
            if (modelToUpdate is null)
            {
                throw new Exception("Пользователь не найден");
            }

            // меняем почту и фотку
            modelToUpdate.Email = model.Email;
            modelToUpdate.Name = model.Name;
            modelToUpdate.Photo = model.Photo;

            _dbContext.Users.Update(modelToUpdate);
            _dbContext.SaveChanges();

            return modelToUpdate;
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
