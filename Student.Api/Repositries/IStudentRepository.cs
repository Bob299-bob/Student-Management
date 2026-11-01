using StudentModel = global::Student.Api.Models.Student;

namespace Student.Api.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentModel>> GetAllAsync();


        Task<StudentModel?> GetByIdAsync(int id);

        Task<StudentModel> AddAsync(StudentModel student);

        Task UpdateAsync(StudentModel student);

        Task DeleteAsync(int id);
        Task<IEnumerable<StudentModel>> SearchByNameAsync(string name);

        Task<IEnumerable<StudentModel>> GetSortedStudentsAsync();
        Task<int> GetStudentCountAsync();
        Task InsertStudentUsingProcedureAsync(StudentModel student);
    }
}