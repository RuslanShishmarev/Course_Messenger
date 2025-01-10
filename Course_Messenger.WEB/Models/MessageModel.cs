namespace Course_Messenger.WEB.Models
{
    public class MessageModel
    {
        /// <summary>
        /// Это обязательное свойство для работы с базой данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Id отправителя
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Id получателя
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// Id чата
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        /// Модель чата
        /// </summary>
        public ChatModel Chat { get; set; }

        /// <summary>
        /// Флаг, прочитано ли сообщение
        /// </summary>
        public bool IsViewed { get; set; }
    }
}
