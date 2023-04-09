using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [Key]
        public ulong Id { get; private set; }

        [Column("LastName", TypeName = "nvarchar(50)")]
        public string LastName { get; private set; }

        [Column("FirstName", TypeName = "nvarchar(50)")]
        public string FirstName { get; private set; }

        [Column("PhoneNumber", TypeName = "varchar(50)")]
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