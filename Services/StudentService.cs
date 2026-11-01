
using Student.Api.DTOs;
using Student.Api.Repositories;
using StudentModel = global::Student.Api.Models.Student;

namespace Student.Api.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    // GET ALL STUDENTS
    public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
    {
        var students = await _repository.GetAllAsync();

        return students.Select(student => new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Course = student.Course,
            Age = student.Age
        });
    }

    // GET STUDENT BY ID
    public async Task<StudentResponseDto?> GetStudentByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
            return null;

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Course = student.Course,
            Age = student.Age
        };
    }

    // ADD STUDENT
    public async Task<StudentResponseDto> AddStudentAsync(StudentCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Student name is required.");

        if (dto.Age <= 0)
            throw new ArgumentException("Age must be greater than zero.");

        var student = new StudentModel
        {
            Name = dto.Name,
            Email = dto.Email,
            Course = dto.Course,
            Age = dto.Age
        };

        var createdStudent = await _repository.AddAsync(student);

        return new StudentResponseDto
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Email = createdStudent.Email,
            Course = createdStudent.Course,
            Age = createdStudent.Age
        };
    }

    // UPDATE STUDENT
    public async Task<bool> UpdateStudentAsync(int id, StudentUpdateDto dto)
    {
        var existingStudent = await _repository.GetByIdAsync(id);

        if (existingStudent == null)
            return false;

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Student name is required.");

        if (dto.Age <= 0)
            throw new ArgumentException("Age must be greater than zero.");

        existingStudent.Name = dto.Name;
        existingStudent.Email = dto.Email;
        existingStudent.Course = dto.Course;
        existingStudent.Age = dto.Age;

        await _repository.UpdateAsync(existingStudent);

        return true;
    }

    // DELETE STUDENT
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<StudentResponseDto>> SearchStudentsAsync(string name)
    {
        var students = await _repository.SearchByNameAsync(name);

        return students.Select(student => new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Course = student.Course,
            Age = student.Age
        });
    }

    public async Task<IEnumerable<StudentResponseDto>> GetSortedStudentsAsync()
    {
        var students = await _repository.GetSortedStudentsAsync();

        return students.Select(student => new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Course = student.Course,
            Age = student.Age
        });
    }

    public async Task<int> GetStudentCountAsync()
    {
        return await _repository.GetStudentCountAsync();
    }
    public async Task InsertStudentUsingProcedureAsync(StudentCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Student name is required.");

        if (dto.Age <= 0)
            throw new ArgumentException("Age must be greater than zero.");

        var student = new StudentModel
        {
            Name = dto.Name,
            Email = dto.Email,
            Course = dto.Course,
            Age = dto.Age
        };

        await _repository.InsertStudentUsingProcedureAsync(student);
    }


}
