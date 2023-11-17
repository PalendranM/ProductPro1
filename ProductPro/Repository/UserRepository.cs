using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductPro.Data;
using ProductPro.Models;
using ProductPro.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductPro.Repository
{
    
    public class UserRepository : IUserRepository
    {
        private readonly ProductDbContext _db;
        private readonly IMapper _mapper;
        private string secretKey;

        public UserRepository(ProductDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _db = dbContext;
            _mapper = mapper;
            secretKey = configuration.GetValue<String>("ApiSettings:Secret");
        }
        public bool IsUniqueUserName(string userName)
        {
            var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName == loginRequestDto.UserName 
            && u.Password == loginRequestDto.Password);

            if (user == null)
            {

                return new LoginResponseDto
                {
                    Token = "",
                    User = null

                };
            }
            // ---------------- JWT Token -------------------
            var tokenheader = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescridtor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim (ClaimTypes.Name,user.Id.ToString()),
                        new Claim (ClaimTypes.Role,user.Role)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenheader.CreateToken(tokenDescridtor);

            var loginResponse = new LoginResponseDto
            {
                Token = tokenheader.WriteToken(token),
                User = user

            };
            return loginResponse;
        }

        public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
        {
           LocalUser user = _mapper.Map<LocalUser>(registrationRequestDto);
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();

            user.Password = "";
            return user;
            

        }
    }
}
