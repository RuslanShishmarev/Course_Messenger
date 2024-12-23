namespace Course_Messenger.App.Models;

public class User : UserShort
{
    /// <summary>
    /// Пароль (его нужно обязятельно шифровать при сохранении в базу)
    /// </summary>
    public string Password { get; set; }
}
