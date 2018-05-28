using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfProductApi.Entities
{
    public class Catalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short CatalogId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public virtual ICollection<CatalogCategory> CatalogCategories { get; } = new List<CatalogCategory>();


    }
}
