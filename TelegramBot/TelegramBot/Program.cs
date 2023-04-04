using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace TelegramBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
        }
    }
}