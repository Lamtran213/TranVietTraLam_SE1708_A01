using TranVietTraLam_Repositories.Models;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;
using TranVietTraLamMVC_Services.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace TranVietTraLamMVC.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        if (!categories.Any())
        {
            return NotFound("No categories found.");
        }
        return View("Category", categories);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategory(short id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null || !category.Any())
        {
            return NotFound($"Category with ID {id} not found.");
        }
        return View("Category", category);
    }
    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryResponse category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddCategoryAsync(category);
            return RedirectToAction("GetCategories");
        }

        var allCategories = await _categoryService.GetCategoriesAsync();

        ViewBag.AddCategoryModel = category;

        return View("Category", allCategories);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryResponse category)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
                return RedirectToAction("GetCategories");
            }

            ViewBag.UpdateCategoryModel = category;
            var allCategories = await _categoryService.GetCategoriesAsync();
            return View("Category", allCategories);
        }
        catch (KeyNotFoundException)
        {
            ViewBag.Error = "Category ID not found.";
            var allCategories = await _categoryService.GetCategoriesAsync();
            return View("Category", allCategories);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Unexpected error: {ex.Message}";
            var allCategories = await _categoryService.GetCategoriesAsync();
            return View("Category", allCategories);
        }
    }
    [HttpPost]
    public async Task<IActionResult> DeleteCategory(short id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Category with ID {id} not found.");
        }
    }
}