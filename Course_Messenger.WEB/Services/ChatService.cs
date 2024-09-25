using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

namespace Course_Messenger.WEB.Services;
public class ChatService : IChatService
{
    public List<ChatModel> GetChats(int userId)
    {
        using (var db = new CourseAppContext())
        {
            //Проверяем значение свойств id по запросу от текущего юзера
            return db.Chats
                .Where(x => x.User1 ==  userId || x.User2 == userId)
                .ToList();
        }
    }

    public MessageModel GetLastMessage(int chatId)
    {
        using (var db = new CourseAppContext())
        {
            //получаем последнее сообщение по id чата
            return db.Messages.LastOrDefault(x => x.ChatId == chatId);
        }
    }

    public List<MessageModel> GetMessages(int chatId)
    {
        using (var db = new CourseAppContext())
        {
            //получаем все сообщения по id чата
            return db.Messages.Where(x => x.ChatId == chatId).ToList();
        }
    }

    public void DeleteChat(int chatId)
    {
        using (var db = new CourseAppContext())
        {
            //ищем чат по id, если не найдет будет исключение
            var chat = db.Chats.First(x => x.Id == chatId);
            db.Chats.Remove(chat);
            db.SaveChanges();
        }
    }

    public MessageModel CreateMessage(int chatId, int from, int to, string text)
    {
        using (var db = new CourseAppContext())
        {
            //найдем отправителя и получателя на существование
            var sender = db.Users.Find(from);
            var recipient = db.Users.Find(to);

            if (sender == null || recipient == null)
            {
                throw new Exception("Пользоваьеди не найдено");
            }

            //ищем чат
            var chat = db.Chats.FirstOrDefault(x => x.Id == chatId);
            var date = DateTime.Now;
            if (chat is null)
            {
                //если его нет, то создаем
                chat = new ChatModel
                {
                    User1 = from,
                    User2 = to,
                    Created = date,
                };
                db.Chats.Add(chat);
                db.SaveChanges();
            }

            //создаем сообщение
            var newMessage = new MessageModel
            {
                ChatId = chat.Id,
                Text = text,
                From = from,
                To = to,
                Created = date
            };

            db.Messages.Add(newMessage);
            db.SaveChanges();

            return newMessage;
        }
    }
}
