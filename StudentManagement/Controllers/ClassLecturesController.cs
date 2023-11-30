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
    public class ClassLecturesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ClassLecturesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ClassLectures
        public async Task<IActionResult> Index()
        {
            if (_context.classLectures != null)
            {
                var classLectures = await _context.classLectures.ToListAsync();
                var classLecturesDetails = new List<ClassLectureDetails>();
                if (classLectures != null && classLectures.Count > 0)
                {
                    classLectures.ForEach(lecture => classLecturesDetails.Add(ClassLectureDetailsMapper(lecture)));
                }
                return View(classLecturesDetails);
            }
            else
                return Problem("Entity set 'ApplicationDBContext.classLectures'  is null.");
        }


        // GET: ClassLectures/Create
        public IActionResult Create()
        {
            var classes = _context.classes.ToList();
            ViewBag.ClassesList = new SelectList(classes, "Id", "Title");
            return View();
        }

        // POST: ClassLectures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ClassId,Day,StartTime,EndTime")] ClassLecture classLecture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classLecture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classLecture);
        }

        // GET: ClassLectures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.classLectures == null)
            {
                return NotFound();
            }

            var classLecture = await _context.classLectures.FindAsync(id);
            if (classLecture == null)
            {
                return NotFound();
            }
            var classes = _context.classes.ToList();
            ViewBag.ClassesList = new SelectList(classes, "Id", "Title");
            return View(classLecture);
        }

        // POST: ClassLectures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ClassId,Day,StartTime,EndTime")] ClassLecture classLecture)
        {
            if (id != classLecture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classLecture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassLectureExists(classLecture.Id))
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
            return View(classLecture);
        }

        // GET: ClassLectures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.classLectures == null)
            {
                return NotFound();
            }

            var classLecture = await _context.classLectures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classLecture == null)
            {
                return NotFound();
            }

            return View(classLecture);
        }

        // POST: ClassLectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.classLectures == null)
            {
                return Problem("Entity set 'ApplicationDBContext.classLectures'  is null.");
            }
            var classLecture = await _context.classLectures.FindAsync(id);
            if (classLecture != null)
            {
                _context.classLectures.Remove(classLecture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassLectureExists(int id)
        {
            return (_context.classLectures?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private ClassLectureDetails ClassLectureDetailsMapper(ClassLecture classLecture)
        {
            var classLectureDetails = new ClassLectureDetails
            {
                Id = classLecture.Id,
                Title = classLecture.Title,
                Day = classLecture.Day.ToLongDateString(),
                StartTime = classLecture.StartTime.ToShortTimeString(),
                EndTime = classLecture.EndTime.ToShortTimeString(),
            };

            var classItem = _context.classes.Find(classLecture.ClassId);
            classLectureDetails.Class = classItem.Title;

            return classLectureDetails;
        }
        public IActionResult YourActionName()
        {
            // Create or retrieve a ClassLectureDetails object
            ClassLectureDetails model = new ClassLectureDetails(); // You need to initialize this object with appropriate data.

            return View(model);


        }
    }
}
