namespace CollegeApp.Model


{
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;

    public class studentDTO
    {
        [ValidateNever]
        public int studentID { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        [StringLength(100)]
        public string name { get; set; }

        [Range(10,30)]
        public int age { get; set; }
        [EmailAddress(ErrorMessage ="Enter Mail")]
        public string email { get; set; }

        //[SpaceCheck]
        public string Password { get; set; }
        [Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
    }
}
