using System;
using System.Collections.Generic;
using System.Text;

namespace GolfProductApi.Entities
{


    public class CatalogCategory
    {
        public short CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; }

        public short CategoryId { get; set; }
        public virtual  Category Category { get; set; }

        
    }
}
