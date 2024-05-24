namespace Atelier3.Controllers
{
    using Atelier3.Models.Repositories;
    using Atelier3.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class SchoolController : Controller
    {
        private readonly ISchoolRepository schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        public IActionResult Index()
        {
            var schools = schoolRepository.GetAll();
            return View(schools);
        }

        public IActionResult Details(int id)
        {
            var school = schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(School school)
        {
            if (ModelState.IsValid)
            {
                schoolRepository.Add(school);
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var school = schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [HttpPost]
        public IActionResult Edit(School school)
        {
            if (ModelState.IsValid)
            {
                schoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var school = schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var school = schoolRepository.GetById(id);
            if (school != null)
            {
                schoolRepository.Delete(school);
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
