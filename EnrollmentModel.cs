using System.Text.Json.Serialization;
namespace StudentManagementRazorClientApp
{
    public record class EnrollmentModel(
        [property: JsonPropertyName("enrollmentId")] int EnrollmentId,
        [property: JsonPropertyName("studentId")] int StudentId,
        [property: JsonPropertyName("courseId")] int CourseId,
        [property: JsonPropertyName("enrollmentDate")] DateTime EnrollmentDate,
        [property: JsonPropertyName("student")] StudentModel? Student,
        [property: JsonPropertyName("course")] CourseModel? Course
        );
    
}
