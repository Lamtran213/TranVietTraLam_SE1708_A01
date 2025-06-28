namespace TranVietTraLamMVC_Services.DTO.Response;

public class UpdateSystemAccountResponse
{
    public short AccountId { get; set; }
    public string? AccountName { get; set; }

    public string? AccountEmail { get; set; }

    public int? AccountRole { get; set; }
}