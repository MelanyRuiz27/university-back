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
    public class InstructorController : ApiController
    {
        private IMapper mapper;
        private readonly InstructorService instructorService = new InstructorService(new InstructorRepository(UniversityContext.Create()));

        public InstructorController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        //Devuelve todos los cursos(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var instructors = await instructorService.GetAll();
            var instructorsDTO = instructors.Select(x => mapper.Map<InstructorDTO>(x));
            return Ok(instructorsDTO);//status code 200
        }

        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var instructor = await instructorService.GetById(id);
            var instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return Ok(instructorDTO);//status code 200
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(InstructorDTO instructorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var instructor = new InstructorDTO
            //{
            //    ID = instructorDTO.ID,
            //    LastName = instructorDTO.LastName,
            //    FirstMidName = instructorDTO.FirstMidName,
            //    HireDate = instructorDTO.HireDate
            //};

            try
            {
                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Insert(instructor);
                return Ok(instructorDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(InstructorDTO instructorDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (instructorDTO.ID != id)
                return BadRequest();

            var flag = await instructorService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Update(instructor);
                return Ok(instructorDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await instructorService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await instructorService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
