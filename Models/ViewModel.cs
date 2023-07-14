using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineTest.Models
{
    public class ViewModel
    {
        [Key]
        public int productid { get; set; }
        [Required]
        public string productname { get; set; } = default!;

        public int categoryid { get; set; }
        [ForeignKey("categoryid")]
        public category category { get; set; } = default!;

        [Required]
        public string categoryname { get; set; } = default!;
    }
}
