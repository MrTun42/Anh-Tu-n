using Student_Manager.Models;
using Student_Manager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



namespace Studednt_Manager.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _service;
        public IList<Student> StudentList { get; set; } = default!;

        [BindProperty]
        public Student NewStudent { get; set; } = default!;

        public IndexModel(StudentService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            StudentList = _service.GetStudents();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewStudent == null)
            {
                return Page();
            }

            _service.AddStudent(NewStudent);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteStudent(id);

            return RedirectToAction("Get");
        }

    }
}
