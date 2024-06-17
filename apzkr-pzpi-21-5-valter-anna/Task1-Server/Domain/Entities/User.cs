namespace Domain.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? Age { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string? PasswordHash { get; set; }

    public string? PasswordSalt { get; set; }
}
