using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{
    [Table("History")]
    public class History:BaseModel
    {
        [PrimaryKey("id_reader", false)]
        public int Id { get; set; }
        [Column("DateOfIssueOrBooking")]
        public DateTime DateOfIssueOrBooking { get; set; }
        [Column("dateOfDel")]
        public DateTime dateOfDel { get; set; }
        [Column("BookingOrExtradition")]
        public bool BookingOrExtradition { get; set; }
        [Column("End")]
        public bool End { get; set; }
        [Column("id_book")]
        public int id_book { get; set; }
       
    }
}
