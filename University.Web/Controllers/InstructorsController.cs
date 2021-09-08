using System;
using System.Collections.Generic;
using System.Web.Mvc;
using University.BL.Models;
using University.BL.DTOs;
using University.BL.Data;
using System.Linq;
using PagedList;

namespace University.Web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();


        [HttpGet]
        public ActionResult Index(int? instructorID, int? pageSize, int? page)
        {
            #region Consluta Instructor
            var query = context.Instructors.ToList();

            var instructors = query.Where(x => x.HireDate < DateTime.Now)
                                .Select(x => new InstructorDTO
                                {
                                    ID = x.ID,
                                    FirstMidName = x.FirstMidName,
                                    LastName = x.LastName,
                                    HireDate = x.HireDate
                                }).ToList();


            #endregion


            #region Lista Instructores

            if (instructorID != null)
            {
                var deparments = (from q in context.Departments
                                  join r in context.Instructors on q.InstructorID equals r.ID
                                  where q.InstructorID == instructorID
                                  select new DepartmentDTO
                                  {

                                      DepartmentID = q.DepartmentID,
                                      Name = q.Name,
                                      Budget = q.Budget
                                  }).ToList();

                ViewBag.Deparments = deparments;
            }

            #endregion

            #region Paginacion
            //Si viene nulo dele 10 por defecto
            pageSize = (pageSize ?? 5);
            //si viene igual por defecto llevelo a la 1 
            page = (page ?? 1);
            ViewBag.pageSize = pageSize;
            #endregion 
            return View(instructors.ToPagedList(page.Value, pageSize.Value));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(InstructorDTO instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(instructor);

                if (instructor.HireDate > DateTime.Now)
                    throw new Exception("La fecha no puede ser mayor a la fecha actual");

                //INSERT INTO Students(FirstMidName,LastName,EnrollmentDate) VALUES(@FirstMidName, @LastName, @EnrollmentDate)
                context.Instructors.Add(new Instructor
                {
                    FirstMidName = instructor.FirstMidName,
                    LastName = instructor.LastName,
                    HireDate = instructor.HireDate
                });
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructor);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var instructor = context.Instructors.Where(x => x.ID == id)
                                          .Select(x => new InstructorDTO
                                          {
                                              ID = x.ID,
                                              LastName = x.LastName,
                                              FirstMidName = x.FirstMidName,
                                              HireDate = x.HireDate
                                          }).FirstOrDefault();

            return View(instructor);
        }

        [HttpPost]
        public ActionResult Edit(InstructorDTO instructor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(instructor);

                if (instructor.HireDate > DateTime.Now)
                    throw new Exception("La fecha no puede ser mayor a la fecha actual");
                var instructortModel = context.Instructors.FirstOrDefault(x => x.ID == instructor.ID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                instructortModel.LastName = instructor.LastName;
                instructortModel.FirstMidName = instructor.FirstMidName;
                instructortModel.HireDate = instructor.HireDate;
                //aplique los cambios en base de datos
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(instructor);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!context.Departments.Any(x => x.InstructorID == id))
            {
                var instructortModel = context.Instructors.FirstOrDefault(x => x.ID == id);
                context.Instructors.Remove(instructortModel);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}