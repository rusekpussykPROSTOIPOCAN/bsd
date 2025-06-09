using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{
    [Table("cool")]
    public class ForHistoryView:BaseModel
    {
        [Column("id_reader")]
        public int id_reader { get; set; }
        [Column("DateOfIssueOrBooking")]
        public DateOnly DateOfIssueOrBooking { get; set; }
        [Column("dateOfDel")]
        public DateOnly dateOfDel { get; set; }
        [Column("BookingOrExtradition")]
        public bool BookingOrExtradition { get; set; }
        [Column("End")]
        public bool End { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Autor")]
        public string Autor { get; set; }
        [Column("PrelimDateOfDel")]
        public DateOnly PrelimDateOfDel { get; set; }

    }
}
