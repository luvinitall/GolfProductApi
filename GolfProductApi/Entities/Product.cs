using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfProductApi.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        //TODO: Add test to validate the regex
        [Required]
        [StringLength(18)]
        [RegularExpression("^([a-zA-Z0-9]+)$")]
        public string Sku { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [ForeignKey("FamilyId")]
        public int FamilyId { get; set; }
        public virtual Family Family { get; set; }

        public Gender Gender { get; set; }


        public Hand Hand { get; set; }
    }
}