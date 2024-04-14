using Student_Manager.DataContext;
using Student_Manager.Models;

namespace Student_Manager.Services
{
    public class StudentService
    {
        //private readonly StudentContext _context = default!;
        private readonly StudentContext _context = default!;
        public IList<Student> Students { get; set; }
        public StudentService(StudentContext context)
        {
            _context = context;
            Students = GetStudents();
        }

        public IList<Student> GetStudents()
        {
            if (_context.Students != null)
            {
                return _context.Students.ToList();
            }
            return new List<Student>();
        }

        public void AddStudent(Student student)
        {
            if (_context.Students != null)
            {
                _context.InsertStudent(student);

            }
        }
        public void UpdateStudent(int id, Student student)
        {
            if (_context.Students != null)
            {
                _context.UpdateStudent(id, student);
            }
        }

        public void DeleteStudent(int id)
        {
            if (_context.Students != null)
            {
                var student = _context.Students.Find(p => p.Id == id);
                if (student != null)
                {
                    _context.DeleteStudent(id);

                }
            }
        }
    }
}
