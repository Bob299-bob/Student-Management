using Microsoft.EntityFrameworkCore;
using Student.Api.Data;
using StudentModel = global::Student.Api.Models.Student;

namespace Student.Api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all students
        public async Task<IEnumerable<StudentModel>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }


        // Get student by ID
        public async Task<StudentModel?> GetByIdAsync(int id)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Add student
        public async Task<StudentModel> AddAsync(StudentModel student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return student;
        }

        // Update student
        public async Task UpdateAsync(StudentModel student)
        {
            _context.Students.Update(student);

            await _context.SaveChangesAsync();
        }

        // Delete student
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
            }
        }
        //Using LINQ
        public async Task<IEnumerable<StudentModel>> SearchByNameAsync(string name)
        {
            return await _context.Students
                .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentModel>> GetSortedStudentsAsync()
        {
            return await _context.Students
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
        public async Task<int> GetStudentCountAsync()
        {
            return await _context.Students.CountAsync();
        }
        public async Task InsertStudentUsingProcedureAsync(StudentModel student)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"CALL insert_student({student.Name}, {student.Email}, {student.Course}, {student.Age})"
            );
        }

    }
}