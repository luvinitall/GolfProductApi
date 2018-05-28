using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfProductModel
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Required]
        [StringLength(250)]
        //[Index("uidx_Catalog_Description", IsUnique = true)]
        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();


    }

    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Required]
        [StringLength(250)]
        //[Index("uidx_Catalog_Description", IsUnique = true)]
        public string Description { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }

        [ForeignKey("CatalogId")]
        public Catalog Catalog { get; set; }
        public short CatalogId { get; set; }

    }
}
