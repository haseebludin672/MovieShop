using ApplicationCore.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email should not be empty")]
        [EmailAddress(ErrorMessage = "Email should be in right format")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name should not be empty!")]
        [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name should not be empty!")]
        [StringLength(50, ErrorMessage = "FirstName cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password Name should not be empty!")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage =
            "Password Should have minimum 8 with at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Date of Birth should not be empty")]
        // year should not be less than 1900
        // Minimum age should be 15
        [MinimumAllowedYear]
        public DateTime DateOfBirth { get; set; }
    }

}
