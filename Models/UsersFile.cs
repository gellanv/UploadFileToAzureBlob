using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace TestFileUpload.Models
{
    public class UsersFile
    {     
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a file")]
        [FileExtensions(Extensions = ".docx", ErrorMessage = "Please select a file with valid format .docx.")]
        public string FilePath { get; set; }
        public IBrowserFile FileObject { get; set; }
    }
}
