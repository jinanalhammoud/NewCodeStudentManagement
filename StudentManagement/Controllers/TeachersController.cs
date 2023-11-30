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
    public class TeachersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TeachersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            if (_context.Teachers != null)
            {
                var teachers = await _context.Teachers
                    .ToListAsync();

                var teachersDetails = new List<TeacherDetails>();

                if(teachers != null && teachers.Any())
                {
                    foreach(var teacher in teachers)
                    {
                        teachersDetails.Add(TeacherDetailsMapper(teacher));
                    }
                }

                return View(teachersDetails);
            }
            else
            {
                return Problem("Entity set 'ApplicationDBContext.Teachers' is null.");
            }
        }


        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }
            var teacherDetails = TeacherDetailsMapper(teacher);
            return View(teacherDetails);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name"); // Pass the departments to the view
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentId,PhoneNumber,Email")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            var departments = _context.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name"); // Pass the departments to the view
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DepartmentId,PhoneNumber,Email")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            var teacherDetails = TeacherDetailsMapper(teacher);
            return View(teacherDetails);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private TeacherDetails TeacherDetailsMapper(Teacher teacher)
        {
            var teacherDetails = new TeacherDetails
            {
                Id = teacher.Id,
                Name = teacher.Name,
                PhoneNumber = teacher.PhoneNumber,
                Email = teacher.Email,
            };
            var department = _context.Departments.Find(teacher.DepartmentId);
            teacherDetails.Department = department.Name;
            return teacherDetails;
        }
    }
}
