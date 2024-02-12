namespace Isu.Models;
using System.Text.RegularExpressions;

public class GroupName
{
    private const int MinLength = 5;
    private const int MaxLength = 6;
    public GroupName(string groupName)
    {
        if ((groupName.Length < MinLength) | (groupName.Length > MaxLength))
            throw new IsuException("Invalid group name");
        Faculty = groupName[0].ToString();
        EducationLevel = new EducationLevel(Convert.ToUInt16(groupName.Substring(1, 1)));
        CourseNumber = new CourseNumber(Convert.ToUInt16(groupName.Substring(2, 1)));
        GroupNumber = Convert.ToUInt16(groupName.Substring(3, 2));

        if (groupName.Length == 6)
        {
            Major = Convert.ToUInt16(groupName.Substring(5, 1));
        }
    }

    public string Faculty { get; }
    public EducationLevel EducationLevel { get; }
    public CourseNumber CourseNumber { get; }
    public int GroupNumber { get; }
    public int Major { get; }
}