using CriticalInformation.Model;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/patient/{id:int}", (int id) =>
{
    var patient = new Patient
    {
        Id = id,
        Name = "Ola Normann",
        DateOfBirth = DateTimeOffset.Now,
        CriticalInformation = []
    };

    return patient;
});

app.Run();

















