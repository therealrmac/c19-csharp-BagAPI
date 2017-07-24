using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BagAPI.Models
{
    public class Child
    {
        [Key]
        public int ChildId { get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue (false)]
        public bool Delivered { get; set; }

        ICollection<Toy> Toys;

    }
}