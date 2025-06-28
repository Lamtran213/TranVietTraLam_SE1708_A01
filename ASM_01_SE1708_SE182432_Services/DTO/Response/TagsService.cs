using ASM_01_SE1708_SE182432_Repositories.Def;
using ASM_01_SE1708_SE182432_Services.Def;
using ASM_01_SE1708_SE182432_Services.DTO.Request;

namespace ASM_01_SE1708_SE182432_Services.DTO.Response;

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