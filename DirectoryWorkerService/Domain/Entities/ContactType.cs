using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Data;

namespace Domain.Entities
{
    [Table("ContactType", Schema = "DirectoryContactType")]
    public class ContactType : IEntity
    {
        public ContactType()
        {
            ContactInfo = new HashSet<ContactInfo>();
        }
        public long Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
    }
}
