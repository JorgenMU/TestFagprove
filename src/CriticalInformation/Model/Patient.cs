namespace CriticalInformation.Model;

public class Patient
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTimeOffset DateOfBirth { get; set; }
    //The list is required but it can be empty.
    public required List<CriticalInformation?> CriticalInformation { get; set; }
}