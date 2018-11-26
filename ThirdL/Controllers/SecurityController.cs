using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ThirdL.Controllers;
using ThirdL.Data;
using ThirdL.Dto;
using ThirdL.Helpers;
using ThirdL.Models;
using ThirdL.Services;

namespace DefaultNamespace
{
    [Produces("application/json")]
    public class SecurityController : Controller
    {
       // private IModelService _modelService;
        private IAuthenticateService _authenticateService;
        private IPatientService _patientService;
        private IDoctorService _doctorService;

        public SecurityController(
                                    IOptions<AppSettings> appSettings,
                                    IDoctorService doctorService,
                                    IPatientService patientService,
            IAuthenticateService authenticateService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _authenticateService = authenticateService;
        }
        
                
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginDto userDto)
        {
            var user = _patientService.Authenticate(userDto.Login, userDto.Password);
 
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            _authenticateService.setPatientToken(user.Id);
                
            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.Name,
                LastName = user.Surname,
                Token = user.Token
            });
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate/doctor")]
        public IActionResult AuthenticateDoctor([FromBody]LoginDto userDto)
        {

            var user = _doctorService.Authenticate(userDto.Login, userDto.Password);
 
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            _authenticateService.setDoctorToken(user.Id);
                
            // return basic user info (without password) and token to store client side
            return Ok(new {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.Name,
                LastName = user.Surname,
                Token = user.Token
            });
        }
        
        
    }
}