using spendo_be.Models;

namespace spendo_be.Services;

public interface IUserService
{
    public User Create(User user);
    public User Create(string email);
    public User GetUser(int id);
}