namespace API.Models
{
    public class Company
    {
        public ulong Id { get; private set; }

        public string FullName { get; private set; }

        public List<User>? Users { get; private set; }

        public List<CheckPoint>? CheckPoints { get; private set; }

        public Company(string fullName)
        {
            Id = default;
            FullName = fullName;
        }
    }
}
