using spendo_be.Context;
using spendo_be.Controllers;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public class UserService : IUserService
{
    private readonly SpendoContext _context = new();

    public bool DoesUserExist(string email)
    {
        return _context.Users.Any(u => u.Email == email);
    }
    
    public User Create(UserDto userInfo)
    {
        var user = new User { Email = userInfo.Email, Name = userInfo.Name, Currencyid = userInfo.CurrencyId};
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
    
    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetUserByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        return user;
    }
}