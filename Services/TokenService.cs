using System;
using PCShop.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PCShop.Models;

namespace PCShop.Services
{
    public class TokenService
    {
        private readonly UserService _userService;

        Dictionary<String, String> tokenArray = new Dictionary<String, String>();

        public TokenService(UserService userService)
        {
            _userService = userService;

            var users = _userService.Get();

            foreach(User user in users)
            {
                this.AddTokenToArray(user.Id, user.MyToken);
            }
           
        }

        public Dictionary<String, String> GetTokens()
        {
            return this.tokenArray;
        }

        public Boolean AddTokenToArray(String userId, String token)
        {
            this.tokenArray.Remove(userId);
            this.tokenArray.Add(userId, token);
            return true;
        }
    }
}

