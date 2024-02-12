using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra;

public class IsuServiceExtra : IsuService
{
    private IdCounter _idCounter = new ();
    private List<GroupExtra> _groups = new List<GroupExtra>();
    public new GroupExtra AddGroup(GroupName name)
    {
        GroupExtra group = FindGroup(name);
        if (group == null)
        {
            group = new GroupExtra(name);
            _groups.Add(group);
        }

        return group;
    }

    public StudentExtra AddStudent(GroupExtra group, string name)
    {
        if (group == null || !_groups.Contains(group))
            throw new IsuException("This group doesn't exist");
        int id = _idCounter.GetId();
        StudentExtra student = new (name, group, id);
        return student;
    }

    public new GroupExtra FindGroup(GroupName groupName)
    {
        return _groups.SingleOrDefault(g => g.Name == groupName);
    }

    public Lesson AddLesson(int week, string subject, GroupExtra group, ClassTime classTime, string teacher, int classRoom)
    {
       Lesson lesson = new Lesson(subject, group, classTime, teacher, classRoom);
       group.AddLesson(week, lesson);
       return lesson;
    }

    public OGNPStream AddOgnpStream(OGNPCourse course, string name)
    {
        OGNPStream stream = new OGNPStream(name);
        course.AddStream(stream);
        return stream;
    }

    public OGNP AddOGNP(string name, MegaFaculty megaFaculty, OGNPCourse course1, OGNPCourse course2)
    {
        return new OGNP(name, megaFaculty, course1, course2);
    }

    public OGNPLesson AddOgnpLeson(int week, OGNPCourse course, OGNPStream stream, ClassTime classTime, string teacher, int classRoom)
    {
        OGNPLesson lesson = new OGNPLesson(course.Subject, classTime, teacher, classRoom);
        stream.AddLesson(week, lesson);
        return lesson;
    }

    public void AddStudentToOgnp(StudentExtra student, OGNP ognp, OGNPStream stream1, OGNPStream stream2)
    {
        if (ognp.MegaFaculty.Faculties.Contains(student.Group.Name.Faculty))
            throw new IsuException("This OGNP from a student's megafacultet");
        student.AddOGNP(ognp, stream1, stream2);
    }

    public void RemoveStudentFromOgnp(StudentExtra student)
    {
        student.RemoveOGNP();
    }

    public List<OGNPStream> FindStreams(OGNPCourse course)
    {
        return course.Streams.ToList();
    }

    public List<StudentExtra> FindOGNPStreamStudents(OGNPStream ognpStream)
    {
       return ognpStream.Students.ToList();
    }

    public List<StudentExtra> FindStudentsWithoutOGNP(GroupExtra group)
    {
        return new List<StudentExtra>(group.Students.Where(s => s.OGNPStream1 == null || s.OGNPStream2 == null));
    }
}