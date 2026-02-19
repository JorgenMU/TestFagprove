using CriticalInformation.Model;
using CriticalInformation.repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CriticalInformationContext>(options =>
    options.UseInMemoryDatabase("CriticalInformation"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/patient/{id:int}", async (int id, CriticalInformationContext context) =>
{
    var patientEntity = await context.Patients.FindAsync(id);
    if (patientEntity == null) return Results.NotFound();

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

    var patient = new Patient
    {
        Id = patientEntity.Id,
        Name = patientEntity.Name,
        DateOfBirth = patientEntity.DateOfBirth,
        CriticalInformation = criticalInformationList
    };

    return Results.Ok(patient);
});

app.MapPost("/patient", async (Patient patient, CriticalInformationContext context) =>
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

    return Results.Created($"/patient/{patient.Id}", patient);
});


app.Run();