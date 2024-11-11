using Microsoft.EntityFrameworkCore;
using WebONIT.Data;
using WebONIT.Requests;

namespace WebONIT.Managers
{
    public class StudentManager
    {
        private readonly OnitDbContext _dbContext;
        public StudentManager(OnitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudent(CreateStudentRequest createStudentRequest)
        {
            if (_dbContext.Students.Any(x => x.RecordBookNumber == createStudentRequest.RecordBookNumber))
                throw new Exception($"{createStudentRequest.RecordBookNumber} - этот номер зачетной книжки уже занят");

            Student newStudent = new Student()
            {
                FIO = createStudentRequest.FIO,
                RecordBookNumber = createStudentRequest.RecordBookNumber,
                BirthDate = createStudentRequest.BirthDate,
                AdmissionDate = createStudentRequest.AdmissionDate,
            };
            _dbContext.Students.Add(newStudent);
            await _dbContext.SaveChangesAsync();

            return newStudent;

        }
        public async Task<List<Student>> GetStudents(Filter filter)
        {
            List<Student> students = _dbContext.Students.ToList();
            if (filter.AdmissionDateAfter != null)
            {
                students = students.FindAll(x => x.AdmissionDate >= filter.AdmissionDateAfter);
            }
            if (filter.AdmissionDateBefore != null)
            {
                students = students.FindAll(x => x.AdmissionDate <= filter.AdmissionDateBefore);
            }
            if (filter.Older != null)
            {
                students = students.FindAll(x => GetAge(x.BirthDate) >= filter.Older);
            }
            if (filter.Younger != null)
            {

                students = students.FindAll(x => GetAge(x.BirthDate) <= filter.Younger);
            }
            return students;
        }
        public async Task<bool> DeleteStudent(int studentId)
        {
            Student student = _dbContext.Students.First(x => x.Id == studentId);
            if (student == null)
                throw new Exception($"Пользователь с Id - {studentId} не найден");
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public static int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }
    }
}
