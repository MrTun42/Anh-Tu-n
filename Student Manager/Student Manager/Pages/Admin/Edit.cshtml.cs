using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Student_Manager.Models;
using Student_Manager.Services;

namespace Student_Manager.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly StudentService _service;
        public EditModel(StudentService service)
        {
            _service = service;
        }
        [BindProperty]
        public Student Students { get; set; }

        public async Task<IActionResult> OnGetAsync(int? itemid)
        {
            if (itemid == null)
            {
                return NotFound();
            }
            var student = _service.Students.FirstOrDefault(p => p.Id == itemid);
            if (student == null)
            {
                return NotFound();
            }
            Students = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.UpdateStudent(Students.Id, Students);
            return RedirectToPage(nameof(Index));
        }
    }
}
