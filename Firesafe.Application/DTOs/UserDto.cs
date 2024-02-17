namespace Application.DTOs;

public class UserShortDto
{
    public required Guid UserId { get; init; }
    public List<string> Roles { get; set; } = new();
}

public class UserDto : UserShortDto
{
    public string? Name { get; init; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
}