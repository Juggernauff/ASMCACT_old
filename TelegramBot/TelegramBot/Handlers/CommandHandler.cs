using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Handlers
{
    public class CommandHandler
    {
        private Models.User _user;

        public CommandHandler(Models.User user) 
        {
            _user = user;
        }

        public async Task HandleCommandAsync(ITelegramBotClient botClient, Message? message)
        {
            if (botClient == null || message == null) return;

            if (message.Text == "/start")
            {
                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[] 
                    {
                        KeyboardButton.WithRequestContact("Зарегистрироваться / Авторизироваться")
                    }
                });

                botClient.SendTextMessageAsync(message.Chat.Id, "Чтобы использовать телеграм бота, войдите в систему!", replyMarkup: keyboard);
            }
            return;
        }
    }
}
