
using Microsoft.AspNetCore.Mvc;
using Student.Api.DTOs;
using Student.Api.Services;

namespace Student.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/student
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchStudents([FromQuery] string name)
    {
        var students = await _studentService.SearchStudentsAsync(name);
        return Ok(students);
    }

    // GET: api/student/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);

        if (student == null)
            return NotFound(new { message = "Student not found." });

        return Ok(student);
    }

    // POST: api/student
    [HttpPost]
    public async Task<IActionResult> AddStudent(StudentCreateDto dto)
    {
        try
        {
            var createdStudent =
                await _studentService.AddStudentAsync(dto);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = createdStudent.Id },
                createdStudent
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/student/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(
        int id, StudentUpdateDto dto)
    {
        try
        {
            var updated =
                await _studentService.UpdateStudentAsync(id, dto);

            if (!updated)
                return NotFound(new { message = "Student not found." });

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/student/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted =
            await _studentService.DeleteStudentAsync(id);

        if (!deleted)
            return NotFound(new { message = "Student not found." });

        return NoContent();
    }

    [HttpGet("sorted")]
    public async Task<IActionResult> GetSortedStudents()
    {
        var students = await _studentService.GetSortedStudentsAsync();
        return Ok(students);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetStudentCount()
    {
        var count = await _studentService.GetStudentCountAsync();
        return Ok(new { totalStudents = count });
    }
    [HttpPost("insert-using-procedure")]
    public async Task<IActionResult> InsertUsingProcedure(StudentCreateDto dto)
    {
        try
        {
            await _studentService.InsertStudentUsingProcedureAsync(dto);

            return Ok(new { message = "Student inserted using PostgreSQL procedure." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
