using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1.Models
{
    public class ModelMap
    {
        public ModelMap(EntityTypeBuilder<Supplier> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
        }

        public ModelMap(EntityTypeBuilder<Item> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.Property(t => t.Price).IsRequired();
            entityBuilder.Property(t => t.Stock).IsRequired();
        }
    }
}
