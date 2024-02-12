using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private IdCounter _idCounter = new ();
    private List<Group> _groups = new List<Group>();
    public Group AddGroup(GroupName name)
    {
        Group group = FindGroup(name);
        if (group == null)
        {
            group = new Group(name);
            _groups.Add(group);
        }

        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        if (!_groups.Contains(group) | group == null)
            throw new IsuException("This group doesn't exist");
        int id = _idCounter.GetId();
        Student student = new (name, group, id);
        return student;
    }

    public Student GetStudent(int id)
    {
        var student = GetStudent(id);
        if (student == null)
            throw new IsuException("This student doesn't exist");
        return student;
    }

    public Student FindStudent(int id)
    {
        return _groups.SelectMany(g => g.Students).SingleOrDefault(s => s.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group group = FindGroup(groupName);
        if (!_groups.Contains(group) | group == null)
            throw new IsuException("This group doesn't exist");
        return group.Students.ToList();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var students = new List<Student>();
        List<Group> groups = FindGroups(courseNumber);
        foreach (Group group in groups)
        {
            students.AddRange(group.Students.ToList());
        }

        return students;
    }

    public Group FindGroup(GroupName groupName)
    {
        return _groups.SingleOrDefault(g => g.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return new List<Group>(_groups.Where(g => g.Name.CourseNumber == courseNumber));
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (!_groups.Contains(newGroup) | newGroup == null)
            throw new IsuException("New group doesn't exist");
        if (FindStudent(student.Id) == null)
            throw new IsuException("This student doesn't exist");
        student.ChangeGroup(newGroup);
    }
}