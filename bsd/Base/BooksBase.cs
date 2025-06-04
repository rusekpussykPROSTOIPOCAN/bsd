using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace bsd.Base
{
    [Table("Books")]
    public class BooksBase : BaseModel
    {
        [PrimaryKey("Id",true)]
        public int Id { get; set; }
        [Column("Autor")]
        public string Autor { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Year")]
        public DateTime Year { get; set; }
        [Column("Chapter")]
        public string Chapter { get; set; }
        [Column("InventaryNum")]
        public int InventaryNum { get; set; }
        [Column("condition")]
        public string condition { get; set; }
        [Column("CountBooking")]
        public int CountBooking { get; set; }
        [Column("ISBN")]
        public int ISBN { get; set; }
    }
}
