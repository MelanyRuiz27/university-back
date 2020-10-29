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
using System.Web.Http.Description;
using System.Collections;
using System.Collections.Generic;


namespace University.Web.Controllers
{
    public class StudentController : ApiController
    {
        private IMapper mapper;
        private readonly StudentService studentService = new StudentService(new StudentRepository(UniversityContext.Create()));

        public StudentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        ///<summary>
        ///Obtiene los objetos de estudiante
        ///</summary>
        ///<returns>Listado de los objetos de estudiantes</returns>
        ///<response code="200">Ok. Devuleve la lista de los objetos solicitado.</response>
        //Devuelve todos los estudiantes(null)
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var students = await studentService.GetAll();
            var studentsDTO = students.Select(x => mapper.Map<StudentDTO>(x));
            return Ok(studentsDTO);//status code 200
        }

        ///<summary>
        ///Obtiene un objeto studiante por su Id.
        ///</summary>
        ///<remarks>
        ///Obtiene un objeto por su Id.
        /// </remarks>
        ///<param name="id">Id del objeto</param>
        ///<response>Objeto estudiante.</response>
        ///<response code="200">Ok. Devuleve la lista de los objetos solicitado.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        //Devuelve solo un curso(parametro)
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var student = await studentService.GetById(id);
            var studentDTO = mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);//status code 200
        }

        ///<summary>
        ///Actualiza los objetos de estudiantes
        ///</summary>
        ///<returns>Actualizar los objetos de estudiantes</returns>
        ///<response code="200">Ok. Se actualiza los datos.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Post(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var student = new StudentDTO
            //{
            //    ID = studentDTO.ID,
            //    LastName = studentDTO.LastName,
            //    FirstMidName = studentDTO.FirstMidName,
            //    EnrollmentDate = studentDTO.EnrollmentDate
            //};

            try
            {
                var student = mapper.Map<Student>(studentDTO);
                student = await studentService.Insert(student);
                return Ok(studentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        ///<summary>
        ///Actualiza los objetos de estudiantes
        ///</summary>
        ///<returns>Actualizar los objetos de estduantes trayendo la informacion con el Id</returns>
        ///<response code="200">Ok. Se actualiza los datos.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Put(StudentDTO studentDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (studentDTO.ID != id)
                return BadRequest();

            var flag = await studentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                var student = mapper.Map<Student>(studentDTO);
                student = await studentService.Update(student);
                return Ok(studentDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        ///<summary>
        ///Elimina estudiante
        ///</summary>
        ///<returns>Elimina estudiante por su Id</returns>
        ///<response code="200">Ok. Se elimino el estudiante.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await studentService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await studentService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}
