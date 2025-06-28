using Microsoft.EntityFrameworkCore;
using TranVietTraLam_Repositories.Def;
using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Impl;

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