
namespace Doctorak.Server.Services.DoctorService;

public class DoctorService : IDoctorService
{
    private readonly DataContext _context;

    public DoctorService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<AvailabilitySlot>>> FetchAvailabilitySlots(int doctorId)
    {
        var response = new ServiceResponse<List<AvailabilitySlot>>();

        try
        {
            var slots = await _context.AvailabilitySlots
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();

            response.Data = slots;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<AvailabilitySlot>> FetchAvailabilitySlot(int doctorId, int slotId)
    {
        var response = new ServiceResponse<AvailabilitySlot>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<AvailabilitySlot>> CreateAvailabilitySlot(int doctorId, AvailabilitySlot slot)
    {
        var response = new ServiceResponse<AvailabilitySlot>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<AvailabilitySlot>> UpdateAvailabilitySlot(int doctorId, int slotId, AvailabilitySlot updatedSlot)
    {
        var response = new ServiceResponse<AvailabilitySlot>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<bool>> DeleteAvailabilitySlot(int doctorId, int slotId)
    {
        var response = new ServiceResponse<bool>();

        try
        {

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

}
