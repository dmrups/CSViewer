using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;
using System;

namespace CSVOnlineEditor.ObjectAccessors
{
    public class ApplicantAccessor : IAccessor<Applicant>
    {
        private IValueSerializer _serializer;

        public ApplicantAccessor(IValueSerializer serializer)
        {
            _serializer = serializer;
        }

        public string[] GetObjectData(Applicant obj)
        {
            var result = new string[4];
            result[0] = _serializer.SerializeName(
                new Tuple<string, string, string>(obj.LastName, obj.FirstName, obj.MiddleName));

            result[1] = _serializer.SerializeDate(obj.BirthDate);
            result[2] = obj.Email;
            result[3] = obj.Phone;

            return result;
        }
    }
}
