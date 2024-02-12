using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Lesson : ILesson
{
    public Lesson(string subject, GroupExtra group, ClassTime classTime, string teacher, int classRoom)
    {
        Subject = subject;
        Group = group;
        ClassTime = classTime;
        Teacher = teacher;
        ClassRoom = classRoom;
    }

    public GroupExtra Group { get; }
    public string Subject { get; }
    public ClassTime ClassTime { get; }
    public string Teacher { get; }
    public int ClassRoom { get; }
}