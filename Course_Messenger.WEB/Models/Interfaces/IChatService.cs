namespace Course_Messenger.WEB.Models.Interfaces;
public interface IChatService
{
    /// <summary>
    /// Получить список чатов
    /// </summary>
    /// <param name="userId">id пользователя, который запрашивает список</param>
    /// <returns>Модель чата</returns>
    List<ChatUserMessage> GetChats(int userId);

    /// <summary>
    /// Получение чата по 2 пользователям
    /// </summary>
    /// <param name="user1">id пользователя 1</param>
    /// <param name="user2">id пользователя 2</param>
    /// <returns></returns>
    ChatUserMessage? GetChatForUsers(int user1, int user2);

    /// <summary>
    /// Получить сообщения в чате
    /// </summary>
    /// <param name="chatId">id чата</param>
    /// <returns>Список сообщений</returns>
    List<MessageModel> GetMessages(int chatId);

    /// <summary>
    /// Прочитать сообщения в чате
    /// </summary>
    /// <param name="chatId">id чата</param>
    /// <param name="readerId">id читающего</param>
    void SetViewedMessages(int chatId, int readerId);

    /// <summary>
    /// Последнее сообщение чата, для отображения в списке чатов (отправлять будем не более 50 символов)
    /// </summary>
    /// <param name="chatId">id чата</param>
    /// <returns>Сокращенный вариант сообщения</returns>
    MessageModel GetLastMessage(int chatId);

    /// <summary>
    /// Удаление чата
    /// </summary>
    /// <param name="chatId">id чата</param>
    /// <param name="userId">id пользователя</param>
    void DeleteChat(int chatId, int userId);

    /// <summary>
    /// Создание сообщения
    /// </summary>
    /// <param name="chatId">id чата (если есть)</param>
    /// <param name="from">id отправителя</param>
    /// <param name="to">id получателя</param>
    /// <param name="text">текст сообщения</param>
    /// <returns></returns>
    MessageModel CreateMessage(int chatId, int from, int to, string text);
}
