namespace Course_Messenger.WEB.Models
{
    public class ChatModel
    {
        /// <summary>
        /// Это обязательное свойство для работы с базой данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя 1
        /// </summary>
        public int User1 { get; set; }

        /// <summary>
        /// Id пользователя 2
        /// </summary>
        public int User2 { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
    }
}
