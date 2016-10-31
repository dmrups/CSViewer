using CSVOnlineEditor.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Data.SqlTypes;

namespace CSVOnlineEditor.Models
{
    public class Applicant
    {
        public Applicant()
        {
            BirthDate = SqlDateTime.MinValue.Value;
        }
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        [JsonConverter(typeof(DateOnly))]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
