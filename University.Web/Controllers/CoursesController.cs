using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;
using University.BL.DTOs;
using University.BL.Models;
using System;
using System.Web.Http.Description;
using System.Collections;
using System.Collections.Generic;

namespace University.Web.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private IMapper mapper;
        private readonly CourseService courseService = new CourseService(new CourseRepository(UniversityContext.Create()));

        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        ///<summary>
        ///Obtiene los objetos de cursos 
        ///</summary>
        ///<returns>Listado de los objetos de cursos</returns>
        ///<response code="200">Ok. Devuleve la lista de los objetos solicitado.</response>
        
        //Devuelve todos los cursos(null)
        [HttpGet]
        [ResponseType(typeof(IEnumerable<CourseDTO>))]
        public async Task<IHttpActionResult> Get()
        {
            var courses = await courseService.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));
            return Ok(coursesDTO);//status code 200
        }
        ///<summary>
        ///Obtiene un objeto Course por su Id.
        ///</summary>
        ///<remarks>
        ///Obtiene un objeto por su Id.
        /// </remarks>
        ///<param name="id">Id del objeto</param>
        ///<response>Objeto Course.</response>
        ///<response code="200">Ok. Devuleve la lista de los objetos solicitado.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        //Devuelve solo un curso(parametro)
        [HttpGet]
        [ResponseType(typeof(CourseDTO))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var course = await courseService.GetById(id);
            var courseDTO = mapper.Map<CourseDTO>(course);
            return Ok(courseDTO);//status code 200
        }

        ///<summary>
        ///Actualiza los objetos de cursos
        ///</summary>
        ///<returns>Actualizar los objetos de cursos</returns>
        ///<response code="200">Ok. Se actualiza los datos.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpPost]
        public async Task<IHttpActionResult> Post(CourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400
            //var course = new CourseDTO
            //{
            //    CourseID = courseDTO.CourseID,
            //    Title = courseDTO.Title,
            //    Credits = courseDTO.Credits
            //};

            try
            {
                var course = mapper.Map<Course>(courseDTO);
                course = await courseService.Insert(course);
                return Ok(courseDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }

            
        }

        ///<summary>
        ///Actualiza los objetos de cursos
        ///</summary>
        ///<returns>Actualizar los objetos de cursos trayendo la informacion con el Id</returns>
        ///<response code="200">Ok. Se actualiza los datos.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpPut]
        public async Task<IHttpActionResult> Put(CourseDTO courseDTO, int id)//objet=> body/primitivo => url
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);//status code 400

            if (courseDTO.CourseID != id)
                return BadRequest();

            var flag = await courseService.GetById(id);
            if(flag == null)
                    return NotFound();//status code 404

            try
            {
                var course = mapper.Map<Course>(courseDTO);
                course = await courseService.Update(course);
                return Ok(courseDTO);//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }

        ///<summary>
        ///Elimina curso
        ///</summary>
        ///<returns>Elimina curso por su Id</returns>
        ///<response code="200">Ok. Se elimino el curso.</response>
        ///<response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        ///<response code="500">NotFound. Error interno del servidor.</response>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await courseService.GetById(id);
            if (flag == null)
                return NotFound();//status code 404

            try
            {
                await courseService.Delete(id);
                return Ok();//status code 200
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);//status code 500
            }


        }
    }
}