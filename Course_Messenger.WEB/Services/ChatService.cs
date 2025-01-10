using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Course_Messenger.WEB.Services;

public class ChatService : IChatService
{
    private CourseAppContext _dbContext;
    public ChatService(CourseAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ChatUserMessage> GetChats(int userId)
    {
        var result = new List<ChatUserMessage>();

        var allChats = _dbContext.Chats
            .Include(x => x.Messages)
            .Where(x => x.User1 == userId || x.User2 == userId)
            .ToList();

        foreach (ChatModel item in allChats)
        {
            UserShortModel user = _dbContext.Users.Find(item.User1 == userId ? item.User2 : item.User1);

            result.Add(new ChatUserMessage(item, user));
        }
        return result;
    }

    public ChatUserMessage? GetChatForUsers(int currentUserId, int userTo)
    {
        ChatModel? chat = _dbContext.Chats
            .Include(x => x.Messages)
            .FirstOrDefault(x => (x.User1 == currentUserId && x.User2 == userTo) || 
                                 (x.User1 == userTo && x.User2 == currentUserId));
        
        UserShortModel? user = _dbContext.Users.Find(userTo);

        if (chat == null && user != null)
        {
            var newChat = new ChatModel
            {
                Created = DateTime.Now,
                User1 = currentUserId,
                User2 = userTo,
            };
            _dbContext.Chats.Add(newChat);
            _dbContext.SaveChanges();

            chat = newChat;
        }
        else if (user is null)
            return null;

        return new ChatUserMessage(chat, user);
    }

    public MessageModel GetLastMessage(int chatId)
    {
        //получаем последнее сообщение по id чата
        return _dbContext.Messages.LastOrDefault(x => x.ChatId == chatId);
    }

    public List<MessageModel> GetMessages(int chatId)
    {
        //получаем все сообщения по id чата
        return _dbContext.Messages.Where(x => x.ChatId == chatId).ToList();
    }

    public void SetViewedMessages(int chatId, int readerId)
    {
        var messages = _dbContext.Messages.Where(x => 
            x.ChatId == chatId &&
            x.To == readerId &&
            x.IsViewed == false)
            .ToList();

        foreach (var message in messages)
        {
            message.IsViewed = true;
        }
        _dbContext.Messages.UpdateRange(messages);
        _dbContext.SaveChangesAsync();
    }

    public void DeleteChat(int chatId, int userId)
    {
        //ищем чат по id, если не найдет будет исключение
        var chat = _dbContext.Chats.First(x => (x.User1 == userId || x.User2 == userId) && x.Id == chatId);
        _dbContext.Chats.Remove(chat);
        _dbContext.SaveChanges();
    }

    public MessageModel CreateMessage(int chatId, int from, int to, string text)
    {
        //найдем отправителя и получателя на существование
        var sender = _dbContext.Users.Find(from);
        var recipient = _dbContext.Users.Find(to);

        if (sender == null || recipient == null)
        {
            throw new Exception("Пользоваьеди не найдено");
        }

        //ищем чат
        var chat = _dbContext.Chats.FirstOrDefault(x => x.Id == chatId);
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
            _dbContext.Chats.Add(chat);
            _dbContext.SaveChanges();
        }

        //создаем сообщение
        var newMessage = new MessageModel
        {
            ChatId = chat.Id,
            Text = text,
            From = from,
            To = to,
            Created = date,
            IsViewed = false,
        };

        _dbContext.Messages.Add(newMessage);
        _dbContext.SaveChanges();

        return newMessage;
    }
}
