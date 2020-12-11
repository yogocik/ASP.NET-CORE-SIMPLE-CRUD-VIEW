using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _11Des2020MVC.DbEntities
{
    public class Books : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public int YearPublished { get; set; }
    }
}
