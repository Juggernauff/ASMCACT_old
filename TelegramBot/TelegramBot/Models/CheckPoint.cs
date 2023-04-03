namespace API.Models
{
    public class CheckPoint
    {
        public ulong Id { get; private set; }

        public string Name { get; private set; }

        public ulong CompanyId { get; private set; }

        public Company? Company { get; private set; }

        public CheckPoint(string name, ulong companyId)
        {
            Name = name;
            CompanyId = companyId;
        }
    }
}
