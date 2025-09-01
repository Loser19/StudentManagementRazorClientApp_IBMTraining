using StudentManagementRazorClientApp;
using System.Text;
using System.Text.Json;
using WebAppRazorStudentClient;

namespace WebAppRazorStudentClient
{
    public class StudentService
    {
        public StudentService()
        {
        }

        public async Task<List<StudentModel>> GetStudents()
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                HttpClient client = new HttpClient(handler);
                await using Stream stream = await client.GetStreamAsync("https://localhost:7221/api/Student");

                var students = await JsonSerializer.DeserializeAsync<List<StudentModel>>(stream);
                return students ?? new List<StudentModel>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw;
            }
        }

        public async Task<StudentModel?> AddStudent(StudentModel student)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PostAsJsonAsync("https://localhost:7221/api/Student", student);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                    return null;
                }

                var createdStudent = await response.Content.ReadFromJsonAsync<StudentModel>();
                return createdStudent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

        public async Task<StudentModel?> GetStudentByIdAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.GetAsync($"https://localhost:7221/api/Student/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var student = await response.Content.ReadFromJsonAsync<StudentModel>();
                return student;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StudentModel?> UpdateStudentAsync(int id,StudentModel student)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.PutAsJsonAsync($"https://localhost:7221/api/Student/{id}", student);

                if (!response.IsSuccessStatusCode)
                    return null;

                var updatedStudent = await response.Content.ReadFromJsonAsync<StudentModel>();
                return updatedStudent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using HttpClient client = new HttpClient(handler);
                var response = await client.DeleteAsync($"https://localhost:7221/api/Student/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
