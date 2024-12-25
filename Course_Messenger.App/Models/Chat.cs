namespace Course_Messenger.App.Models;

public class Chat
{
    /// <summary>
    /// Это обязательное свойство для работы с базой данных
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public UserShort UserTo { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Последнее сообщение в чате
    /// </summary>
    public string? LastMessage { get; set; }

    public string? LastMessageShort => LastMessage?.Take(100).ToString();
}
