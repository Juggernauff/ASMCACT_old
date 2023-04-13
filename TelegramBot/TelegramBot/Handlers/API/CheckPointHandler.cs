using Newtonsoft.Json;
using System.Net.Http.Json;
using TelegramBot.Models;

namespace TelegramBot.Handlers.API
{
    public class CheckPointHandler
    {
        public async Task<bool> Open(CheckPoint checkPoint)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                JsonContent jsonContent = JsonContent.Create(checkPoint);
                var response = await httpClient.PostAsync("https://localhost:7172/api/CheckPoint/Open", jsonContent);
                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
        }

        public async Task<CheckPoint> GetCheckPoint(CheckPoint checkPoint)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                JsonContent jsonContent = JsonContent.Create(checkPoint);
                var response = await httpClient.PostAsync("https://localhost:7172/api/CheckPoint/GetCheckPoint", jsonContent);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                CheckPoint result = JsonConvert.DeserializeObject<Models.CheckPoint>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
        }
    }
}
