using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;

namespace TranVietTraLamMVC_Services.Def;

public interface ICategoryService
{
    public Task<IEnumerable<GetAllCategory>> GetCategoriesAsync();
    public Task<IEnumerable<GetAllCategory>?> GetCategoryByIdAsync(short id);
    public Task<Category> AddCategoryAsync(AddCategoryResponse category);
    public Task<Category> UpdateCategoryAsync(UpdateCategoryResponse category);
    public Task DeleteCategoryAsync(short id);
}