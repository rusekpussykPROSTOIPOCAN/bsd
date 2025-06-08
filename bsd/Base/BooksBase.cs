using Supabase;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace bsd.Base
{
    [Table("Books")]
    public class BooksBase : BaseModel
    {
        [PrimaryKey("Id",false)]
        public int Id { get; set; }
        [Column("Autor")]
        public string Autor { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Year")]
        public DateOnly Year { get; set; }
        [Column("Chapter")]
        public string Chapter { get; set; }
        [Column("InventaryNum")]
        public string InventaryNum { get; set; }
        [Column("condition")]
        public string condition { get; set; }
        [Column("CountBooking")]
        public int CountBooking { get; set; }
        [Column("ISBN")]
        public string ISBN { get; set; }
       
    }
}
