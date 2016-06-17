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

        public static async Task<string> IsFreePlace()
        {
            return await App.MobileService.InvokeApiAsync<string>("isfreeplace/isfreeplace",
                            HttpMethod.Get,
                            new Dictionary<string, string>() {
                });
        }

        public static async Task<string> IsActiveGame()
        {
            return await App.MobileService.InvokeApiAsync<string>("isactivegame/isactivegame",
                            HttpMethod.Get,
                            new Dictionary<string, string>()
                            {
                            });
        }

        public static async Task<string> GetUsernameById(Int32 id)
        {
            return await App.MobileService.InvokeApiAsync<string>("getusernamebyid/getusernamebyid",
                            HttpMethod.Get,
                            new Dictionary<string, string>()
                            {
                                {"id", id.ToString()}
                            });
        }
        public static async Task<string> GetDollarsById(Int32 id)
        {
            return await App.MobileService.InvokeApiAsync<string>("getdollarsbyid/getdollarsbyid",
                            HttpMethod.Get,
                            new Dictionary<string, string>()
                            {
                                {"id", id.ToString()}
                            });
        }

        public static async Task<string> FillFirstAvailableId(Int32 id)
        {
            return await App.MobileService.InvokeApiAsync<string>("FillFirstAvailableId/FillFirstAvailableId".ToLower(),
                            HttpMethod.Get,
                            new Dictionary<string, string>()
                            {
                                {"id_in", id.ToString()}
                            });
        }

        public static async Task<GameTable> GetGameTable()
        {
            return await App.MobileService.InvokeApiAsync<GameTable>("GetGameTable/GetGameTable".ToLower(),
                HttpMethod.Get,
                new Dictionary<string, string>()
                {
                });
        }

    }
}
