using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramBot.Handlers;

namespace TelegramBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();

            TelegramBotClient client = new TelegramBotClient(config["Token"]);

            client.StartReceiving<MainHandler>();

            Console.ReadLine();
        }
    }
}