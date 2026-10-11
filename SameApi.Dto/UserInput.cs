namespace SameApi.Dto
{
    public class UserInput
    {
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPictureUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public bool? IsPrivate { get; set; }
        public string? GenderId { get; set; }
        public string? SchoolId { get; set; }
    }
}