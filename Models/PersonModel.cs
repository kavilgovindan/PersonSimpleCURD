using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonSimpleCURD.Models
{
    public class PersonModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="this feild is required")]
        [Column(TypeName ="nvarchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "this feild is required")]
        [Column(TypeName = "nvarchar(50)")]
        public int Age { get; set; }  
    }
}
