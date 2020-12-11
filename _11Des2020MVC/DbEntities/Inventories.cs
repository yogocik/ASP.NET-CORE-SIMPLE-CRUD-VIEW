using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _11Des2020MVC.DbEntities
{
    public class Inventories
    {
        public string InventID { get; set; }
        public string LenderID { get; set; }
        public string BookID { get; set; }
        public DateTime LendingDate { get; set; }
        public DateTime DueDate { get; set; }

    }
}
