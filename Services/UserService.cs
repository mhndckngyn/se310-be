using spendo_be.Context;
using spendo_be.Models;

namespace spendo_be.Services;

public class UserService : IUserService
{
    private readonly SpendoContext _context = new();
    
    public User Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User Create(string email)
    {
        var user = new User { Email = email };
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetUser(int id)
    {
        var user = _context.Users.Find(id);
        return user;
    }
}