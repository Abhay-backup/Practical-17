using Practical_17.Models;

namespace Practical_17.Interfaces
{
	public interface IStudentRepository
	{
		List<Student> GetStudents();

		Student GetStudentById(int id);

		Task AddStudentAsync(Student student);

		Task<bool> UpdateStudentAsync(int id, Student student);

		Task DeleteStudentAsync(int id);

		bool StudentExists(int id);
	}
}
