using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PCShop.Models;
using PCShop.Services;

namespace PCShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public LoginController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public ActionResult<String> Login (User user)
        {
            User found = _userService.GetByEmail(user.Email);

            if (found != null && found.Password == user.Password)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var token = new String(stringChars);

                _tokenService.AddTokenToArray(found.Id, token);

                found.MyToken = token;

                _userService.Update(found.Id, found);

                

                String responseToken = "{\"token\" :" + "\"" + token + "\"}";
                return responseToken;
            }
            String responseTokenError = "{\"message\" :" + "\"" + "Unauthorized!" + "\"}";
            return responseTokenError;
        }
    }
}
