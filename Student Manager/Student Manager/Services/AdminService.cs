using Student_Manager.DataContext;
using Student_Manager.Models;

namespace Student_Manager.Services
{
    public class AdminService
    {
        //private readonly StudentContext _context = default!;
        private readonly AdminContext _context = default!;
        public IList<Admin> Teachers { get; set; }
        public AdminService(AdminContext context)
        {
            _context = context;
            Teachers = GetStudents();
        }

        public IList<Admin> GetStudents()
        {
            if (_context.Teachers != null)
            {
                return _context.Teachers.ToList();
            }
            return new List<Admin>();
        }

        public void AddTeacher(Admin teacher)
        {
            if (_context.Teachers != null)
            {
                _context.InsertTeacher(teacher);

            }
        }
        public void UpdateTeacher(int id, Admin teacher)
        {
            if (_context.Teachers != null)
            {
                _context.InsertTeacher(teacher);
            }
        }

        public void DeleteTeacher(int id)
        {
            if (_context.Teachers != null)
            {
                var teacher = _context.Teachers.Find(p => p.Id == id);
                if (teacher != null)
                {
                    _context.DeleteTeacher(id);

                }
            }
        }
    }
}
