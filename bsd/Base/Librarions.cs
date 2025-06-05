using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{
        [Table("librarions")]
    public class Librarions:BaseModel
    {
      
            [PrimaryKey("id", false)]
            public int Id { get; set; }
            [Column("firstname")]
            public string firstname { get; set; }
            [Column("lastname")]
            public string lastname { get; set; }
            [Column("patronymic")]
            public string patronymic { get; set; }
            [Column("login")]
            public string login { get; set; }
            [Column("pass")]
            public string pass { get; set; }
            [Column("id_role")]
            public int id_role { get; set; }
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
