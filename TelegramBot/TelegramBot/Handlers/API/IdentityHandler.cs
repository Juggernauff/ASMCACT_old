using Newtonsoft.Json;
using System.Net.Http.Json;
using Telegram.Bot;

namespace TelegramBot.Handlers.API
{
    public class IdentityHandler
    {
        public async Task<Models.User> Register(Models.User user)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                JsonContent jsonContent = JsonContent.Create(user);
                var response = await httpClient.PostAsync("https://localhost:7172/api/Identity/Register", jsonContent);
                Models.User result = JsonConvert.DeserializeObject<Models.User>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public async Task<Models.User> Login(Models.User user)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                JsonContent jsonContent = JsonContent.Create(user);
                var response = await httpClient.PostAsync("https://localhost:7172/api/Identity/Login", jsonContent);
                Models.User result = JsonConvert.DeserializeObject<Models.User>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }

        public async Task<bool> UserExists(Models.User user)
        {
            try
            {
                Models.User result = await Login(user);
                return result != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
