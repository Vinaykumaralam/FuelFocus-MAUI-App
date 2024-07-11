using FuelFocusApI.Model;
using FuelFocusApI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]/[Action]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehiclesController(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
    {
        return Ok(await _vehicleRepository.GetVehiclesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
    {
        await _vehicleRepository.AddVehicleAsync(vehicle);
        return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.VehicleID }, vehicle);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehicle(int id, [FromBody] Vehicle vehicle)
    {
        if (id != vehicle.VehicleID)
        {
            return BadRequest();
        }

        await _vehicleRepository.UpdateVehicleAsync(vehicle);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        await _vehicleRepository.DeleteVehicleAsync(id);
        return NoContent();
    }

    [HttpPut("UploadImage/{id}")]
    public async Task<IActionResult> UploadImage(int id, IFormFile carImage = null, IFormFile userProfileImage = null)
    {
        try
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound(new { Message = "Vehicle not found" });
            }

            if (carImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await carImage.CopyToAsync(memoryStream);
                    vehicle.CarImage = memoryStream.ToArray();
                }
            }

            if (userProfileImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await userProfileImage.CopyToAsync(memoryStream);
                    vehicle.UserProfileImage = memoryStream.ToArray();
                }
            }

            await _vehicleRepository.UpdateVehicleAsync(vehicle);
            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while uploading the image", Error = ex.Message });
        }
    }



}