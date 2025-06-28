using TranVietTraLam_Repositories.Def;
using TranVietTraLamMVC_Services.Def;
using TranVietTraLamMVC_Services.DTO.Request;

namespace TranVietTraLamMVC_Services.DTO.Response;

public class TagsService : ITagsService
{
    private readonly ITagsRepo _tagsRepo;

    public TagsService(ITagsRepo tagsRepo)
    {
        _tagsRepo = tagsRepo;
    }

    public async Task<IEnumerable<TagsRequest>> GetAllTags()
    {
        var tags = await _tagsRepo.GetAllTags();
        return tags.Select(tag => new TagsRequest
        {
            TagId = tag.TagId,
            TagName = tag.TagName
        }).ToList();
    }
}