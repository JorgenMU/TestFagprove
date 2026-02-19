namespace CriticalInformation.repository;

public class PatientEntity
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
}