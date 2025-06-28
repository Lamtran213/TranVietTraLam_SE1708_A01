using ASM_01_SE1708_SE182432_Repositories.Def;
using ASM_01_SE1708_SE182432_Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_01_SE1708_SE182432_Repositories.Impl;

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
        var checkId = await GetCategoryByIdAsync(category.CategoryId);
        if (checkId == null)
        {
            throw new KeyNotFoundException("Category not found.");
        }
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
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