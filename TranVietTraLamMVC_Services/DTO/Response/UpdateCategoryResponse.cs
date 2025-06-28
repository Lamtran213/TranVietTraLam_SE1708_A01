namespace TranVietTraLamMVC_Services.DTO.Response;

public class UpdateCategoryResponse
{
    public short CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    public string CategoryDesciption { get; set; } = null!;

    public short? ParentCategoryId { get; set; }

    public bool? IsActive { get; set; }
}