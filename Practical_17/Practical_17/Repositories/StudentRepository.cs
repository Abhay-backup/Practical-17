using Practical_17.Data;
using Practical_17.Interfaces;
using Practical_17.Models;

namespace Practical_17.Repositories
{
	public class StudentRepository : IStudentRepository
	{
		private readonly DatabaseContext _db;

		public StudentRepository(DatabaseContext db)
		{
			this._db = db;
		}

		public async Task AddStudentAsync(Student student)
		{
			_db.Students.Add(student);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteStudentAsync(int id)
		{
			if (StudentExists(id))
			{
				_db.Students.Remove(_db.Students.First(x => x.Id == id));
				await _db.SaveChangesAsync();
			}
		}

		public Student GetStudentById(int id)
		{
			if (!StudentExists(id))
			{
				return null;
			}
			return _db.Students.First(x => x.Id == id);
		}

		public List<Student> GetStudents()
		{
			return _db.Students.ToList();
		}

		public bool StudentExists(int id)
		{
			return _db.Students.Any(x => x.Id == id);
		}

		public async Task<bool> UpdateStudentAsync(int id, Student student)
		{
			Student temp = _db.Students.FirstOrDefault(x => x.Id == id);
			if (temp != null)
			{
				temp.Name = student.Name;
				temp.Standard = student.Standard;
				temp.Age = student.Age;
				await _db.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}


	}
}
