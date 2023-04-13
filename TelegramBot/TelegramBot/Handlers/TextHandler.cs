using System.Data.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.API;
using TelegramBot.Models;

namespace TelegramBot.Handlers
{
    public class TextHandler
    {
        public async Task HandleTextAsync(ITelegramBotClient botClient, Message? message)
        {
            if (botClient == null || message == null) return;

            if (message.Text.Contains('/'))
            {
                CheckPointHandler handler = new CheckPointHandler();
                
                CheckPoint checkPoint = await handler.GetCheckPoint(new CheckPoint(message.Text.Split('/')[1].Trim()));
                
                if (checkPoint == null) await botClient.SendTextMessageAsync(message.Chat.Id, "Неизвестная ошибка, попробуйте позже!", replyMarkup: null);

                bool result = await handler.Open(checkPoint);

                List<KeyboardButton> buttons = new List<KeyboardButton>();
                buttons.Add(new KeyboardButton(message.Text));

                if (result)
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Пост {message.Text} открыт!", replyMarkup: new ReplyKeyboardMarkup(buttons));
                }
                else
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Произошла неизвестная ошибка. Пожалуйста, попробуйте позже!", replyMarkup: new ReplyKeyboardMarkup(buttons));
                }
            }
            return;
        }
    }
}
