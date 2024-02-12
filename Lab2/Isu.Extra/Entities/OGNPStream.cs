namespace Isu.Extra.Entities;

public class OGNPStream
{
    public const int _maxStudentsAmount = 31;
    private List<StudentExtra> _students;

    public OGNPStream(string name)
    {
        Name = name;
        _students = new List<StudentExtra>();
        Week1 = new ();
        Week2 = new ();
    }

    public string Name { get;  }
    public Timetable Week1 { get; }
    public Timetable Week2 { get; }
    public IReadOnlyCollection<StudentExtra> Students => _students.AsReadOnly();

    public void AddLesson(int week, OGNPLesson newLesson)
    {
        if (week == 1)
            Week1.AddLesson(newLesson);
        else if (week == 2)
            Week2.AddLesson(newLesson);
        else
            throw new IsuException("Wrong week");
    }

    public void AddStudent(StudentExtra student)
    {
        if (_students.Contains(student))
            throw new IsuException("The student is already in the OGNP Stream");
        if (_students.Count >= _maxStudentsAmount)
            throw new IsuException("Students limit exceeded");
        foreach (var lesson in student.Group.Week1.Lessons)
        {
            if (Week1.Lessons.SingleOrDefault(l => lesson.ClassTime.Day == l.ClassTime.Day & lesson.ClassTime.Start <= l.ClassTime.End & lesson.ClassTime.End >= l.ClassTime.Start) != null)
                throw new IsuException("Timetable intersection");
        }

        foreach (var lesson in student.Group.Week2.Lessons)
        {
            if (Week2.Lessons.SingleOrDefault(l => lesson.ClassTime.Day == l.ClassTime.Day & lesson.ClassTime.Start <= l.ClassTime.End & lesson.ClassTime.End >= l.ClassTime.Start) != null)
                throw new IsuException("Timetable intersection");
        }

        _students.Add(student);
    }

    public void RemoveStudent(StudentExtra student)
    {
        if (!_students.Contains(student))
            throw new IsuException("The student isn't in this OGNP Stream");
        _students.Remove(student);
    }
}