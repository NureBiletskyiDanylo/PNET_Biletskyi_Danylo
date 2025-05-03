namespace BookCatalog_API.DTOs
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
        public required string Role { get; set; }
    }
}
