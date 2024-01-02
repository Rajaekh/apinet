using LesApi.Models;
using LesApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
           _studentService = studentService;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            return _studentService.Get();
        }

       
        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            var student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound($"Student with Id={id} not found");
            }
            return student;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {
            _studentService.Create(student);
            return CreatedAtAction(nameof(Get),new {id=student.Id}, student);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Student student)
        {
            var existingStudent= _studentService.GetStudent(id);
            if(existingStudent == null)
            {
                return NotFound($"Student with Id ={id} not found ");
            }
            _studentService.Update(id, student);
            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public  ActionResult Delete(string id)
        {
         var student=_studentService.GetStudent(id);
            if(student == null)
            {
                return NotFound($"Student with Id={id} not Found");
            }
            _studentService.Remove(student.Id);
            return Ok($"Student with Id={id} deleted");
        }
    }
}
