using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using ThirdL.Controllers;

namespace ThirdL.Models
{
    public class Patient : Object
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
        public ICollection<Comment> Comments { get; set; }
        public int? DoctorId { get; set; }
        [JsonIgnore]
        public virtual Doctor Doctor { get; set;  }
        [JsonIgnore]
        public string Token { get; set; }
    }
}