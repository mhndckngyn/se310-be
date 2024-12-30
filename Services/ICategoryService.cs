using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public interface ICategoryService
{
    public List<CategoryDto> GetCategories();
}