using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSVOnlineEditor.Interfaces;
using CSVOnlineEditor.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CSVOnlineEditor.Controllers
{
    //[Route("api/[controller]")]
    public class CSVController : Controller
    {
        private readonly IApplicantRepository _repository;
        private readonly ICSVSerializer _serializer;
        private readonly IBuilder<Applicant> _builder;
        private readonly IAccessor<Applicant> _accessor;

        public CSVController(IApplicantRepository repository, ICSVSerializer serializer, 
            IBuilder<Applicant> builder, IAccessor<Applicant> accessor)
        {
            _repository = repository;
            _serializer = serializer;
            _builder = builder;
            _accessor = accessor;
        }

        [HttpGet]
        [Route("api/csv/applicants")]
        public object GetApplicants()
        {
            return _serializer.Serialize(_repository.Get(), _accessor);
        }

        [HttpPost]
        [Route("api/csv/applicants")]
        public void PostApplicants([FromBody]string[] files)
        {
            var data = new List<Applicant>();

            foreach (var file in files)
            {
                data.AddRange(_serializer.Deserialize(file, _builder));
            }

            _repository.Add(data);
        }
    }
}
