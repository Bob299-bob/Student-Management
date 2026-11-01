
using Student.Api.DTOs;

namespace Student.Api.Services;

public interface IStudentService
{
    Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();

    Task<StudentResponseDto?> GetStudentByIdAsync(int id);

    Task<StudentResponseDto> AddStudentAsync(StudentCreateDto dto);

    Task<bool> UpdateStudentAsync(int id, StudentUpdateDto dto);

    Task<bool> DeleteStudentAsync(int id);

    Task<IEnumerable<StudentResponseDto>> SearchStudentsAsync(string name);
    Task<IEnumerable<StudentResponseDto>> GetSortedStudentsAsync();
    Task<int> GetStudentCountAsync();
    Task InsertStudentUsingProcedureAsync(StudentCreateDto dto);

}
