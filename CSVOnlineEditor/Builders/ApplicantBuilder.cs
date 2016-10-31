using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;
using System;
using System.Collections.Generic;

namespace CSVOnlineEditor.Builders
{
    class ApplicantBuilder : IBuilder<Applicant>
    {
        private IValueParser _parser;

        public ApplicantBuilder(IValueParser parser)
        {
            _parser = parser;
        }

        public Applicant CreateObject(string[] fields)
        {
            if (fields.Length < 4)
            {
                throw new ArgumentException($"4 values expected, actual: {fields.Length} ");
            }

            var name = _parser.ParseName(fields[0]);

            return new Applicant()
            {
                LastName = name.Item1,
                FirstName = name.Item2,
                MiddleName = name.Item3,
                BirthDate = _parser.ParseDate(fields[1]),
                Email = _parser.ParseEmail(fields[2]),
                Phone = _parser.ParsePhone(fields[3])
            };
        }

        public Applicant CreateObject(Dictionary<string, string> fields)
        {
            return new Applicant()
            {
                Id = int.Parse(fields[nameof(Applicant.Id)]),
                LastName = fields[nameof(Applicant.LastName)],
                FirstName = fields[nameof(Applicant.FirstName)],
                MiddleName = fields[nameof(Applicant.MiddleName)],
                BirthDate = _parser.ParseDate(fields[nameof(Applicant.BirthDate)]),
                Email = _parser.ParseEmail(fields[nameof(Applicant.Email)]),
                Phone = _parser.ParsePhone(fields[nameof(Applicant.Phone)])
            };
        }
    }
}
