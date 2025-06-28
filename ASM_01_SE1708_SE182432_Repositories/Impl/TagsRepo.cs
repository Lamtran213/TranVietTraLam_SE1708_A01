using ASM_01_SE1708_SE182432_Repositories.Def;
using ASM_01_SE1708_SE182432_Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace ASM_01_SE1708_SE182432_Repositories.Impl;

public class TagsRepo : ITagsRepo
{
    private readonly FunewsManagementContext _context;

    public TagsRepo(FunewsManagementContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTags()
    {
        return await _context.Tags.ToListAsync();
    }
}