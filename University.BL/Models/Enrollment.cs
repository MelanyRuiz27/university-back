using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University.BL.Models
{
    public enum Grade
    {
        A, B, D, C, E
    }

    [Table("Enrollment", Schema = "dbo")]
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [ForeignKey ("Course")]//Primero especificar la foreign
        public int CourseID { get; set; }

        [ForeignKey("Student")]
        public int StudentID { get; set; }

        public Grade Grade { get; set; }

        //Segundo especificar la Navegabilidad
        public Course Course { get; set; }
        public Student Student { get; set; }

        //Tercero ir a la tabla donde esta la primaria
    }
}
