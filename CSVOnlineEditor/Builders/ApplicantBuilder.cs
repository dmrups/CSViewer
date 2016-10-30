using CSVOnlineEditor.Helpers;
using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;
using System;
using System.Collections.Generic;

namespace CSVOnlineEditor.Builders
{
    class ApplicantBuilder : IBuilder<Applicant>
    {
        public Applicant CreateObject(string[] fields)
        {
            if (fields.Length < 4)
            {
                throw new ArgumentException($"4 values expected, actual: {fields.Length} ");
            }

            var name = ParseHelper.ParseName(fields[0]);

            return new Applicant(
                0,
                name.Item1,
                name.Item2,
                name.Item3,
                ParseHelper.ParseDate(fields[1]),
                ParseHelper.ParseEmail(fields[2]),
                ParseHelper.ParsePhone(fields[3]));
        }

        public Applicant CreateObject(Dictionary<string, string> fields)
        {
            return new Applicant(
                int.Parse(fields[nameof(Applicant.Id)]),
                fields[nameof(Applicant.LastName)],
                fields[nameof(Applicant.FirstName)],
                fields[nameof(Applicant.MiddleName)],
                ParseHelper.ParseDate(fields[nameof(Applicant.BirthDate)]),
                ParseHelper.ParseEmail(fields[nameof(Applicant.Email)]),
                ParseHelper.ParsePhone(fields[nameof(Applicant.Phone)]));
        }
    }
}
