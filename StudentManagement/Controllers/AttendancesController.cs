using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ClassesManager _classesManager;

        public AttendancesController(ApplicationDBContext context, ClassesManager classesManager)
        {
            _context = context;
            _classesManager = classesManager;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            if (_context.Attendances != null)
            {
                var attendances = await _context.Attendances.Include(x => x.Lecture).ThenInclude(x => x.Class).ThenInclude(x => x.Course).Include(x => x.Student).ToListAsync();
                var attendancesDetails = new List<AttendanceDetails>();
                if (attendances != null && attendances.Count > 0)
                {
                    attendances.ForEach(att => attendancesDetails.Add(AttendanceDetailsMapper(att)));
                }
                return View(attendancesDetails);
            }
            else
                return Problem("Entity set 'ApplicationDBContext.Attendances'  is null.");
        }

        private AttendanceDetails AttendanceDetailsMapper(Attendance attendance)
        {
            return new AttendanceDetails
            {
                Id = attendance.Id,
                Lecture = attendance.Lecture.Title,
                Class = $"{attendance.Lecture.Class.Course.Code} - {attendance.Lecture.Class.GroupName}",
                Student = attendance.Student.Name
            };
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TakeAttendance()
        {
            // Retrieve a list of classes from your database or another source.
            var classes = _classesManager.GetClassInfos();

            var viewModel = new SelectClassViewModel
            {
                Classes = new SelectList(classes, "Id", "Title")
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult MarkAttendance(int selectedClassId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(TakeAttendance));
            }
            // Based on the selectedClassId, retrieve the list of students registered for that class.

            var classLectures = _context.ClassLectures.Where(lec => lec.ClassId == selectedClassId)?.ToList();
            var lecturesList = new SelectList(classLectures, "Id", "Title");

            var registredStudentsIds = _context.ClassRegistrations
                .Where(reg => reg.ClassId == selectedClassId)?
                .Select(reg => reg.StudentId)
                .ToList();

            var registeredStudentsViewModel = new List<StudentViewModel>();

            if (registredStudentsIds != null && registredStudentsIds.Count > 0)
            {
                var registeredStudents = _context.Students.Where(std => registredStudentsIds.Contains(std.Id))?.ToList();
                foreach (var reg in registeredStudents)
                {
                    registeredStudentsViewModel.Add(new StudentViewModel
                    {
                        StudentId = reg.Id,
                        StudentName = reg.Name,
                    });
                }
            }

            var viewModel = new MarkAttendanceViewModel
            {
                SelectedClassId = selectedClassId,
                Students = registeredStudentsViewModel,
                Lectures = lecturesList
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveAttendance(MarkAttendanceViewModel viewModel)
        {
            ModelState.Remove("Lectures");
            if (ModelState.IsValid)
            {
                var existingAttendances = _context.Attendances;
                // Process attendance data, save it to the database, and redirect to a success page.
                // For example, you might loop through viewModel.Students and save their attendance.
                foreach (var student in viewModel.Students)
                {
                    var existingStudentAttendance = existingAttendances?.Where(x => x.StudentId == student.StudentId && x.LectureId == viewModel.SelectedLectureId)?.FirstOrDefault();
                    if (student.Attended && existingStudentAttendance == null)
                    {
                        var attendance = new Attendance
                        {
                            LectureId = viewModel.SelectedLectureId,
                            StudentId = student.StudentId,
                        };
                        _context.Attendances.Add(attendance);
                    }
                    if (!student.Attended && existingStudentAttendance != null)
                    {
                        existingAttendances.Remove(existingStudentAttendance);
                    }
                }

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(TakeAttendance));
            }
        }

        private bool AttendanceExists(int id)
        {
            return (_context.Attendances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
