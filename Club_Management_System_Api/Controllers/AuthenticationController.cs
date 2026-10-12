using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Club_Management_System_Api.DTOs.AuthDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Club_Management_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IunitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthenticationController(IunitOfWork unit, IMapper mapper,IConfiguration config)
        {
            _unit = unit;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO DTO)
        {
            if (DTO is null)
                return  BadRequest("isn't good");

            var item = await _unit.UserRepo.GetUserByUserName(DTO.UserName);

            if (item != null)
                return BadRequest("This user is already exist !!");

            var EntityUser = _mapper.Map<User>(DTO);

            //var passwordHasher = new PasswordHasher<User>();
            //EntityUser.Password = passwordHasher.HashPassword(EntityUser, DTO.Password);

            await _unit.UserRepo.CreateAsync(EntityUser);

            await _unit.SaveChangesAsync();
            return Ok("Created Successfully" + EntityUser);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO DTO)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }

            if (DTO is null)
                return BadRequest();

            var item = await _unit.UserRepo.GetUserByUserName(DTO.UserName);

            if (item is null)
                return BadRequest();

            if (DTO.Password != item.Password)
                return BadRequest("Passwrod doesn't match ");

            var token = GenerateToken(item);

            return Ok(token);
        }

        private string GenerateToken(User item)
        {
            var KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(KEY, SecurityAlgorithms.HmacSha256);

            var Claims = new List<Claim>()
            {

                new Claim(ClaimTypes.NameIdentifier , item.Id.ToString()),
                new Claim(ClaimTypes.Name , item.UserName),
                new Claim(ClaimTypes.Role , item.Role)
            };


            var Token = new JwtSecurityToken(
                issuer : _config["JWT:Issuer"],
                audience : _config["JWT:Audience"],
                expires : DateTime.Now.AddHours(1),
                claims : Claims,
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
