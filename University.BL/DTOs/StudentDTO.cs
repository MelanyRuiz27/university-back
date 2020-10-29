using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo LastName es requerido")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es requerido")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo EnrollmentDate es requerido")]
        public DateTime EnrollmentDate { get; set; }
        public string FullName 
        { 
            get { return string.Format("{0} {1}", FirstMidName, LastName); } 
        }
    }
}
