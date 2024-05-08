namespace WebApplication.Models
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class Person
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
        public required Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsGraduated { get; set; }
    }
}