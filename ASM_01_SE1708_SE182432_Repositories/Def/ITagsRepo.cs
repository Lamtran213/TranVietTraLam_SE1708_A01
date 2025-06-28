using ASM_01_SE1708_SE182432_Repositories.Models;

namespace ASM_01_SE1708_SE182432_Repositories.Def;

public interface ITagsRepo
{
    public Task<IEnumerable<Tag>> GetAllTags();
}