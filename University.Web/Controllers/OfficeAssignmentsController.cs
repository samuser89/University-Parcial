using System;
using System.Linq;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;

namespace University.Web.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();

        // GET: OfficeAssignments
        public ActionResult Index()
        {
            //  SELECT * FROM OfficeAssignment
            var query = context.OfficeAssignments.Include("Instructor").ToList();
            var offices = query.Select(x => new OfficeAssignmentDTO
            {
                InstructorID = x.InstructorID,
                Location = x.Location,
                Instructor = new InstructorDTO
                {
                    FirstMidName = x.Instructor.FirstMidName,
                    LastName = x.Instructor.LastName
                }
            });

            return View(offices);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadData();

            return View();
        }

        [HttpPost]
        public ActionResult Create(OfficeAssignmentDTO office)
        {
            LoadData();

            if (!ModelState.IsValid)
                return View(ModelState);

            // INSERT INTO OfficeAssignments
            context.OfficeAssignments.Add(new BL.Models.OfficeAssignment
            {
                InstructorID = office.InstructorID,
                Location = office.Location
            });
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        

        private void LoadData()
        {
            var instructors = context.Instructors.Select(x => new InstructorDTO
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName
            }).ToList();
            ViewData["Instructors"] = new SelectList(instructors, "ID", "FullName");
        }



        [HttpGet]
        public ActionResult Edit(int instructorid){

            var offices = context.OfficeAssignments.Where(x => x.InstructorID == instructorid)
                                          .Select(x => new OfficeAssignmentDTO
                                          {
                                              InstructorID = x.InstructorID,
                                              Location = x.Location
                                          }).FirstOrDefault();

            return View(offices);
        }

        [HttpPost]
        public ActionResult Edit(OfficeAssignmentDTO office)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(office);

                var officeModel = context.OfficeAssignments.FirstOrDefault(x => x.InstructorID == office.InstructorID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                officeModel.Location = office.Location;

                //aplique los cambios en base de datos
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(office);
        }


        [HttpGet]
        public ActionResult Delete(int instructorid)
        {
            /*if (!context.OfficeAssignments.Any(x => x.InstructorID == instructorid))
            {*/
            //NO ES NECESARIO VALIDAR ELIMINA DIRECTO
            var officeModel = context.OfficeAssignments.FirstOrDefault(x => x.InstructorID == instructorid);
            context.OfficeAssignments.Remove(officeModel);
            context.SaveChanges();
            //}

            return RedirectToAction("Index");
        }

    }
}