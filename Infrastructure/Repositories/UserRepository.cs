using AuthenticationApiProject.Application.Interfaces;
using AuthenticationApiProject.Domain.Entities;
using AuthenticationApiProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace AuthenticationApiProject.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;
    public UserRepository(AuthDbContext context) => _context = context;

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.Include(u => u.Friends).FirstOrDefaultAsync(u => u.Id == id);

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
