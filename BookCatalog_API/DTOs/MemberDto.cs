namespace BookCatalog_API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string Role { get; set; } = "User";
}

