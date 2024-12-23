using Course_Messenger.WEB.Models;
using Course_Messenger.WEB.Models.Interfaces;
using Course_Messenger.WEB.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

namespace Course_Messenger.WEB.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public IEnumerable<UserShortModel> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpGet]
        public IEnumerable<UserShortModel> GetAllFilter(string name)
        {
            return _userService.GetAll(name);
        }

        [HttpGet("me")]
        public UserModel Get()
        {
            return _userService.Get(HttpContext.User.Identity.Name);
        }

        [HttpPost]
        [AllowAnonymous]
        public UserModel Create([FromBody] UserModel model)
        {
            return _userService.Create(model);
        }

        [HttpPatch]
        public ActionResult<UserModel> Update([FromBody] UserModel model)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.Get(currentUserEmail);

            if (currentUser == null || currentUser.Id != model.Id)
            {
                return NotFound();
            }
            return _userService.Update(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var currentUserEmail = HttpContext.User.Identity.Name;
            var currentUser = _userService.Get(currentUserEmail);

            if (currentUser == null || currentUser.Id != id)
            {
                return NotFound();
            }

            _userService.Delete(id);
            return new OkResult();
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public object GetToken()
        {
            // get user data from DB
            var userData = _userService.GetUserLoginPassFromBasicAuth(Request);
            // get identity
            var identity = _userService.GetIdentity(userData.login, userData.password, out int userId);

            if (identity is null) return NotFound("login or password is not correct");

            // create jwt token 
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.AddMinutes(AuthOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // return token
            var tokenModel = new AuthToken(
                minutes: AuthOptions.LIFETIME,
                accessToken: encodedJwt,
                userName: userData.login,
                userId: userId);

            return Ok(tokenModel);
        }
    }
}
