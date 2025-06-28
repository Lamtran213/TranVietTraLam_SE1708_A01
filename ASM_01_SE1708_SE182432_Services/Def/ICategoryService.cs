using ASM_01_SE1708_SE182432_Repositories.Models;
using ASM_01_SE1708_SE182432_Services.DTO.Request;
using ASM_01_SE1708_SE182432_Services.DTO.Response;

namespace ASM_01_SE1708_SE182432_Services.Def;

public interface ICategoryService
{
    public Task<IEnumerable<GetAllCategory>> GetCategoriesAsync();
    public Task<IEnumerable<GetAllCategory>?> GetCategoryByIdAsync(short id);
    public Task<Category> AddCategoryAsync(AddCategoryResponse category);
    public Task<Category> UpdateCategoryAsync(UpdateCategoryResponse category);
    public Task DeleteCategoryAsync(short id);
}