using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ThirdL.Data;
using ThirdL.Helpers;

namespace ThirdL.Services
{


    public interface IAuthenticateService
    {
        void setPatientToken(int Id);
        void setDoctorToken(int Id);
    }
    
    
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly PatientContext _patientContext;
       
       
        public AuthenticateService(IOptions<AppSettings> appSettings,
                                   PatientContext patientContext)
        {
            _patientContext = patientContext;
            _appSettings = appSettings.Value;
            
            
        }


        public void setPatientToken(int Id)
        {
            var patient = _patientContext.Patients.SingleOrDefault(p => p.Id == Id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            patient.Token = tokenString;
            _patientContext.SaveChanges();
        }
        
        
        public void setDoctorToken(int Id)
        {
            var doctor = _patientContext.Doctors.SingleOrDefault(p => p.Id == Id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            doctor.Token = tokenString;
            _patientContext.SaveChanges();
        }
        
        


    }
}