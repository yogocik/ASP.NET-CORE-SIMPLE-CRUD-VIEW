using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _11Des2020MVC.DbEntities
{
    public class InventoryMap
    {
        public InventoryMap(EntityTypeBuilder<Inventories> entityBuilder)
        {
            entityBuilder.HasKey(t => t.InventID);
            entityBuilder.Property(t => t.LenderID).IsRequired();
            entityBuilder.Property(t => t.BookID).IsRequired();
            entityBuilder.Property(t => t.LendingDate).IsRequired();
        }
    }
}
