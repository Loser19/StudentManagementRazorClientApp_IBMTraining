using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentManagementRazorClientApp.Pages.Enrollment
{
    public class EnrollmentDetailsModel : PageModel
    {
        private readonly EnrollmentService _service;

        public EnrollmentDetailsModel(EnrollmentService service)
        {
            _service = service;
        }

        public EnrollmentModel Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var data = await _service.GetEnrollmentByIdAsync(id);
            if (data == null) return NotFound();

            Enrollment = data;
            return Page();
        }
    }
}
