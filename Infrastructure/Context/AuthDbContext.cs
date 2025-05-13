using AuthenticationApiProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApiProject.Infrastructure.Context;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Friend> Friends => Set<Friend>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurations
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Friend>().HasKey(f => f.Id);

        // Static IDs and Hashes for seeding
        var user1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var user4Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var user5Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

        var users = new List<User>
        {
            new() { Id = user1Id, Email = "a@mail.com", PasswordHash = "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX12", Username = "alice" },
            new() { Id = user2Id, Email = "b@mail.com", PasswordHash = "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX34", Username = "bob" },
            new() { Id = user3Id, Email = "c@mail.com", PasswordHash = "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX56", Username = "carol" },
            new() { Id = user4Id, Email = "d@mail.com", PasswordHash = "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX78", Username = "dan" },
            new() { Id = user5Id, Email = "e@mail.com", PasswordHash = "$2a$11$EXAMPLEEXAMPLEEXAMPLEEXAMPLEX90", Username = "erin" }
        };

        modelBuilder.Entity<User>().HasData(users);

        modelBuilder.Entity<Friend>().HasData(new List<Friend>
        {
            new() { Id = Guid.Parse("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), UserId = user1Id, FriendId = user2Id },
            new() { Id = Guid.Parse("aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), UserId = user2Id, FriendId = user1Id },
            new() { Id = Guid.Parse("aaaaaaa3-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), UserId = user3Id, FriendId = user4Id }
        });

        base.OnModelCreating(modelBuilder);
    }
}
