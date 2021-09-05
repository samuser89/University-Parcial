using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class StudentDTO
    {
        #region ID
        [Display(Name = "ID")]
        [Required(ErrorMessage = "El campo ID es requerido.")]
        public int ID { get; set; }
        #endregion


        #region LastName
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "El campo Last Name es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string LastName { get; set; }
        #endregion


        #region FirstMidName
        [Display(Name = "First Mid Name")]
        [Required(ErrorMessage = "El campo First Mid Name es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string FirstMidName { get; set; }
        #endregion


        #region EnrollmentDate
        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "El campo Enrollment Date es requerido.")]
        [DataType(DataType.DateTime)]
        public DateTime EnrollmentDate { get; set; }
        #endregion


        #region Concatenacion del nombre completo
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", LastName, FirstMidName);
            }
        }
        #endregion

        //complete
    }
}
