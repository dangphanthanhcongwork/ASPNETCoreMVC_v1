using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public enum Gender
    {
        Empty,
        Male,
        Female,
        Other
    }
    public class Person
    {
        [Display(Order = 1)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "First name", Order = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name", Order = 3)]
        public string LastName { get; set; }

        [Display(Order = 4)]
        public string FullName { get { return LastName + " " + FirstName; } }

        [Required]
        [Display(Name = "Gender", Order = 5)]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Date of birth", Order = 6)]
        public DateTime DateOfBirth { get; set; }

        [Display(Order = 7)]
        public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }

        [Display(Order = 8)]
        public string? Birthplace { get; set; }

        [Required]
        [Display(Name = "Phone number", Order = 9)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Is graduated", Order = 10)]
        public bool IsGraduated { get; set; }

        public override string ToString()
        {
            return $"Name: {FullName}, Gender: {Gender}, Date of Birth: {DateOfBirth}, Birthplace: {Birthplace}, Phone number: {PhoneNumber}, Graduated: {IsGraduated}";
        }
    }
}
