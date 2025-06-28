using ASM_01_SE1708_SE182432_Services.DTO.Request;

namespace ASM_01_SE1708_SE182432_Services.Def;

public interface ITagsService
{
    public Task<IEnumerable<TagsRequest>> GetAllTags();
}