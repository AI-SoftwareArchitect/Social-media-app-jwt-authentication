namespace AuthenticationApiProject.Domain.Entities;

public class Friend
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
}