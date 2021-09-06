using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;

namespace University.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityContext context = new UniversityContext();
        // GET: Courses
        [HttpGet]
        public ActionResult Index(int? courseid, int? pageSize, int? page)
        {
            #region Consulta cursos
            var query = context.Courses.ToList();

            var courses = query.Select(x => new CourseDTO
            {
                CourseID = x.CourseID,
                Title = x.Title,
                Credits = x.Credits
            }).ToList();
            #endregion

            #region Listar intructores
            if (courseid != null)
            {
                var instructores = (from q in context.CourseInstructors
                                    join r in context.Courses on q.CourseID equals r.CourseID
                                    join s in context.Instructors on q.InstructorID equals s.ID
                                    where q.CourseID == courseid
                                    select new InstructorDTO
                                    {
                                        LastName = s.LastName,
                                        FirstMidName = s.FirstMidName,

                                    }).ToList();

                ViewBag.Instructores = instructores;
            }
            #endregion
            #region Paginacion
            //Si viene nulo dele 10 por defecto
            pageSize = (pageSize ?? 10);
            //si viene igual por defecto llevelo a la 1 
            page = (page ?? 1);
            ViewBag.pageSize = pageSize;
            #endregion 
            return View(courses.ToPagedList(page.Value, pageSize.Value));

        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        public ActionResult Create(CourseDTO course)
        {
            #region Crear course
            try
            {
                if (!ModelState.IsValid)
                    return View(course);

                context.Courses.Add(new Course
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Credits = course.Credits

                });
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            #endregion
            return View(course);
        }

        // GET: Courses/Edit
        [HttpGet]
        public ActionResult Edit(int courseid)
        {
            #region Optener id
            var course = context.Courses.Where(x => x.CourseID == courseid)
                                             .Select(x => new CourseDTO
                                             {
                                                 CourseID = x.CourseID,
                                                 Title = x.Title,
                                                 Credits = x.Credits
                                             }).FirstOrDefault();
            #endregion
            return View(course);
        }


        // POST: Courses/Edit
        [HttpPost]
        public ActionResult Edit(CourseDTO course)
        {
            #region Editar curso
            try
            {
                if (!ModelState.IsValid)
                    return View(course);
                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == course.CourseID);

                //campos que se van a modificar
                //sobreescribo las propiedades del modelo de base de datos
                courseModel.Title = course.Title;
                courseModel.Credits = course.Credits;
                //aplique los cambios en base de datos
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            #endregion
            return View(course);
        }


        // GET: Courses/Delete
        [HttpGet]
        public ActionResult Delete(int courseid)
        {
            if (!context.CourseInstructors.Any(x => x.CourseID == courseid))
            {
                var courseModel = context.Courses.FirstOrDefault(x => x.CourseID == courseid);
                context.Courses.Remove(courseModel);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
