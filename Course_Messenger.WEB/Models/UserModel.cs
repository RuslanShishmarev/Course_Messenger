namespace Course_Messenger.WEB.Models
{
    public class UserModel
    {
        /// <summary>
        /// Это обязательное свойство для работы с базой данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Почта будет использоваться для авториции пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль (его нужно обязятельно шифровать при сохранении в базу)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фото пользователя
        /// </summary>
        public byte[]? Photo { get; set; }
    }
}
