using CriticalInformation.Model;
using CriticalInformation.repository;
using CriticalInformation.Manager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CriticalInformationContext>(options =>
    options.UseInMemoryDatabase("CriticalInformation"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PatientManager>();
builder.Services.AddScoped<EventManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/transparency/{id:int}", (int id, EventManager eventManager) =>
    eventManager.GetEvents(id));

app.MapGet("/patient/{id:int}", async (int id, PatientManager patientManager) =>
{
    var patient = await patientManager.GetPatient(id);

    return Results.Ok(patient);
});

app.MapPost("/patient/{id:int}", async (int id, HPQuery hpQuery, PatientManager patientManager, EventManager eventManager) =>
{
    var patient = patientManager.GetPatient(id);
    
    var eventEntity = new EventEntity
    {
        Id = new Random().Next(),
        HprNumber = hpQuery.HprNumber,
        HpName = hpQuery.HprName,
        PatientId = id,
        CreateDate = DateTimeOffset.Now,
    };

    await eventManager.CreateEvent(eventEntity);
    return Results.Ok(patient);
});

app.MapPost("/patient", async (Patient patient, PatientManager patientManager) =>
{
    var createdPatient = await patientManager.CreatePatient(patient);

    return Results.Created($"/patient/{createdPatient.Id}", createdPatient);
});


app.Run();