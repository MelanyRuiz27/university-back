using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentDTO
    {
        public OfficeAssignmentDTO()
        {
            Instructor = new InstructorDTO();
        }

        //[Required(ErrorMessage = "El campo InstructorID es requerido")]
        public int InstructorID { get; set; }

        [Required(ErrorMessage = "El campo Location es requerido")]
        [StringLength(50)]

        public string Location { get; set; }

        public InstructorDTO Instructor { get; set; }
    }
}
