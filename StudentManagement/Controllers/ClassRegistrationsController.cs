using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class ClassRegistrationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ClassRegistrationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ClassRegistrations
        public async Task<IActionResult> Index()
        {
            if (_context.classRegistrations != null)
            {
                var classes = await _context.classRegistrations
                    .ToListAsync();

                var classRegistrationDetails = new List<ClassRegistrationDetails>();

                if (classes != null && classes.Any())
                {
                    foreach (var classItem in classes)
                    {
                        classRegistrationDetails.Add(ClassRegistrationDetailsMapper(classItem));
                    }
                }

                return View(classRegistrationDetails);
            }
            else
            {
                return Problem("Entity set 'ApplicationDBContext.ClassRegistrations' is null.");
            }
        }

        // GET: ClassRegistrations/Create
        public IActionResult Create()
        {
            var students = _context.Students.ToList();
            var classes = _context.classes.ToList();

            ViewBag.StudentsList = new SelectList(students, "Id", "Name");
            ViewBag.ClassesList = new SelectList(classes, "Id", "Title");
            return View();
        }

        // POST: ClassRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,ClassId")] ClassRegistration classRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classRegistration);
        }

        // GET: ClassRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.classRegistrations == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.classRegistrations.FindAsync(id);
            if (classRegistration == null)
            {
                return NotFound();
            }

            var students = _context.Students.ToList();
            var classes = _context.classes.ToList();

            ViewBag.StudentsList = new SelectList(students, "Id", "Name");
            ViewBag.ClassesList = new SelectList(classes, "Id", "Title");

            return View(classRegistration);
        }

        // POST: ClassRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ClassId")] ClassRegistration classRegistration)
        {
            if (id != classRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassRegistrationExists(classRegistration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(classRegistration);
        }

        // GET: ClassRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.classRegistrations == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.classRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classRegistration == null)
            {
                return NotFound();
            }

            return View(ClassRegistrationDetailsMapper(classRegistration));
        }

        // POST: ClassRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.classRegistrations == null)
            {
                return Problem("Entity set 'ApplicationDBContext.classRegistrations'  is null.");
            }
            var classRegistration = await _context.classRegistrations.FindAsync(id);
            if (classRegistration != null)
            {
                _context.classRegistrations.Remove(classRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassRegistrationExists(int id)
        {
            return (_context.classRegistrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private ClassRegistrationDetails ClassRegistrationDetailsMapper(ClassRegistration classRegistration)
        {
            var classRegistrationDetails = new ClassRegistrationDetails();
            classRegistrationDetails.Id = classRegistration.Id;

            var classItem = _context.classes.Find(classRegistration.ClassId);
            var student = _context.Students.Find(classRegistration.StudentId);

            classRegistrationDetails.Student = student.Name;
            classRegistrationDetails.Class = classItem.Title;

            return classRegistrationDetails;
        }
    }
}
