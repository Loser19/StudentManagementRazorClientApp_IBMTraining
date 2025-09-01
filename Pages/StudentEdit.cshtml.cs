using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementRazorClientApp;

namespace WebAppRazorStudentClient.Pages
{
    public class StudentEditModel : PageModel
    {
        private readonly StudentService _service;

        public StudentEditModel(StudentService service)
        {
            _service = service;
        }

        [BindProperty]
        public StudentModel EditStudent { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var student = await _service.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            EditStudent = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           await _service.UpdateStudentAsync(EditStudent.StudentId,EditStudent);
            //if (updated == null)
            //{
            //    ModelState.AddModelError(string.Empty, "Update failed.");
            //    return Page();
            //}

            return RedirectToPage("StudentList");
        }
    }
}
