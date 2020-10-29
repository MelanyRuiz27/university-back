using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class InstructorDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo LastName es requerido")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es requerido")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        public DateTime HireDate { get; set; }
    }
}
