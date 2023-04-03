using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Company
    {
        [Key]
        public ulong Id { get; private set; }

        [Column("FullName", TypeName = "nvarchar(MAX)")]
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