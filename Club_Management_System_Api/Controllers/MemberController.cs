using AutoMapper;
using Club_Management_System_Api.DTOs.MemberDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;

        public MemberController(IunitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpGet("GetMemberWithPagignation")]
        public async Task<IActionResult> GetMemberWithPagignation(int pageNumber, int pageSize)
        {
            var items = await _unit.MemberRepo.GetMemberWithPagignation(pageNumber, pageSize);

            if (items is null || items.Count == 0)
                return NotFound("No members found.");

            var DTO = _mapper.Map<List<MemberDTO>>(items);

            return Ok(DTO);
        }

        [HttpGet("GetMemberByName")]
        public async Task<IActionResult> GetMemberByName(string name)
        {
            var items = await _unit.MemberRepo.GetMemberByName(name);

            if (items is null || items.Count == 0)
                return NotFound($"No members found with name: {name}");

            return Ok(items);
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] CreateMemberDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Member data is null.");

            var Entity = _mapper.Map<Member>(DTO);

            await _unit.MemberRepo.CreateAsync(Entity);

            await _unit.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMemberByName), new { name = DTO.MemberFullName }, DTO);
        }

        [HttpPut("UpdateMember/{id}")]
        public async Task<IActionResult> UpdateMember([FromRoute] int id, [FromBody] UpdateMemberDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Member data is null.");

            var existingMember = await _unit.MemberRepo.GetByIdAsync(id);
            if (existingMember == null)
                return NotFound($"Member with ID {id} not found.");


            _mapper.Map(DTO, existingMember);

            await _unit.MemberRepo.UpdateAsync(id ,existingMember);
            await _unit.SaveChangesAsync();
            return NoContent();
        }


    }
}