using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _11Des2020MVC.DbEntities
{
    public class Students : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
    }
}
