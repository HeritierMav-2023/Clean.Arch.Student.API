using Clean.Arch.Student.Application.IRepository;
using Clean.Arch.Student.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Arch.Student.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //1-reference repository
        private readonly IStudentRepository _studentRepository;


        //2- Constructeur
        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;  
        }

        #region Methods Verbs
        [HttpGet]
        public ActionResult<Domain.Entities.Student> GetStudentDetails()
        {
            var data = _studentRepository.GetAll();

            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Domain.Entities.Student>>GetData(int Id)
        {
            return await _studentRepository.GetbyId(Id);

        }
        [HttpPost]
        public async Task<ActionResult<int>>PostStudent(Domain.Entities.Student student)
        {
            if (student == null)
            {
                return NotFound();
            }
            return await _studentRepository.Add(student);
        }
        [HttpPut]
        public async Task<ActionResult<int>> PutStudent(Domain.Entities.Student student)
        {
            try
            {
                if (student.Id == 0)
                    return 0;
                else
                    return await _studentRepository.Update(student);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

        }
        [HttpDelete("DeleteStudent/{id}")]
        public  async Task<int> Delete(int id)
        { 
            var entity = await _studentRepository.GetbyId(id);
            if (entity == null)
                return 0;
            return await _studentRepository.Delete(id);
        }
        #endregion
    }
}
