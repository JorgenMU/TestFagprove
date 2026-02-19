using Microsoft.EntityFrameworkCore;

namespace CriticalInformation.repository;

public class CriticalInformationContext(DbContextOptions<CriticalInformationContext> options) : DbContext(options)
{
    public DbSet<PatientEntity> Patients { get; set; }
    public DbSet<CriticalInformationEntity> CriticalInformation { get; set; }
    public DbSet<EventEntity> Events { get; set; }
}