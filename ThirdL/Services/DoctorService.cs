using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThirdL.Controllers;
using ThirdL.Data;
using ThirdL.Dto;

namespace ThirdL.Services
{
    
    public interface IDoctorService
    {
        Doctor Authenticate(string username, string password);
        IEnumerable<Doctor> GetAll();
        Doctor GetById(int id);
        Doctor Create(Doctor patient);
        void Update(Doctor patient);
        void Delete(int id);
        bool CheckToken(string token);
        Doctor addPatient(PatientDoctor patientDoctor);
    }
    
    
    
    public class DoctorService : IDoctorService
    {
        
        private readonly PatientContext _patientContext;


        public DoctorService(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        public Doctor Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var doctor = _patientContext.Doctors.SingleOrDefault(d => d.Login.Equals(login));

            if (doctor == null)
            {
                return null;
            }
            if (!VerifyPasswordHash(password))
                return null;

            return doctor;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _patientContext.Doctors;
        }

        public Doctor GetById(int id)
        {
            return _patientContext.Doctors
                .Include(d => d.Patients)
                .SingleOrDefault(p => p.Id == id);
        }

        public Doctor Create(Doctor doctor)
        {
            _patientContext.Doctors.Add(doctor);
            _patientContext.SaveChanges();

            return doctor;
        }

        public void Update(Doctor doctor)
        {
            var entity = _patientContext.Doctors.Find(doctor.Id);
            _patientContext.Entry(entity).CurrentValues.SetValues(doctor);
            _patientContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _patientContext.Doctors.Find(id);
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
        
        public Doctor addPatient(PatientDoctor patientDoctor)
        {
            var doctor = _patientContext.Doctors.SingleOrDefault(d => d.Login.Equals(patientDoctor.DoctorLogin));
            var patient = _patientContext.Patients.SingleOrDefault(p => p.Login.Equals(patientDoctor.PatientLogin));
            patient.DoctorId = doctor.Id;
            _patientContext.SaveChanges();
            return doctor;
        }
        
        public bool CheckToken(string token)
        {
            return _patientContext.Doctors.Any(d => d.Token == token);
        }
    }
}