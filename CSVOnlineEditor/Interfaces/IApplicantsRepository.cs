using CSVOnlineEditor.Models;
using System.Collections.Generic;

namespace CSVOnlineEditor.Interfaces
{
    public interface IApplicantRepository
    {
        void Add(ICollection<Applicant> collection);
        void Update(Applicant entity);
        void Clean();

        IEnumerable<Applicant> Get();
    }
}
