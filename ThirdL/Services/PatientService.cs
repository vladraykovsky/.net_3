using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ThirdL.Data;
using ThirdL.Models;

namespace ThirdL.Services
{
    
    public interface IPatientService
    {
        Patient Authenticate(string username, string password);
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        Patient Create(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
        bool CheckToken(string token);
    }
    
    public class PatientService : IPatientService
    {
        
        private readonly PatientContext _patientContext;

        public PatientService(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }


        public Patient Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var patient = _patientContext.Patients.SingleOrDefault(p => p.Login.Equals(login));

            if (patient == null)
            {
                    return null;
            }
            if (!VerifyPasswordHash(password))
                    return null;

            return patient;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientContext.Patients;
        }

        public Patient GetById(int id)
        {
            return _patientContext.Patients
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);
        }

        public Patient Create(Patient patient)
        {
           
            _patientContext.Patients.Add(patient);
            _patientContext.SaveChanges();

            return patient;
        }

        public void Update(Patient patient)
        {
            
            var entity = _patientContext.Patients.Find(patient.Id);
            _patientContext.Entry(entity).CurrentValues.SetValues(patient);
            _patientContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var entity = _patientContext.Patients.Find(id);
            _patientContext.Attach(entity);
            _patientContext.Remove(entity);
            _patientContext.SaveChanges();
        }
        
        private static bool VerifyPasswordHash(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
 
           
            return true;
        }

        public bool CheckToken(string token)
        {
            return true;
        }
        
    }
}