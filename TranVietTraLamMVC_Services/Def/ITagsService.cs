using TranVietTraLamMVC_Services.DTO.Request;

namespace TranVietTraLamMVC_Services.Def;

public interface ITagsService
{
    public Task<IEnumerable<TagsRequest>> GetAllTags();
}