using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace pokerDealerApp
{
    class PokerDealerProxy
    {
        public static async Task<string> addUser(string username, string password,
            string forename, string lastname, string email, string phone)
        {
            return await App.MobileService.InvokeApiAsync<string>("signin/adduser",
                HttpMethod.Get,
                new Dictionary<string, string>() {
                    {"username", username},
                    {"password", password},
                    {"forename", forename},
                    {"lastname", lastname},
                    {"email"   , email},
                    {"phone"   , phone}
                });
        }
        public static async Task<string> logIn(string username, string password)
        {
            return await App.MobileService.InvokeApiAsync<string>("loginuser/loginuser",
                            HttpMethod.Get,
                            new Dictionary<string, string>() {
                    {"username", username},
                    {"password", password}
                });
        }
    }
}
