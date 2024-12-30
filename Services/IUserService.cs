using spendo_be.Controllers;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public interface IUserService
{
    public User Create(UserDto userInfo);
    public User GetUserById(int id);
    public User? GetUserByEmail(string email);
}