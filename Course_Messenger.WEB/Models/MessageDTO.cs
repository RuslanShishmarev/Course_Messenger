namespace Course_Messenger.WEB.Models;

public class MessageDTO
{
    public int Id { get; set; }

    public int ChatId { get; set; }

    public int From { get; set; }

    public int To { get; set; }

    public string Text { get; set; }
}
