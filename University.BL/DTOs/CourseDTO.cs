using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {
        #region Course ID
        
        [Display(Name = "Course ID")]
        [Required(ErrorMessage = "El campo Course ID es requerido.")]
        public int CourseID { get; set; }
        #endregion

        #region Title
        [Display(Name = "Title")]
        [Required(ErrorMessage = "El campo Title es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Title { get; set; }
        #endregion

        #region Credits
        [Display(Name = "Credits")]
        [Required(ErrorMessage = "El campo Credits es requerido.")]
        public int Credits { get; set; }
        #endregion

        // complete
    }
}