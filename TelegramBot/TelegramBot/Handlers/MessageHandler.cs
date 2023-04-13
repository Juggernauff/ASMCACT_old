using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot.Handlers.API;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Handlers
{
    public class MessageHandler
    {
        private const char _prefix = '/';

        private Models.User _user;

        public MessageHandler(Models.User user)
        {
            _user = user;
        }

        private bool IsCommand(string str) => str.StartsWith(_prefix);

        public async Task HandleMessageAsync(ITelegramBotClient botClient, Message? message)
        {
            if (botClient == null || message == null) return;

            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                if (IsCommand(message.Text))
                {
                    CommandHandler handler = new CommandHandler(_user);
                    await handler.HandleCommandAsync(botClient, message);
                }
                else
                {
                    TextHandler handler = new TextHandler();
                    await handler.HandleTextAsync(botClient, message);
                }
            }
            else if (message.Contact != null && _user == null)
            {
                if (message.Contact.UserId == message.From.Id)
                {
                    Models.User user = new Models.User(message.Contact.LastName, message.Contact.FirstName, message.Contact.PhoneNumber);

                    IdentityHandler handler = new IdentityHandler();

                    if (handler.UserExists(user).Result)
                        _user = await handler.Login(user);
                    else
                        _user = await handler.Register(user);

                    List<KeyboardButton> buttons = new List<KeyboardButton>();

                    if (_user.Companies.Any())
                    {
                        foreach (Models.Company company in _user.Companies)
                        {
                            string companyName = company.FullName;
                            string fullName = string.Empty;

                            if (!company.CheckPoints.Any()) continue;

                            foreach (Models.CheckPoint checkPoint in company.CheckPoints)
                            {
                                fullName = companyName + " / " + checkPoint.Name;
                                buttons.Add(new KeyboardButton(fullName));
                            }
                        }
                    }

                    botClient.SendTextMessageAsync(message.Chat.Id,
                        $"Вы вошли как {message.Contact.LastName} {message.Contact.FirstName}.",
                        replyMarkup: new ReplyKeyboardMarkup(buttons));

                    Console.WriteLine($"Вошел: {_user.LastName} {_user.FirstName} с номером {_user.PhoneNumber}");
                }
            }
            return;
        }
    }
}