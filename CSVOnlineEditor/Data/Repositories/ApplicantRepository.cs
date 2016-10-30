using CSVOnlineEditor.Data.DBContexts;
using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;
using System.Collections.Generic;

namespace CSVOnlineEditor.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly Storage _storage;

        public ApplicantRepository(Storage storage)
        {
            _storage = storage;
        }

        public void Add(ICollection<Applicant> collection)
        {
            _storage.Applicants.AddRange(collection);
            _storage.SaveChanges();
        }

        public void Clean()
        {
            _storage.Applicants.RemoveRange(_storage.Applicants);
            _storage.SaveChanges();
        }

        public IEnumerable<Applicant> Get()
        {
            return _storage.Applicants;
        }

        public void Update(Applicant entity)
        {
            var storedEntity = _storage.Applicants.Update(entity);
            _storage.SaveChanges();
        }
    }
}
