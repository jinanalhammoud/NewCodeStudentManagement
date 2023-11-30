﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ClassesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            if (_context.classes != null)
            {
                var classes = await _context.classes
                    .ToListAsync();

                var classDetails = new List<ClassDetails>();

                if (classes != null && classes.Any())
                {
                    foreach (var classItem in classes)
                    {
                        classDetails.Add(ClassDetailsMapper(classItem));
                    }
                }

                return View(classDetails);
            }
            else
            {
                return Problem("Entity set 'ApplicationDBContext.Classes' is null.");
            }
        }

        private ClassDetails ClassDetailsMapper(Classes classItem)
        {
            var classDetails = new ClassDetails
            {
                Id = classItem.Id,
                Year = classItem.Year,
                Semester = classItem.Semester,
            };
            var classTeacher = _context.Teachers.Find(classItem.TeacherId);
            var classCourse = _context.Courses.Find(classItem.CourseId);
            classDetails.Course = classCourse.Code;
            classDetails.Teacher = classTeacher.Name;
            classDetails.Title = classItem.Title;
            return classDetails;
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.classes == null)
            {
                return NotFound();
            }

            var classes = await _context.classes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (classes == null)
            {
                return NotFound();
            }
            var classDetails = ClassDetailsMapper(classes);
            return View(classDetails);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            var courses = _context.Courses.ToList();
            ViewBag.CoursesList = new SelectList(courses, "Id", "Code");
            var teachers = _context.Teachers.ToList();
            ViewBag.TeachersList = new SelectList(teachers, "Id", "Name");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,Title,TeacherId,Year,Semester")] Classes classItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classItem);
        }


        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.classes == null)
            {
                return NotFound();
            }

            var classItem = await _context.classes.FindAsync(id);
            if (classItem == null)
            {
                return NotFound();
            }
            var courses = _context.Courses.ToList();
            ViewBag.CoursesList = new SelectList(courses, "Id", "Code");
            var teachers = _context.Teachers.ToList();
            ViewBag.TeachersList = new SelectList(teachers, "Id", "Name");
            return View(classItem);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,Title,TeacherId,Year,Semester")] Classes classItem)
        {
            if (id != classItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassesExists(classItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof( Index));
            }
            return View(classItem);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.classes == null)
            {
                return NotFound();
            }

            var classItem = await _context.classes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classItem == null)
            {
                return NotFound();
            }

            var classDetails = ClassDetailsMapper(classItem);
            return View(classDetails);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.classes == null)
            {
                return Problem("Entity set 'ApplicationDBContext.classItem'  is null.");
            }
            var classes = await _context.classes.FindAsync(id);
            if (classes != null)
            {
                _context.classes.Remove(classes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof( Index));
        }

        private bool ClassesExists(int id)
        {
            return (_context.classes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private ClassDetails MapClassDetails(Classes classes)
        {
            var classDetails = new ClassDetails
            {
                Id = classes.Id,
                Semester = classes.Semester,
                Year = classes.Year,
            };
            var course = _context.Courses.Find(classes.CourseId);
            var teacher = _context.Teachers.Find(classes.TeacherId);
            classDetails.Course = course.Code;
            classDetails.Teacher = teacher.Name;
            return classDetails;
        }
    }
}
