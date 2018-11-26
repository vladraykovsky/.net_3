using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThirdL.Data;
using ThirdL.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ThirdL.Helpers;
using ThirdL.Services;

namespace ThirdL.Controllers
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Controller
    {
        private IPatientService _patientService;
        private IAuthenticateService _authenticateService;    
        
        public PatientsController(  
            IPatientService patientService,
            IAuthenticateService authenticateService)
        {
            _patientService = patientService;
         //   _modelService = modelService;
            _authenticateService = authenticateService;

        }

        
        [HttpGet]
        public IEnumerable<Patient> GetPatients([FromHeader] string authorization)
        {
            if (checkDoctorToken(authorization))
            {
                return _patientService.GetAll();
            }
            return new List<Patient>();
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient =  _patientService.GetById(id);
               
            
            if (patient == null)
            {
                return NotFound();
            }
            
            if (authorization.Equals("Bearer "+patient.Token) || checkDoctorToken(authorization))
            {
                return Ok(patient);
            }
            else
            {
                return BadRequest();
            }
           
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatintByID([FromRoute] int id, [FromBody] Patient patient, [FromHeader] string authorization)
        {
            var patientCurrentState = _patientService.GetById(id);
            
            if (authorization.Equals("Bearer "+patientCurrentState.Token))
            {
                patient.Token = patientCurrentState.Token;
                _patientService.Update(patient);   
                return Ok(patient);
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Id)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        public async Task<IActionResult> GetCreatedPatientById([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _patientService.GetById(id);
            
            if (patient == null)
            {
                return NotFound();
            }
             
            return Ok(patient);
        }
      
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _patientService.Create(patient);
            
            
            return CreatedAtAction("GetCreatedPatientById", new { id = patient.Id }, patient);
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id, [FromHeader] string authorization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = _patientService.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }

            if (authorization.Equals("Bearer " + workout.Token))
            {
                _patientService.Delete(id);   
                return Ok(workout);
            }

            return NotFound();
        }
        
        
//        private bool PatientExists(int id)
//        {
//            return _patientContext.Patients.Any(e => e.Id == id);
//        }

        private bool checkDoctorToken(string token)
        {
            string[] tokens = token.Split(' ');
            return _patientService.CheckToken(tokens[0]);
        }

    }
}