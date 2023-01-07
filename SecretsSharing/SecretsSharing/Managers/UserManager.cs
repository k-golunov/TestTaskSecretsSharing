using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecretsSharing.DTO;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// Get instance from di
        /// </summary>
        /// <param name="userRepository">DAL</param>
        /// <param name="configuration">configuration app</param>
        /// <param name="mapper">map models in DTO</param>
        public UserManager(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Generate access token and save data in database with userRepository
        /// </summary>
        /// <param name="model">Authenticate model</param>
        /// <returns>AuthenticateResponse</returns>
        public AuthenticateResponse Authenticate(AuthModel model)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
                return null;
            

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        /// <summary>
        /// Register user and create AuthenticateResponse with method Authenticate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AuthenticateResponse> Register(AuthModel model)
        {
            var userModel = _mapper.Map<User>(model);
            var addedUser = await _userRepository.AddAsync(userModel);
            
            var response = Authenticate(new AuthModel
            {
                Email = userModel.Email,
                Password = userModel.Password
            });
            
            return response;
        }

        public User GetById(Guid id) => _userRepository.GetById(id);
        
        /// <summary>
        /// Generate new Jwt Token use secret key in configuration and algorithms HmacSha256
        /// </summary>
        /// <param name="user"></param>
        /// <returns>new jwt token</returns>
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}