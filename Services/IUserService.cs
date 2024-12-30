using spendo_be.Controllers;
using spendo_be.Models;

namespace spendo_be.Services;

public interface IUserService
{
    public User Create(UserDto userInfo);
    public User GetUser(int id);
}