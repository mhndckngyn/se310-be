using spendo_be.Context;
using spendo_be.Models;

namespace spendo_be.Services;

public class CategoryService : ICategoryService
{
    private readonly SpendoContext _context = new();
    
    public List<Category> GetCategories()
    {
        return _context.Categories.ToList();
    }
}