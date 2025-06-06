using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsd.Models
{
    //Потом исправлю эту хуйню
    internal class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int SerialAndNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Sex { get; set; }
        public string IssuedByWhom { get; set; }
        public string UnitCode { get; set; }

    }
}
