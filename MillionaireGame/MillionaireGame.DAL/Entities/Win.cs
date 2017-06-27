using System.ComponentModel.DataAnnotations;

namespace MillionaireGame.DAL.Entities
{
    public class Win
    {
        [Key]
        [Range(1, 15, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Level { get; set; }

        public int Sum { get; set; }
    }
}
