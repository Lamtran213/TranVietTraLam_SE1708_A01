using TranVietTraLam_Repositories.Models;

namespace TranVietTraLamMVC_Services.DTO.Response;

public sealed class AddNewsArticleResponse
{
    public string? NewsTitle { get; set; }

    public string Headline { get; set; } = null!;
    public DateTime? CreatedDate { get; set; } = DateTime.Now;

    public string? NewsContent { get; set; }

    public string? NewsSource { get; set; }

    public short? CategoryId { get; set; }

    public bool? NewsStatus { get; set; }

    public short? CreatedById { get; set; }

    public short? UpdatedById { get; set; }

    public Category? Category { get; set; }

    public SystemAccount? CreatedBy { get; set; }
    public List<int> SelectedTagIds { get; set; } = [];
}