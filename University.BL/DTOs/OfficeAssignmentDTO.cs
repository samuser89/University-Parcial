using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class OfficeAssignmentDTO
    {
        #region InstructorID
        [Display(Name = "InstructorID")]
        [Required(ErrorMessage = "El campo InstructorID es requerido.")]
        public int InstructorID { get; set; }
        #endregion


        #region Location
        [Display(Name = "Location")]
        [Required(ErrorMessage = "El campo Location es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Location { get; set; }
        #endregion

        #region ForeignKeys
        public InstructorDTO Instructor { get; set; }
        #endregion


        //complete
    }
}