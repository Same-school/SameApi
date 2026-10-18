namespace SameApi.Dto.User
{
    public class MeResponse : UserResponse
    {
        public string Email { get; set; } = "";
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}