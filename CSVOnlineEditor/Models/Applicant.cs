using CSVOnlineEditor.Interfaces;
using System;
using System.Data.SqlTypes;

namespace CSVOnlineEditor.Models
{
    public class Applicant : ICSVSerializable
    {
        public Applicant()
        {
            BirthDate = SqlDateTime.MinValue.Value;
        }

        public Applicant(
            int id, 
            string lastName, 
            string firstName, 
            string middleName,
            DateTime birthDate,
            string email, 
            string phone)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Email = email;
            Phone = phone;

            if (birthDate < SqlDateTime.MinValue.Value)
            {
                birthDate = SqlDateTime.MinValue.Value;
            }

            if (birthDate > SqlDateTime.MaxValue.Value)
            {
                birthDate = SqlDateTime.MaxValue.Value;
            }

            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string[] GetObjectData()
        {
            return new string[]
            {
                LastName + " " + FirstName + " " + MiddleName,
                BirthDate.ToString(),
                Email,
                Phone
            };
        }
    }
}
