namespace Course_Messenger.WEB.Models;

public class ChatUserMessage
{
    /// <summary>
    /// ��� ������������ �������� ��� ������ � ����� ������
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ����������
    /// </summary>
    public UserShortModel UserTo { get; set; }

    /// <summary>
    /// ���� ��������
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// ��������� ���������
    /// </summary>
    public string? LastMessage { get; set; }

    public ChatUserMessage(ChatModel chatModel, UserShortModel userShort)
    {
        this.Id = chatModel.Id;
        this.UserTo = userShort;
        this.Created = chatModel.Created;
        LastMessage = chatModel.Messages.LastOrDefault()?.Text;
    }
}