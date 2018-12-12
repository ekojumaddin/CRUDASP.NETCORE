using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core1.Models
{
    public class Item : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        [ForeignKey("Supplier")] 
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
