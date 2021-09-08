using System;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class DepartmentDTO
    {
        #region DepartmentID
        [Display(Name = "Department ID")]
        [Required(ErrorMessage = "El campo Department ID es requerido.")]
        public int DepartmentID { get; set; }
        #endregion


        #region Name
        [Display(Name = "Name")]
        [Required(ErrorMessage = "El campo Name es requerido.")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Name { get; set; }
        #endregion


        #region Budget
        [Display(Name = "Budget")]
        [Required(ErrorMessage = "El campo Budget es requerido.")]
        public decimal Budget { get; set; }
        #endregion


        #region StartDate
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "El campo Start Date es requerido.")]
        public DateTime StartDate { get; set; }
        #endregion


        #region InstructorID
        [Display(Name = "Instructor ID")]
        [Required(ErrorMessage = "El campo Instructor ID es requerido.")]
        public int InstructorID { get; set; }
        #endregion


        #region ForeignKeys
        public InstructorDTO Instructor { get; set; }
        #endregion

        // complete
        public string StartDateSTR
        {
            get
            {
                return StartDate.ToString("yyyy-MM-dd");
            }
        }
        public string BudgetSTR
        {
            get
            {
                return Budget.ToString("");
            }
        }
    }
}