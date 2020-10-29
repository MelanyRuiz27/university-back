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
    public class OfficeAssignmentController : ApiController
    {
        private IMapper mapper;
        private readonly OfficeAssignmentServicie officeAssignmentServicie = new OfficeAssignmentServicie(new OfficeAssignmentRepository(UniversityContext.Create()));

        public OfficeAssignmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        //Devuelve todos los cursos(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var officeAssignments = await officeAssignmentServicie.GetAll();
            var officeAssignmentsDTO = officeAssignments.Select(x => mapper.Map<OfficeAssignmentDTO>(x));
            return Ok(officeAssignmentsDTO);//status code 200
        }

        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var officeAssignment = await officeAssignmentServicie.GetById(id);
            var officeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(officeAssignment);
            return Ok(officeAssignmentDTO);//status code 200
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(OfficeAssignmentDTO officeAssignmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var officeAssignment = new OfficeAssignmentDTO
            //{
            //    InstructorID = officeAssignmentDTO.InstructorID,
            //    Location = officeAssignmentDTO.Location
            //};

            try
            {
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentServicie.Insert(officeAssignment);
                return Ok(officeAssignmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(OfficeAssignmentDTO officeAssignmentDTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (officeAssignmentDTO.InstructorID != id)
                return BadRequest();

            var flag = await officeAssignmentServicie.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentServicie.Insert(officeAssignment);
                return Ok(officeAssignmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await officeAssignmentServicie.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await officeAssignmentServicie.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
