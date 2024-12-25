namespace Course_Messenger.WEB.Models;

public class ChatWithMessage
{
    /// <summary>
    /// Это обязательное свойство для работы с базой данных
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получатель
    /// </summary>
    public UserShortModel UserTo { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Последнее сообщение
    /// </summary>
    public string? LastMessage { get; set; }

    public ChatWithMessage(ChatModel chatModel, UserShortModel userShort)
    {
        this.Id = chatModel.Id;
        this.UserTo = userShort;
        this.Created = chatModel.Created;
        LastMessage = chatModel.Messages.LastOrDefault()?.Text;
    }
}