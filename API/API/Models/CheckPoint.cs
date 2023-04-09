using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class CheckPoint
    {
        [Key]
        public ulong Id { get; private set; }

        [Column("Name", TypeName = "nvarchar(MAX)")]
        public string Name { get; private set; }

        [Column("CompanyId", TypeName = "decimal(20, 0)")]
        public ulong CompanyId { get; private set; }

        public Company? Company { get; private set; }

        public CheckPoint(string name)
        {
            Name = name;
            CompanyId = default;
            Company = default;
        }
    }
}