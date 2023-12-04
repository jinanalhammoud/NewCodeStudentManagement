using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement
{
    public class ClassesManager
    {
        private readonly ApplicationDBContext _context;
        public ClassesManager(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<Classes> GetClasses()
        {
            return _context.Classes.Include(x => x.Teacher).Include(x => x.Course).ToList();
        }

        public List<ClassInfo> GetClassInfos()
        {
            return GetClasses().Select(ClassInfoMapper).ToList();
        }

        public string GetClassTitle(Classes classItem)
        {
            return $"{classItem.Course.Code} - {classItem.GroupName} - {classItem.Year} - {classItem.Semester}";
        }

        private ClassInfo ClassInfoMapper(Classes classItem)
        {
            return new ClassInfo
            {
                Id = classItem.Id,
                Title = GetClassTitle(classItem)
            };
        }
    }
}
