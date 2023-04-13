namespace TelegramBot.Models
{
    public class User
    {
        public ulong Id { get; private set; }

        public string LastName { get; private set; }

        public string FirstName { get; private set; }

        public string PhoneNumber { get; private set; }

        public List<Company>? Companies { get; private set; }

        public User(string lastName, string firstName, string phoneNumber)
        {
            Id = default;
            LastName = lastName ?? string.Empty;
            FirstName = firstName ?? string.Empty;
            PhoneNumber = phoneNumber ?? string.Empty;
            Companies = new List<Company>();
        }
    }
}