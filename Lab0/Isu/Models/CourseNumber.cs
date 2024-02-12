using Isu.Entities;
namespace Isu.Models;

public class CourseNumber
{
    private const int MinCourse = 1;
    private const int MaxCourse = 4;
    private int _value;

    public CourseNumber(int course)
    {
        if (course < MinCourse || course > MaxCourse)
            throw new IsuException("Invalid course number");

        _value = course;
    }
}