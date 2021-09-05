using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstractions.Data;

namespace Domain.Entities
{
    [Table("User", Schema = "DirectoryUser")]
    public class User : IEntity
    {
        public User()
        {
            ContactInfo = new HashSet<ContactInfo>();
        }
        public long Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public virtual ICollection<ContactInfo> ContactInfo { get; set; }
    }
}
