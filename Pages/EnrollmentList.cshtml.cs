using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementRazorClientApp.Pages
{
    public class EnrollmentListModel : PageModel
    {
        private readonly EnrollmentService _service;

        public EnrollmentListModel(EnrollmentService service)
        {
            _service = service;
        }

        public Task<List<EnrollmentModel>> Enrollments { get; set; } = default!;
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _service.DeleteEnrollmentAsync(id);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete enrollment.");
            }

            return RedirectToPage();
        }

        //public async Task OnGetAsync()
        //{
        //    Enrollments = await _service.GetEnrollmentsAsync();
        //}
        public void OnGet()
        {
            Enrollments = _service.GetEnrollmentsAsync();
        }
    }
}
