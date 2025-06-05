using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{
    [Table("Role")]
    public class Role:BaseModel
    {
        [PrimaryKey("id_role", false)]
        public int Id { get; set; }
        [Column("Role")]
        public string role { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Readers message &&
                    Id == message.Id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
