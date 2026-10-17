namespace SameApi.Dto
{
    public class UserInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public string? BirthDate { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? CoverPictureUrl { get; set; }
        public bool? IsPrivate { get; set; }
        public string? GenderId { get; set; }
        public string? SchoolId { get; set; }
    }
}