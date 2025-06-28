using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Def;

public interface ICategoryRepo
{
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(short id);
    public Task<Category> AddCategoryAsync(Category category);
    public Task<Category> UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(short id);
}