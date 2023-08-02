using System.ComponentModel.DataAnnotations;

namespace DataShare
{
    public class FrontToBack
    {
        [Required]
        public string NameOrNuber { get; set; }
        public string Type { get; set; }

    }
}