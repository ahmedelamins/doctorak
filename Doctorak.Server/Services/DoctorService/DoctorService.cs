
namespace Doctorak.Server.Services.DoctorService;

public class DoctorService : IDoctorService
{
    public Task<ServiceResponse<List<AvailabilitySlot>>> GetAvailabilitySlots(int doctorId)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<AvailabilitySlot>> GetAvailabilitySlot(int doctorId, int slotId)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<AvailabilitySlot>> CreateAvailabilitySlot(int doctorId, AvailabilitySlot slot)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<AvailabilitySlot>> UpdateAvailabilitySlot(int doctorId, int slotId, AvailabilitySlot updatedSlot)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> DeleteAvailabilitySlot(int doctorId, int slotId)
    {
        throw new NotImplementedException();
    }

}
