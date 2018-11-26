using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ThirdL.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTimeOffset CreationData { get; set;  }
        public string FirstName
        {
            get; set;
        }
        public string LastName {
            get; set;
        }
        public int PatientId { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set;  }
    }
}