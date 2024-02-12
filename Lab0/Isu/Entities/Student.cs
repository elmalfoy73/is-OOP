using Isu.Models;

namespace Isu.Entities;
public class Student
{
    public Student(string name, Group group, int id)
    {
        Name = name;
        Group = group;
        group.AddStudent(this);
        Id = id;
    }

    public int Id { get; }
    public string Name { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        if (newGroup.Students.Count >= Group._maxStudentsAmount)
            throw new IsuException("New group students limit exceeded");
        newGroup.AddStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }
}