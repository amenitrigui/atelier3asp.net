using Atelier3.Models.Repositories;
using Atelier3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Atelier3.Migrations;
using Microsoft.Extensions.Hosting.Internal;


namespace tp03.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;

        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            // Récupérer la liste des étudiants depuis le repository
            var students = _studentRepository.GetAll();

            // Configurer la liste déroulante des écoles
            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName");

            // Passer la liste des étudiants à la vue
            return View(students);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = _studentRepository.GetById(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
               
                // If the Photo property on the incoming model object is not null, then the user has selected an image to upload.
               
                Student newProduct = new Student
                {
                    StudentId = model.StudentId,
                    StudentName = model.StudentName,
                    Age = model.Age,
                    BirthDate = model.BirthDate,
                    SchoolID = model.SchoolID,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table

                };
                _studentRepository.Add(newProduct);
                   return RedirectToAction(nameof(Index));
            }
            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        }




















        //// POST: StudentController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _studentRepository.Add(student);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName");
        //    return View(student);
        //}

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("StudentId, StudentName, Age, BirthDate, SchoolID")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _studentRepository.Edit(student);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
            return View(student);
        }
        // POST: ProductController/Delete/5





        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
       {
            var student = _studentRepository.GetById(id);
            return View(student);
        }

        // POST: StudentController/Delete/5
      
        
        
        
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = _studentRepository.GetById(id);
            _studentRepository.Delete(student);
            return RedirectToAction(nameof(Index));
        }
        // GET: StudentController/Search

      






























































        public ActionResult Search(string name, int? schoolid)
        {
            var result = _studentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = _studentRepository.FindByName(name);
            else if (schoolid != null)
                result = _studentRepository.GetStudentsBySchoolID(schoolid);

            ViewBag.SchoolID = new SelectList(_schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }
    }
}