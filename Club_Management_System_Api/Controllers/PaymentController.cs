using AutoMapper;
using Club_Management_System_Api.DTOs.PaymentDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;

        public PaymentController(IunitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            var Entity = _mapper.Map<Payment>(payment);

            await _unit.PaymentRepo.CreateAsync(Entity);

            await _unit.SaveChangesAsync();

            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _unit.PaymentRepo.GetAllPaymentsAsync();
            if(payments is null)
                return BadRequest(ModelState);

            var DTO = _mapper.Map<IEnumerable<PaymentStatisticsDTO>>(payments); //////// not importnant
            return Ok(DTO) ;
        }
    }
}
