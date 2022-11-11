using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Gaines_Opus_Institute_Current.Models
{
    [Index(nameof(email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A username is required.")]
        [MinLength(2, ErrorMessage = "Username must be atleast 2 characters")]
        [MaxLength(15, ErrorMessage = "Username must be atmost 15 characters")]
        [RegularExpression("^(?=[a-zA-Z0-9._]{3,20}$)(?!.*[_.]{2})[^_.].*[^_.]$", ErrorMessage = "Enter a valid username.")]
        public string username { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Password must be 6 characters long and include a capital, lowercase, special character, and digit.")]
        public string password { get; set; }

        [Required(ErrorMessage = "An email is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[a-zA-Z0-9!#$%&'*+/=?^_‘{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_‘{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", ErrorMessage = "Must be a valid email adress.")]
        public string email { get; set; }

        //policy
        //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
        //public bool TermsAndConditions { get; set; }	

        [Required]
        public bool rememberMe { get; set; } = true;

        [Required(ErrorMessage = "A instrument is required.")]
        [MinLength(2, ErrorMessage = "Instrumet must be atleast 2 characters")]
        [MaxLength(10, ErrorMessage = "Instrument must be atmost 10 characters")]
        public string instrument { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }
        public int tokens { get; set; }


    }
}
