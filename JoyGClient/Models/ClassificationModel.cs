using System.ComponentModel.DataAnnotations;

namespace JoyGClient.Models
{
    public class ClassificationModel
    {
        [Required(ErrorMessage = "The Classification Name field is required.")]
        public string ClassificationName { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Id { get; set; }
    }
}
