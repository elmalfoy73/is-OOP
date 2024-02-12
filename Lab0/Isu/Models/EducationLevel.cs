namespace Isu.Models;

public class EducationLevel
{
    private const int MinLevel = 3;
    private const int MaxLevel = 5;
    private int _value;
    public EducationLevel(int level)
    {
        if (level < MinLevel || level > MaxLevel)
            throw new IsuException("Invalid education level");

        _value = level;
    }
}