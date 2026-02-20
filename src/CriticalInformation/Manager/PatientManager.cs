using CriticalInformation.Model;
using CriticalInformation.repository;

namespace CriticalInformation.Manager;

public class PatientManager(CriticalInformationContext context)
{
    public async Task<Patient?> GetPatient(int id)
    {
        var patientEntity = await context.Patients.FindAsync(id);
        if (patientEntity == null) return null;

        var criticalInfoEntities = context.CriticalInformation.Where(criticalInfo =>
            criticalInfo.PatientId == id).ToList();

        var criticalInformationList = new List<CriticalInformation.Model.CriticalInformation?>();

        if (criticalInfoEntities.Count is not 0)
        {
            criticalInformationList.AddRange(criticalInfoEntities.Select(criticalInfoEntity =>
                new CriticalInformation.Model.CriticalInformation
                {
                    Id = criticalInfoEntity.Id,
                    Type = criticalInfoEntity.Type,
                    Information = criticalInfoEntity.Information,
                }));
        }

        return new Patient
        {
            Id = patientEntity.Id,
            Name = patientEntity.Name,
            DateOfBirth = patientEntity.DateOfBirth,
            CriticalInformation = criticalInformationList
        };
    }

    public async Task<Patient> CreatePatient(Patient patient)
    {
        var patientEntity = new PatientEntity
        {
            Id = patient.Id,
            Name = patient.Name,
            DateOfBirth = patient.DateOfBirth,
        };
        context.Patients.Add(patientEntity);

        var criticalInfoEntities = patient.CriticalInformation.Select(criticalInfo =>
            new CriticalInformationEntity
            {
                Id = criticalInfo!.Id,
                Type = criticalInfo.Type,
                Information = criticalInfo.Information,
                PatientId = patient.Id
            });

        context.CriticalInformation.AddRange(criticalInfoEntities);

        await context.SaveChangesAsync();
        
        return patient;
    }
}