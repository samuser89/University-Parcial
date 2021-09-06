using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class Enrollment
    {
        #region EnrollmentID
        [Display(Name = "Enrollment ID")]
        [Required(ErrorMessage = "El campo Enrollment ID es requerido.")]
        public int EnrollmentID { get; set; }
        #endregion


        #region CourseID
        [Display(Name = "Course ID")]
        [Required(ErrorMessage = "El campo Course ID es requerido.")]
        public int CourseID { get; set; }
        #endregion


        #region StudentID
        [Display(Name = "Student ID")]
        [Required(ErrorMessage = "El campo Student ID es requerido.")]
        public int StudentID { get; set; }
        #endregion


        #region Grade
        [Display(Name = "Grade")]
        [Required(ErrorMessage = "El campo Grade es requerido.")]
        public int Grade { get; set; }
        #endregion

        #region ForeignKeys
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
        #endregion

        //complete
    }
}
