using TranVietTraLam_Repositories.Models;

namespace TranVietTraLamMVC_Services.DTO.Response;

public class UpdateNewsArticleResponse
{
    public string NewsArticleId { get; set; } = null!;
    public string? NewsTitle { get; set; }

    public string Headline { get; set; } = null!;
    public string? NewsContent { get; set; }

    public string? NewsSource { get; set; }

    public short? CategoryId { get; set; }

    public bool? NewsStatus { get; set; }
    public short? UpdatedById { get; set; }

    public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;

    public virtual Category? Category { get; set; }

    public virtual SystemAccount? CreatedBy { get; set; }
    public List<int> SelectedTagIds { get; set; } = [];
    
}