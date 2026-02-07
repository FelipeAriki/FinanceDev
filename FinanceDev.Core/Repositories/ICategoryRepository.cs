using FinanceDev.Core.Models;

namespace FinanceDev.Core.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
}
