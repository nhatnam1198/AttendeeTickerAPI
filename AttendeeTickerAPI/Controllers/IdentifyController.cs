using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AttendeeTickerAPI.DAL;
using AttendeeTickerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.Face;

namespace AttendeeTickerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentifyController : ControllerBase
    {
        private readonly AttendeeTickerDbContext _context;

        public IdentifyController(AttendeeTickerDbContext context)
        {
            _context = context;
        }
        IFaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials("4cecff50281e457eb50cdcbaed557c62")) { Endpoint = "https://attendeefacerecogservice.cognitiveservices.azure.com/" };

        string personGroupId = "utc_students";
        // POST: api/Identify
        [HttpPost]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> IdentifyStudent(IFormFile file)
        {
            List<StudentDTO> personList = new List<StudentDTO>();
            using (Stream stream = file.OpenReadStream())
            {
                var faces = await faceClient.Face.DetectWithStreamAsync(stream);
                var faceIds = faces.Select(face => face.FaceId.Value).ToArray();
                var results = await faceClient.Face.IdentifyAsync(faceIds, personGroupId);
                foreach(var identifyResult in results)
                {
                    if(identifyResult.Candidates.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        var candidateId = identifyResult.Candidates[0].PersonId;
                        var person = await faceClient.PersonGroupPerson.GetAsync(personGroupId, candidateId);
                        var student = _context.Student.Where(s => s.PersonID == person.PersonId.ToString()).Take(1).ToList();
                        personList.Add(new StudentDTO()
                        {
                            StudentID = student[0].StudentID,
                            IsAttended = true
                        });
                    }
                }
            }
            return personList;
        }
    }
}