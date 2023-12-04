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
        private readonly ClassesManager _classesManager;

        public ClassLecturesController(ApplicationDBContext context, ClassesManager classesManager)
        {
            _context = context;
            _classesManager = classesManager;
        }

        // GET: ClassLectures
        public async Task<IActionResult> Index()
        {
            if (_context.ClassLectures != null)
            {
                var classLectures = await _context.ClassLectures.Include(x => x.Class).ThenInclude(x => x.Course).ToListAsync();
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
            var classes = _classesManager.GetClassInfos();
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
            ModelState.Remove(nameof(classLecture.Class));

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
            if (id == null || _context.ClassLectures == null)
            {
                return NotFound();
            }

            var classLecture = await _context.ClassLectures.FindAsync(id);
            if (classLecture == null)
            {
                return NotFound();
            }
            var classes = _classesManager.GetClassInfos();
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

            ModelState.Remove(nameof(classLecture.Class));

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
            if (id == null || _context.ClassLectures == null)
            {
                return NotFound();
            }

            var classLecture = ClassLectureDetailsMapper(await _context.ClassLectures.Include(x => x.Class).ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(m => m.Id == id));
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
            if (_context.ClassLectures == null)
            {
                return Problem("Entity set 'ApplicationDBContext.classLectures'  is null.");
            }
            var classLecture = await _context.ClassLectures.FindAsync(id);
            if (classLecture != null)
            {
                _context.ClassLectures.Remove(classLecture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassLectureExists(int id)
        {
            return (_context.ClassLectures?.Any(e => e.Id == id)).GetValueOrDefault();
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
                Class = $"{classLecture.Class.Course.Code} - {classLecture.Class.GroupName}",
            };

            return classLectureDetails;
        }
    }
}
