using spendo_be.Models;

namespace spendo_be.Services;

public interface ICategoryService
{
    public List<Category> GetCategories();
}