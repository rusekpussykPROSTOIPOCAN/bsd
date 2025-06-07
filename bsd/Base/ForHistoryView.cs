using Supabase.Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Base
{
    [Table("countries_view")]
    class ForHistoryView
    {
        [Column("DateOfIssueOrBooking")]
        public DateOnly DateOfIssueOrBooking { get; set; }
        [Column("dateOfDel")]
        public string dateOfDel { get; set; }
        [Column("BookingOrExtradition")]
        public string BookingOrExtradition { get; set; }
        [Column("End")]
        public DateTime End { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Autor")]
        public int Autor { get; set; }
        [Column("PrelimDateOfDel")]
        public string PrelimDateOfDel { get; set; }

    }
}
