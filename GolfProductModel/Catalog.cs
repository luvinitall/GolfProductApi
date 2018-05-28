using System;
using System.ComponentModel.DataAnnotations;

namespace GolfProductModel
{
    public class Catalog
    {
        [Key]
        public short CatalogId { get; set; }

        [Required]
        [StringLength(250)]
        //[Index("uidx_Catalog_Description", IsUnique = true)]
        public string Description { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }


    }
}
