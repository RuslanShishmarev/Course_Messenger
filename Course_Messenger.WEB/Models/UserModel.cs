namespace Course_Messenger.WEB.Models;

public class UserModel : UserShortModel
{
    /// <summary>
    /// Пароль (его нужно обязятельно шифровать при сохранении в базу)
    /// </summary>
    public string Password { get; set; }
}
