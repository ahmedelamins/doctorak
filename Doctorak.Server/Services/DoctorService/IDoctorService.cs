namespace Doctorak.Server.Services.DoctorService;

public interface IDoctorService
{
    Task<ServiceResponse<List<AvailabilitySlot>>> FetchAvailabilitySlots(int doctorId);
    Task<ServiceResponse<AvailabilitySlot>> FetchAvailabilitySlot(int doctorId, int slotId);
    Task<ServiceResponse<AvailabilitySlot>> CreateAvailabilitySlot(int doctorId, AvailabilitySlot slot);
    Task<ServiceResponse<AvailabilitySlot>> UpdateAvailabilitySlot(int doctorId, int slotId, AvailabilitySlot updatedSlot);
    Task<ServiceResponse<bool>> DeleteAvailabilitySlot(int doctorId, int slotId);
}
