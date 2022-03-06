using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string  Name { get; set; }
        public int MovieID { get; set; }

        //navigation property
        //kind of property that navigate from one propety to another property

        public Movie Movie { get; set; }
        
    }
}
