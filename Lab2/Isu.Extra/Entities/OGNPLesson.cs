using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNPLesson : ILesson
{
    public OGNPLesson(string subject, ClassTime classTime, string teacher, int classRoom)
    {
        Subject = subject;
        ClassTime = classTime;
        Teacher = teacher;
        ClassRoom = classRoom;
    }

    public string Subject { get; }
    public ClassTime ClassTime { get; }
    public string Teacher { get; }
    public int ClassRoom { get; }
}