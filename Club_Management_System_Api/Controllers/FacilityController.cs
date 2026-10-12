using AutoMapper;
using Club_Management_System_Api.DTOs.FacilityDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;

        public FacilityController(IunitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }


        [HttpGet("SortedByField")]
        public async Task<IActionResult> GetSortedFacilities()
        {
            var facilities = await _unit.FacilityRepo.GetAllFacilitiesSortedAsync();

            if(facilities == null || !facilities.Any())
                return NotFound("No facilities found.");

            var DTO = _mapper.Map<List<FacilityDTO>>(facilities);

            return Ok(DTO);
        }

        [HttpPost("AddFacility")]
        public async Task<IActionResult> AddFacility([FromBody] CreateFacilityDTO facilityDTO)
        {
            if (facilityDTO == null)
                return BadRequest("Facility data is null.");


            var facilityEntity = _mapper.Map<Facility>(facilityDTO);

            await _unit.FacilityRepo.CreateAsync(facilityEntity);

            await _unit.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSortedFacilities), new { id = facilityEntity.FacilityId }, facilityDTO);
        }

        [HttpDelete("DeleteFacility/{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facility = await _unit.FacilityRepo.GetByIdAsync(id);
            if (facility == null)
                return NotFound($"Facility with ID {id} not found.");

            if(facility.Bookings != null && facility.Bookings.Any())
                return BadRequest("Cannot delete facility with existing bookings.");

            _unit.FacilityRepo.DeleteAsync(id);
            await _unit.SaveChangesAsync();
            return NoContent();
        }
    }
}
