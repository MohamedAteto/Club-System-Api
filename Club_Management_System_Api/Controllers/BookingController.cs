using AutoMapper;
using Club_Management_System_Api.DTOs.BookingDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;

        public BookingController(IunitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }



        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Booking data is null.");

            var Facility = await _unit.FacilityRepo.GetByIdAsync(DTO.FacilityId);
            if (Facility == null)
                return NotFound($"Facility with ID {DTO.FacilityId} not found.");

            var Member = await _unit.MemberRepo.GetByIdAsync(DTO.MemberId);
            if (Member == null)
                return NotFound($"Member with ID {DTO.MemberId} not found.");



            var booking = _mapper.Map<Booking>(DTO);

            await _unit.BookingRepo.CreateAsync(booking);

            await _unit.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.BoookingId }, booking);
        }


        [HttpGet]
        public async Task<IActionResult> GetBookingSorted(string Status , int PageNumber , int PageSize)
        {
            var booking = await _unit.BookingRepo.GetBookingsFilterdWithStatusUsingPagignation(Status, PageNumber , PageSize);

            if (booking == null)
                return NotFound($"Bookings not found.");

            var DTO = _mapper.Map<List<BookingDTO>>(booking);

            return Ok(DTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _unit.BookingRepo.GetByIdAsync(id);


            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            var DTO = _mapper.Map<BookingDTO>(booking);
            return Ok(DTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(int id ,[FromBody] UpdateBookingDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Booking data is null.");

            var booking = await _unit.BookingRepo.GetByIdAsync(id);

            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            _mapper.Map(DTO, booking);

            await _unit.BookingRepo.UpdateAsync(id,booking);
            await _unit.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _unit.BookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            await _unit.BookingRepo.DeleteAsync(id);
            await _unit.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetBookingReport()
        {
            var bookings = await _unit.BookingRepo.GetBookingsWithFacilityAsync();

            if (bookings is null || !bookings.Any())
                return NotFound("No bookings found to generate a report.");

            var report = bookings
                .Where(b => b.Facility != null)
                .GroupBy(b => b.Facility.FacilityName)
                .Select(g => new FacilityBookingReportDTO
                {
                    FacilityName = g.Key,
                    TotalBookings = g.Count(),
                    TotalGroupSize = g.Sum(b => b.GroupSize)
                })
                .ToList();

            return Ok(report);
        }
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _unit.BookingRepo.GetByIdAsync(id);
            if (booking == null)
                return NotFound($"Booking with ID {id} not found.");

            await _unit.BookingRepo.CancelBooking(id);
            await _unit.BookingRepo.UpdateAsync(id, booking);
            await _unit.SaveChangesAsync();
            return NoContent();
        }

    }
}
