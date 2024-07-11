using FuelFocusAPI.Models;
using FuelFocusAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FuelFocusAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripDbContext _dbContext;

        public TripController(TripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] TripDetail tripDetail)
        {
            if (tripDetail == null)
            {
                return BadRequest();
            }

            _dbContext.Tbl_TripDetails.Add(tripDetail);
            await _dbContext.SaveChangesAsync();
            return Ok(tripDetail);
        }
        [HttpGet]
        public List<TripDetail> GetRecords()
        {
            try
            {
                var records = _dbContext.Tbl_TripDetails.ToList();
                return records;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
