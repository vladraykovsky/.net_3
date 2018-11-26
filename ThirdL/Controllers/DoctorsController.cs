using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThirdL.Data;
using ThirdL.Dto;
using ThirdL.Models;
using ThirdL.Services;

namespace ThirdL.Controllers
{
    [Produces("application/json")]
    [Route("api/Doctors")]
    public class DoctorsController : Controller
    {
       
        private IDoctorService _doctorService;
        private IAuthenticateService _authenticateService;
        private readonly ILogger _logger;
        public DoctorsController(
            IDoctorService doctorService,
            ILogger<DoctorsController> logger,
            IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
            _logger = logger;
            _doctorService = doctorService;
        }
        
        [Authorize]
        [HttpGet]
        public IEnumerable<Doctor> GetPatients()
        {
            return _doctorService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var doctor = _doctorService.GetById(id);
            _logger.LogDebug("Bearer "+ doctor.Token);
            _logger.LogDebug(authorization);
            
            if (doctor == null)
            {
                return NotFound();
            }
            
            if (authorization.Equals("Bearer "+doctor.Token))
            {
                return Ok(doctor);
            }
            else
            {
                return BadRequest();
            }
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDoctorByID([FromRoute] int id, [FromBody] Doctor doctor, [FromHeader]string authorization)
        {
            var patientCurrentState = _doctorService.GetById(id);
            
            if (authorization.Equals("Bearer "+patientCurrentState.Token))
            {
                doctor.Token = patientCurrentState.Token;
                _doctorService.Update(doctor);   
                return Ok(doctor);
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.Id)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _doctorService.Create(doctor);
            
            
            return CreatedAtAction("GetCreatedDoctorById", new { id = doctor.Id }, doctor);
        }
        
        
        public async Task<IActionResult> GetCreatedDoctorById([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _doctorService.GetById(id);
            
            if (patient == null)
            {
                return NotFound();
            }
             
            return Ok(patient);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = _doctorService.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }

            if (authorization.Equals("Bearer " + workout.Token))
            {
                _doctorService.Delete(id);   
                return Ok(workout);
            }

            return NotFound();
        }
        
                [HttpPost("{login}")]
                public async Task<IActionResult> PostDoctor([FromBody] PatientDoctor patientDoctor, [FromRoute]string login)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
        
                    var doctor = _doctorService.addPatient(patientDoctor);
                    return CreatedAtAction("GetCreatedDoctorById", new { id = doctor.Id }, doctor);
                }
       
    }
}