using System.ComponentModel.DataAnnotations;

namespace JoyGClient.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The First Name field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Surname field is required.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "The ID number field is required.")]
        [RegularExpression(@"^(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))",
         ErrorMessage = "Enter a valid South African ID")]
        public string IDNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password field is required.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain only alphanumeric characters")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Confirm Password field is required.")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "The Date Of Birth field is required.")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The Cell Number field is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "The Address field is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The Province field is required.")]
        public string Province { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "The IsEmployed field is required.")]
        public bool IsEmployed { get; set; } = false;
        [Required(ErrorMessage = "The User Role field is required.")]
        public string UserRole { get; set; }
        [Required(ErrorMessage = "The Created By field is required.")]
        public string CreatedBy { get; set; }

    }
}
