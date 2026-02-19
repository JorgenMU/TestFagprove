namespace CriticalInformation.repository;

public class CriticalInformationEntity
{
    public required int Id { get; set; }
    public required string Type { get; set; }
    public required string Information { get; set; }
    public required int PatientId { get; set; }
}