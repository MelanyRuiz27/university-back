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
    public class EnrollmentController : ApiController
    {
        private IMapper mapper;
        private readonly EnrollmentService enrollmentService = new EnrollmentService(new EnrollmentRepository(UniversityContext.Create()));

        public EnrollmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        //Devuelve todos los cursos(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var enrollments = await enrollmentService.GetAll();
            var enrollmentsDTO = enrollments.Select(x => mapper.Map<EnrollmentDTO>(x));
            return Ok(enrollmentsDTO);//status code 200
        }

        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var enrollment = await enrollmentService.GetById(id);
            var enrollmentDTO = mapper.Map<EnrollmentDTO>(enrollment);
            return Ok(enrollmentDTO);//status code 200
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(EnrollmentDTO enrollmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var enrollment = new EnrollmentDTO
            //{
            //    EnrollmentID = enrollmentDTO.EnrollmentID,
            //    CourseID = enrollmentDTO.CourseID,
            //    StudentID = enrollmentDTO.StudentID,
            //    Grade = enrollmentDTO.Grade
            //};

            try
            {
                var enrollment = mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Insert(enrollment);
                return Ok(enrollmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(EnrollmentDTO enrollmentDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (enrollmentDTO.EnrollmentID != id)
                return BadRequest();

            var flag = await enrollmentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var enrollment = mapper.Map<Enrollment>(enrollmentDTO);
                enrollment = await enrollmentService.Insert(enrollment);
                return Ok(enrollmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await enrollmentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await enrollmentService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
