using System;
using System.Collections.Generic;
using System.Text;

namespace GolfProductApi.Entities
{


    public class CatalogCategory
    {
        public short CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        public short CategoryId { get; set; }
        public Category Category { get; set; }

        
    }
}
