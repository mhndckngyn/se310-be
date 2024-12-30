using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public class CategoryService : ICategoryService
{
    private readonly SpendoContext _context = new();
    
    public List<CategoryDto> GetCategories()
    {
        return _context.Categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();
    }
}