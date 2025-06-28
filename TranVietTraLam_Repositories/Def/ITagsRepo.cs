using TranVietTraLam_Repositories.Models;

namespace TranVietTraLam_Repositories.Def;

public interface ITagsRepo
{
    public Task<IEnumerable<Tag>> GetAllTags();
}