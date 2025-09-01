using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementRazorClientApp;
using WebAppRazorCourseClient;
using WebAppRazorStudentClient;

namespace StudentManagementRazorClientApp.Pages.Enrollment
{
    public class EnrollmentEditModel : PageModel
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EnrollmentEditModel(EnrollmentService enrollmentService, StudentService studentService, CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]
        public EnrollmentModel EditEnrollment { get; set; } = default!;

        public SelectList StudentOptions { get; set; } = default!;
        public SelectList CourseOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            EditEnrollment = enrollment;
            await PopulateDropdownsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            var updated = await _enrollmentService.UpdateEnrollmentAsync(id, EditEnrollment);
            if (updated == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to update enrollment.");
                await PopulateDropdownsAsync();
                return Page();
            }

            return RedirectToPage("/Enrollment/EnrollmentList");
        }

        private async Task PopulateDropdownsAsync()
        {
            var students = await _studentService.GetStudents() ?? new List<StudentModel>();
            var courses = await _courseService.GetCourses() ?? new List<CourseModel>();

            StudentOptions = new SelectList(students, "StudentId", "Name");
            CourseOptions = new SelectList(courses, "CourseId", "Title");
        }
    }
}
