using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course_Messenger.WEB.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ChatsController : ControllerBase
{
    private IChatService _chatService;
    public ChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("{userId}")]
    public List<ChatModel> GetChats(int userId)
    {
        return _chatService.GetChats(userId);
    }

    [HttpGet("messages/{chatId}")]
    public List<MessageModel> GetMessages(int chatId)
    {
        return _chatService.GetMessages(chatId);
    }

    [HttpGet("last-message/{chatId}")]
    public MessageModel GetLastMessage(int chatId)
    {
        return _chatService.GetLastMessage(chatId);
    }

    [HttpDelete("{chatId}")]
    public IActionResult DeleteChat(int chatId)
    {
        _chatService.DeleteChat(chatId);
        return Ok("Чат удален");
    }
}
