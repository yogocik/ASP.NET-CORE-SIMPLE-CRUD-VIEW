using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _11Des2020MVC.DbEntities

{
    public class BookMap
    {
        public BookMap(EntityTypeBuilder<Books> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Author).IsRequired();
            entityBuilder.Property(t => t.Title).IsRequired();
            entityBuilder.Property(t => t.YearPublished).IsRequired();
        }
    }
}
