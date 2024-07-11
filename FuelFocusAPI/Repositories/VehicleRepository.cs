using FuelFocusAPI.Data;
using Microsoft.EntityFrameworkCore;
using FuelFocusApI.Repositories;
using FuelFocusApI.Model;

public class VehicleRepository : IVehicleRepository
{
    private readonly VehicleDbContext _context;

    public VehicleRepository(VehicleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
    {
        return await _context.Tbl_Vehicle_Details.ToListAsync();
    }

    public async Task<Vehicle> GetVehicleByIdAsync(int id)
    {
        return await _context.Tbl_Vehicle_Details.FindAsync(id);
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        _context.Tbl_Vehicle_Details.Add(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
    {
        _context.Entry(vehicle).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return vehicle;
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var vehicle = await _context.Tbl_Vehicle_Details.FindAsync(id);
        if (vehicle != null)
        {
            _context.Tbl_Vehicle_Details.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
