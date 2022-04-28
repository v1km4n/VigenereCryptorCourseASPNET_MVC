using System.ComponentModel.DataAnnotations;

namespace VigenereCryptorCourseMVC.Models
{
    public class SingleFileModel 
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
        [Required(ErrorMessage = "Please enter file name")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
    }
}
