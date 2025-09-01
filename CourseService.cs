using StudentManagementRazorClientApp;
using System.Text;
using System.Text.Json;
using WebAppRazorCourseClient;

namespace WebAppRazorCourseClient
{
    public class CourseService
    {
        public CourseService()
        {
        }

        public async Task<List<CourseModel>> GetCourses()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7221/api/Course");

                var courses = await JsonSerializer.DeserializeAsync<List<CourseModel>>(stream);
                return courses ?? new List<CourseModel>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw;
            }
        }

        public async Task<CourseModel?> AddCourse(CourseModel course)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7221/api/Course", course);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                var createdCourse = await response.Content.ReadFromJsonAsync<CourseModel>();
                return createdCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<CourseModel?> GetCourseByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7221/api/Course/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var course = await response.Content.ReadFromJsonAsync<CourseModel>();
                return course;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CourseModel?> UpdateCourseAsync(int id,CourseModel course)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7221/api/Course/{id}", course);

                if (!response.IsSuccessStatusCode)
                    return null;

                var updatedCourse = await response.Content.ReadFromJsonAsync<CourseModel>();
                return updatedCourse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7221/api/Course/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
