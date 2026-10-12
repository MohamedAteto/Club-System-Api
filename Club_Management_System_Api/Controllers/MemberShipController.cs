using AutoMapper;
using Club_Management_System_Api.DTOs.MemberShipDTOs;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberShipController : ControllerBase
    {
        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;

        public MemberShipController(IunitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("GetActiveMemberShips")]
        public async Task<IActionResult> GetActiveMemberShips()
        {
            var items = await _unit.MemberShipRepo.GetActiveMemberShipsAsync();

            if (items is null || items.Count == 0)
                return NotFound("No active memberships found.");

            var DTO = _mapper.Map<ICollection<MemberShipDTO>>(items);
            return Ok(DTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberShipDTO DTO)
        {
            if (DTO is null)
                return BadRequest("MemberShip data is null.");

            var Member = await _unit.MemberRepo.GetByIdAsync(DTO.MemberId);
            if (Member is null)
                return NotFound("Member not found.");

            var Entity = _mapper.Map<Models.MemberShip>(DTO);

            await _unit.MemberShipRepo.CreateAsync(Entity);

            await _unit.SaveChangesAsync();

            var createdDTO = _mapper.Map<MemberShipDTO>(Entity);

            return CreatedAtAction(nameof(GetActiveMemberShips), new { id = Entity.MemberShipId }, createdDTO);
        }

        [HttpPatch]
        public async Task<IActionResult> RenewMemberShipPayment(int MemberShipId)
        {
           var item  = await _unit.MemberShipRepo.GetByIdAsync(MemberShipId);
            if (item is null)
                return NotFound("Membership not found.");


            await _unit.MemberShipRepo.Renew_MemberShipPaymentAsync(MemberShipId);
            await _unit.SaveChangesAsync();

            return Ok("Membership payment renewed successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMemberShip(int MemberShipId)
        {
            var item = await _unit.MemberShipRepo.GetByIdAsync(MemberShipId);
            if (item is null)

                return NotFound("Membership not found.");



            await _unit.MemberShipRepo.DeleteAsync(item.MemberShipId);
            await _unit.SaveChangesAsync();

            return Ok("Membership deleted successfully.");
        }

    }
}
