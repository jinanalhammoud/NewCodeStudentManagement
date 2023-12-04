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
        private readonly ClassesManager _classesManager;

        public ClassRegistrationsController(ApplicationDBContext context, ClassesManager classesManager)
        {
            _context = context;
            _classesManager = classesManager;
        }

        // GET: ClassRegistrations
        public async Task<IActionResult> Index()
        {
            if (_context.ClassRegistrations != null)
            {
                var classes = await _context.ClassRegistrations.Include(x => x.Student).Include(x => x.Class).ThenInclude(x => x.Course)
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
            var classes = _classesManager.GetClassInfos();

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
            ModelState.Remove(nameof(classRegistration.Class));
            ModelState.Remove(nameof(classRegistration.Student));
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
            if (id == null || _context.ClassRegistrations == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.ClassRegistrations.FindAsync(id);
            if (classRegistration == null)
            {
                return NotFound();
            }

            var students = _context.Students.ToList();
            var classes = _classesManager.GetClassInfos();

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

            ModelState.Remove(nameof(classRegistration.Student));
            ModelState.Remove(nameof(classRegistration.Class));

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
            if (id == null || _context.ClassRegistrations == null)
            {
                return NotFound();
            }

            var classRegistration = await _context.ClassRegistrations.Include(x => x.Student).Include(x => x.Class).ThenInclude(x => x.Course)
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
            if (_context.ClassRegistrations == null)
            {
                return Problem("Entity set 'ApplicationDBContext.classRegistrations'  is null.");
            }
            var classRegistration = await _context.ClassRegistrations.FindAsync(id);
            if (classRegistration != null)
            {
                _context.ClassRegistrations.Remove(classRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassRegistrationExists(int id)
        {
            return (_context.ClassRegistrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private ClassRegistrationDetails ClassRegistrationDetailsMapper(ClassRegistration classRegistration)
        {
            var classRegistrationDetails = new ClassRegistrationDetails();
            classRegistrationDetails.Id = classRegistration.Id;

            classRegistrationDetails.Student = classRegistration.Student.Name;
            classRegistrationDetails.Class = $"{classRegistration.Class.Course.Code} - {classRegistration.Class.GroupName}";

            return classRegistrationDetails;
        }
    }
}
