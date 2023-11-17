
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductPro.Models;
using ProductPro.Models.Dto;
using ProductPro.Repository;
using System.Net;

namespace ProductPro.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private APIRespons _respons;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _respons =new APIRespons();  
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequestDto loginRequestDto)
        {
            var loginresponse = await _userRepo.Login(loginRequestDto);
            if (loginresponse.Token == null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _respons.StatusCode = HttpStatusCode.BadRequest;
                _respons.IsSuccess = false;
                _respons.ErrorMessages.Add("UserName or Password is Invalid");
            }
            _respons.IsSuccess = true;
            _respons.StatusCode = HttpStatusCode.OK;
            _respons.Resutl = loginresponse;
            return Ok(_respons);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDto requestDto)
        {
            bool isUnique = _userRepo.IsUniqueUserName(requestDto.UserName);
            if (!isUnique)
            {
                _respons.StatusCode = HttpStatusCode.BadRequest;
                _respons.IsSuccess = false;
                _respons.ErrorMessages.Add("UserName Already Exist");
                return BadRequest(_respons);
            }
            var user =await _userRepo.Register(requestDto);
            if (user == null)
            {
                _respons.StatusCode = HttpStatusCode.BadRequest;
                _respons.IsSuccess = false;
                _respons.ErrorMessages.Add("Server error");
                return BadRequest(_respons);
            }
            _respons.StatusCode = HttpStatusCode.OK;
            _respons.IsSuccess = true;

            return Ok(_respons);
        }

           
    }
}
