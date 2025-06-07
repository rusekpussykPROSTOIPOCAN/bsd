using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{

    [Table("Reader")]
    public class Readers:BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("Fname")]
        public string Fname { get; set; }
        [Column("Lname")]
        public string Lname { get; set; }
        [Column("Pass")]
        public string Pass { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("SeriaAndNum")]
        public string SeriaAndNum { get; set; }
        [Column("Sex")]
        public string Sex { get; set; }
        [Column("IsueByWhom")]
        public string IsueByWhom { get; set; }
        [Column("Code")]
        public string Code { get; set; }
        [Column("DateOfIssue")]
        public DateTime DateOfIssue { get; set; }
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
