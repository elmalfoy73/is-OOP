namespace Isu.Extra.Models;

public class ClassTime
{
    public ClassTime(DayOfWeek day, TimeOnly start, TimeOnly end)
    {
        if (day == DayOfWeek.Sunday)
            throw new IsuException("No classes on Sunday");
        Day = day;
        Start = start;
        End = end;
    }

    public DayOfWeek Day { get; }
    public TimeOnly Start { get;  }
    public TimeOnly End { get; }
}