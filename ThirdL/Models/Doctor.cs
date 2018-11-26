using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using ThirdL.Models;

namespace ThirdL.Controllers
{
    public class Doctor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Patient> Patients{ get; set; } 
        [JsonIgnore]
        public string Token { get; set; }
    }
}