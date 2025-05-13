namespace AuthenticationApiProject.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Username { get; set; } = null!;
    public byte[]? ProfileImage { get; set; } 
    public ICollection<Friend> Friends { get; set; } = new List<Friend>();
}