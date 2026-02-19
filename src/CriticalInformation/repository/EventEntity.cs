namespace CriticalInformation.repository;

public class EventEntity
{
    public required int Id { get; set; }
    public required int HprNumber { get; set; }
    public required string HpName { get; set; }
    public required int PatientId { get; set; }
    public required DateTimeOffset CreateDate { get; set; }
}