using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;
using University.BL.DTOs;
using University.BL.Models;

namespace University.Web.Controllers
{
    public class CourseInstructorController : ApiController
    {
        private IMapper mapper;
        private readonly CourseInstructorService courseInstructorService = new CourseInstructorService(new CourseInstructorRepository(UniversityContext.Create()));

        public CourseInstructorController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        //Devuelve todos los cursos(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var coursesInstrutors = await courseInstructorService.GetAll();
            var coursesInstrutorsDTO = coursesInstrutors.Select(x => mapper.Map<CourseInstructorDTO>(x));
            return Ok(coursesInstrutorsDTO);//status code 200
        }

        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var courseInstrutor = await courseInstructorService.GetById(id);
            var courseInstrutorDTO = mapper.Map<CourseInstructorDTO>(courseInstrutor);
            return Ok(courseInstrutorDTO);//status code 200
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(CourseInstructorDTO courseInstrutorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var courseInstrutor = new CourseInstructorDTO
            //{
            //    ID = coursesInstrutorsDTO.ID,
            //    CourseID = coursesInstrutorsDTO.CourseID,
            //    InstructorID = coursesInstrutorsDTO.InstructorID
            //};

            try
            {
                var courseInstrutor = mapper.Map<CourseInstructor>(courseInstrutorDTO);
                courseInstrutor = await courseInstructorService.Insert(courseInstrutor);
                return Ok(courseInstrutorDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(CourseInstructorDTO courseInstructorDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (courseInstructorDTO.ID != id)
                return BadRequest();

            var flag = await courseInstructorService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var courseInstructor = mapper.Map<CourseInstructor>(courseInstructorDTO);
                courseInstructor = await courseInstructorService.Update(courseInstructor);
                return Ok(courseInstructorDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await courseInstructorService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await courseInstructorService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
