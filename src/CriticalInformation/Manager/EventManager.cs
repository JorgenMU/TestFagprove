using CriticalInformation.repository;

namespace CriticalInformation.Manager;

public class EventManager(CriticalInformationContext context)
{
    public async Task CreateEvent(EventEntity eventEntity)
    {
        context.Add(eventEntity);
        await context.SaveChangesAsync();
    }

    public IEnumerable<EventEntity> GetEvents(int patientId)
    {
        return context.Events.Where(eventEntity => 
            eventEntity.PatientId == patientId);
    }
}