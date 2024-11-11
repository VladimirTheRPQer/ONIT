using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebONIT.Data;
using WebONIT.Managers;
using WebONIT.Requests;

namespace WebONIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private StudentManager _studentManager;
        public StudentsController( StudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        [HttpPost("create")]
        public async Task<Student> AddStudent(CreateStudentRequest createStudentRequest)
        {
            return await _studentManager.AddStudent(createStudentRequest);
        }
        [HttpPost("read")]
        public async Task<List<Student>> GetStudents(Filter filter)
        {
            return await _studentManager.GetStudents(filter);
        }
        [HttpDelete("delete/{studentId:int}")]
        public async Task<bool> DeleteStudent([FromRoute] int studentId)
        {
            return await _studentManager.DeleteStudent(studentId);
        }
    }
}
