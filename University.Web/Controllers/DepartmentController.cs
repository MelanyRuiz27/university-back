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
    public class DepartmentController : ApiController
    {
        private IMapper mapper;
        private readonly DepartmentService departmentService = new DepartmentService(new DepartmentRepository(UniversityContext.Create()));

        public DepartmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        //Devuelve todos los cursos(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var departaments = await departmentService.GetAll();
            var departamentsDTO = departaments.Select(x => mapper.Map<DepartmentDTO>(x));
            return Ok(departamentsDTO);//status code 200
        }

        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var departament = await departmentService.GetById(id);
            var departamentDTO = mapper.Map<DepartmentDTO>(departament);
            return Ok(departamentDTO);//status code 200
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var departament = new DepartmentDTO
            //{
            //    DepartmentID = departmentDTO.DepartmentID,
            //    Name = departmentDTO.Name,
            //    Budget = departmentDTO.Budget,
            //    StartDate = departmentDTO.StartDate,
            //    InstructorID = departmentDTO.InstructorID
            //};

            try
            {
                var departament = mapper.Map<Department>(departmentDTO);
                departament = await departmentService.Insert(departament);
                return Ok(departmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(DepartmentDTO departmentDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (departmentDTO.DepartmentID != id)
                return BadRequest();

            var flag = await departmentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var department = mapper.Map<Department>(departmentDTO);
                department = await departmentService.Update(department);
                return Ok(departmentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await departmentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await departmentService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
