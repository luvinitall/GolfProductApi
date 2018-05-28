using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfProductApi.Entities
{
    public class Category
    {
        [Key]
        public short CategoryId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        //public virtual ICollection<Family> Families { get; set; }

        public virtual ICollection<CatalogCategory> CatalogCategories { get; set; }

    }
}