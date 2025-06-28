using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;

namespace TranVietTraLamMVC_Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepo _categoryRepo;

    public CategoryService(ICategoryRepo categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<GetAllCategory>> GetCategoriesAsync()
    {
        var categories = await _categoryRepo.GetCategoriesAsync();
        return categories.Select(category => new GetAllCategory
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDesciption = category.CategoryDesciption,
            ParentCategoryId = category.ParentCategoryId,
            IsActive = category.IsActive
        }).ToList();
    }

    public async Task<IEnumerable<GetAllCategory>?> GetCategoryByIdAsync(short id)
    {
        var category = await _categoryRepo.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return null;
        }
        return new List<GetAllCategory>
        {
            new()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDesciption = category.CategoryDesciption,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive
            }
        };
    }

    public async Task<Category> AddCategoryAsync(AddCategoryResponse addCategory)
    {
        var category = new Category
        {
            CategoryName = addCategory.CategoryName,
            CategoryDesciption = addCategory.CategoryDesciption,
            ParentCategoryId = addCategory.ParentCategoryId,
            IsActive = addCategory.IsActive
        };
        return await _categoryRepo.AddCategoryAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(UpdateCategoryResponse updateCategory)
    {
        var category = new Category
        {
            CategoryName = updateCategory.CategoryName,
            CategoryDesciption = updateCategory.CategoryDesciption,
            ParentCategoryId = updateCategory.ParentCategoryId,
            IsActive = updateCategory.IsActive
        };
        return await _categoryRepo.UpdateCategoryAsync(category);
    }

    public Task DeleteCategoryAsync(short id)
    {
        return _categoryRepo.DeleteCategoryAsync(id);
    }
}