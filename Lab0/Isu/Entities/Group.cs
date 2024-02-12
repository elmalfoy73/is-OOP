using Isu.Models;
using Exception = System.Exception;

namespace Isu.Entities;

public class Group
{
    public const int _maxStudentsAmount = 31;
    private List<Student> _students;

    public Group(GroupName name)
    {
        Name = name;
        _students = new List<Student>();
    }

    public GroupName Name { get; }

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    public void AddStudent(Student student)
    {
        if (_students.Contains(student))
            throw new IsuException("The student is already in the group");
        if (_students.Count >= _maxStudentsAmount)
            throw new IsuException("Students limit exceeded");
        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
            throw new IsuException("The student isn't in this group");
        _students.Remove(student);
    }
}