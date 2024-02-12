namespace Isu.Extra.Entities;

public class Timetable
{
    private List<ILesson> _lessons;

    public Timetable()
    {
        _lessons = new List<ILesson>();
    }

    public IReadOnlyCollection<ILesson> Lessons => _lessons.AsReadOnly();
    public void AddLesson(ILesson newLesson)
    {
        if (_lessons.SingleOrDefault(l => newLesson.ClassTime.Day == l.ClassTime.Day & newLesson.ClassTime.Start <= l.ClassTime.End & newLesson.ClassTime.End >= l.ClassTime.Start) != null)
            throw new IsuException("Timetable intersection");

        _lessons.Add(newLesson);
    }
}