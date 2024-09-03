using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Course_Messenger.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public UserModel Get(string email, string password)
        {
            return _userService.Get(email, password);
        }

        [HttpPost]
        public UserModel Create([FromBody] UserModel model)
        {
            return _userService.Create(model);
        }

        [HttpPatch]
        public UserModel Update([FromBody] UserModel model)
        {
            return _userService.Update(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return new OkResult();
        }
    }
}
