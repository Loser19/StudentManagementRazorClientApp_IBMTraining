using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppRazorCourseClient;
using WebAppRazorStudentClient;

namespace StudentManagementRazorClientApp.Pages.Enrollment
{
    public class EnrollmentCreateModel : PageModel
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public EnrollmentCreateModel(EnrollmentService enrollmentService, StudentService studentService, CourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        [BindProperty]
        public EnrollmentModel Enrollment { get; set; } = default!;
        //[BindProperty]
        public SelectList Students { get; set; } = default!;
        //[BindProperty]
        public SelectList Courses { get; set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    Students = new SelectList(await _studentService.GetStudents(), "StudentId", "Name");
        //    Courses = new SelectList(await _courseService.GetCourses(), "CourseId", "Title");
        //}
        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateDropdownsAsync();
            return Page();
        } 

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            var result = await _enrollmentService.AddEnrollmentAsync(Enrollment);
            if (result == null)
            {
                ModelState.AddModelError("", "Failed to create enrollment.");
                return Page();
            }

            return RedirectToPage("EnrollmentList");
        }
        private async Task PopulateDropdownsAsync()
        {
            var students = await _studentService.GetStudents() ?? new List<StudentModel>();
            var courses = await _courseService.GetCourses() ?? new List<CourseModel>();

            Students = new SelectList(students, "StudentId", "Name");
            Courses = new SelectList(courses, "CourseId", "Title");
        }
    }
}
