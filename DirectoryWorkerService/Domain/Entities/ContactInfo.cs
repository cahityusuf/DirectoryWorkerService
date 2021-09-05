using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Data;

namespace Domain.Entities
{
    [Table("ContactInfo", Schema = "DirectoryContactInfo")]
    public class ContactInfo : IEntity
    {
        public long Id { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public ContactType ContactType { get; set; }
        public long ContactTypeId { get; set; }
        public string Description { get; set; }

    }
}
