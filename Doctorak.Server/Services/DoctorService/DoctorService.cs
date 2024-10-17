
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
            response.Data = await _context.AvailabilitySlots
                .Where(s => s.DoctorId == doctorId)
                .ToListAsync();

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
            var slot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Id == slotId);

            if (slot == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            response.Data = slot;

            return response;
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
            slot.DoctorId = doctorId;

            _context.AvailabilitySlots.Add(slot);
            await _context.SaveChangesAsync();

            response.Data = slot;
            response.Message = "New slot added";

            return response;
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
            var slot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Id == slotId);

            if (slot == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            slot.Day = updatedSlot.Day;
            slot.Starts = updatedSlot.Starts;
            slot.Ends = updatedSlot.Ends;

            await _context.SaveChangesAsync();

            response.Data = slot;
            response.Message = "Slot updated";

            return response;
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
            var slot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Id == slotId);

            if (slot == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            _context.AvailabilitySlots.Remove(slot);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Slot deleted";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

}
