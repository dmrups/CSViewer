using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CSVOnlineEditor.Controllers
{
    [Route("api/[controller]")]
    public class ApplicantsController : Controller
    {
        private readonly IApplicantRepository _repository;
        private readonly IBuilder<Applicant> _builder;

        public ApplicantsController(IApplicantRepository repository, IBuilder<Applicant> builder)
        {
            _repository = repository;
            _builder = builder;
        }

        [HttpGet]
        public object Get()
        {
            return _repository.Get();
        }

        [HttpPut("{id}")]
        public object Put(int id, [FromBody]Dictionary<string, string> fields)
        {
            fields = fields.ToDictionary(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase);
            var entity = _builder.CreateObject(fields);
            _repository.Update(entity);

            return entity;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _repository.Clean();
        }
    }
}
