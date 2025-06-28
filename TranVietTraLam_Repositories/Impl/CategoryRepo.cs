using Microsoft.EntityFrameworkCore;
using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Impl;

public class CategoryRepo : ICategoryRepo
{
    private readonly FunewsManagementContext _context;

    public CategoryRepo(FunewsManagementContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(short id)
    {
        return await _context.Categories
            .Where(a => a.CategoryId == id)
            .FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Category not found.");
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();

        return await Task.FromResult(category);
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(category.CategoryId);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }

        existingCategory.CategoryName = category.CategoryName;
        existingCategory.CategoryDesciption = category.CategoryDesciption;
        existingCategory.ParentCategoryId = category.ParentCategoryId;
        existingCategory.IsActive = category.IsActive;

        await _context.SaveChangesAsync();
        return existingCategory;
    }
    public async Task DeleteCategoryAsync(short id)
    {
        var deleteId = await GetCategoryByIdAsync(id);
        if (deleteId == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }
        _context.Categories.Remove(deleteId);
        await _context.SaveChangesAsync();
    }
}