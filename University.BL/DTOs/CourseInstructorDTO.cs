using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseInstructorDTO
    {
        #region ID
        [Display(Name = "ID")]
        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int ID { get; set; }
        #endregion


        #region CourseID
        [Display(Name = "Course ID")]
        [Required(ErrorMessage = "El campo Course ID es requerido.")]
        public int CourseID { get; set; }
        #endregion


        #region InstructorID
        [Display(Name = "Instructor ID")]
        [Required(ErrorMessage = "El campo Instructor ID es requerido.")]
        public int InstructorID { get; set; }
        #endregion


        #region foreignKeys
        public CourseDTO Course{ get; set; }
        public InstructorDTO Instructor { get; set; }
        #endregion

        //complete
    }
}
