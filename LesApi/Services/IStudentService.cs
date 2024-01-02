using LesApi.Models;

namespace LesApi.Services
{
    public interface IStudentService
    {
        List<Student> Get();
        Student GetStudent(string id);
        Student Create(Student student);
        void Update(string id , Student student);
        void Remove(string id);

    }
}
