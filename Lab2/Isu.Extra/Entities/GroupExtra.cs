using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : Group
{
    private List<StudentExtra> _students;
    public GroupExtra(GroupName name)
        : base(name)
    {
        _students = new List<StudentExtra>();
        Week1 = new ();
        Week2 = new ();
    }

    public Timetable Week1 { get; }
    public Timetable Week2 { get; }

    public new IReadOnlyCollection<StudentExtra> Students => _students.AsReadOnly();

    public void AddLesson(int week, Lesson newLesson)
    {
        if (week == 1)
            Week1.AddLesson(newLesson);
        else if (week == 2)
            Week2.AddLesson(newLesson);
        else
            throw new IsuException("Wrong week");
    }
}