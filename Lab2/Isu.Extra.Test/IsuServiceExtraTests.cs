using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuServiceExtraTests
{
    private IsuServiceExtra _isuServiceExtra = new IsuServiceExtra();

    [Fact]
    public GroupExtra CreateGroupWithTimetable()
    {
        GroupExtra group1 = _isuServiceExtra.AddGroup(new GroupName("M32061"));
        string oop = "OOП";
        string lectorOOP = "Носовицкий";
        string tutorOOP = "Чикишев";
        Lesson lesson1 = _isuServiceExtra.AddLesson(1, oop, group1, new ClassTime(DayOfWeek.Friday, new TimeOnly(10, 0), new TimeOnly(11, 30)), lectorOOP, 1229);
        Lesson lesson2 = _isuServiceExtra.AddLesson(1, oop, group1, new ClassTime(DayOfWeek.Saturday, new TimeOnly(13, 30), new TimeOnly(15, 0)), tutorOOP, 2335);
        Lesson lesson3 = _isuServiceExtra.AddLesson(1, oop, group1, new ClassTime(DayOfWeek.Saturday, new TimeOnly(15, 20), new TimeOnly(16, 50)), tutorOOP, 2335);
        string os = "OСи";
        string сolleague = "Маятин";
        Lesson lesson4 = _isuServiceExtra.AddLesson(1, os, group1, new ClassTime(DayOfWeek.Tuesday, new TimeOnly(10, 0), new TimeOnly(11, 30)), сolleague, 2337);

        Assert.Contains(lesson1, group1.Week1.Lessons);
        Assert.Contains(lesson2, group1.Week1.Lessons);
        Assert.Contains(lesson3, group1.Week1.Lessons);
        Assert.Contains(lesson4, group1.Week1.Lessons);

        return group1;
    }

    [Fact]
    public void CreateOGNP()
    {
        MegaFaculty ctu = new ("КТУ", new List<string>() { "R", "P", "N", "H" });
        string clown = "Препод с КТУ";
        OGNPCourse cs = new ("Кибербез");
        OGNPCourse po = new ("Методы и средства ПО");
        OGNP ognp = _isuServiceExtra.AddOGNP("ОГНП КТУ", ctu, cs, po);
        OGNPStream csStream1 = _isuServiceExtra.AddOgnpStream(cs, "1");
        OGNPStream onlineStream2 = _isuServiceExtra.AddOgnpStream(po, "1");
        OGNPLesson ognpLesson = _isuServiceExtra.AddOgnpLeson(1, cs, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(13, 30), new TimeOnly(15, 0)), clown, 311);
        _isuServiceExtra.AddOgnpLeson(1, cs, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(15, 20), new TimeOnly(16, 50)), clown, 111);
        _isuServiceExtra.AddOgnpLeson(1, po, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(17, 0), new TimeOnly(18, 30)), clown, 111);

        Assert.Contains(ognpLesson, csStream1.Week1.Lessons);
        Assert.Contains(csStream1, cs.Streams);
        Assert.Contains(csStream1, _isuServiceExtra.FindStreams(cs));
        Assert.Equal(cs, ognp.Course1);
    }

    [Fact]
    public StudentExtra EnrollStudentOnTheOGNP()
    {
        MegaFaculty ctu = new ("КТУ", new List<string>() { "R", "P", "N", "H" });
        string clown = "Препод с КТУ";
        OGNPCourse cs = new ("Кибербез");
        OGNPCourse po = new ("Методы и средства ПО");
        OGNP ognp = _isuServiceExtra.AddOGNP("ОГНП КТУ", ctu, cs, po);
        OGNPStream csStream1 = _isuServiceExtra.AddOgnpStream(cs, "1");
        OGNPStream poStream1 = _isuServiceExtra.AddOgnpStream(po, "1");
        _isuServiceExtra.AddOgnpLeson(1, cs, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(13, 30), new TimeOnly(15, 0)), clown, 311);
        _isuServiceExtra.AddOgnpLeson(1, cs, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(15, 20), new TimeOnly(16, 50)), clown, 111);
        _isuServiceExtra.AddOgnpLeson(1, po, csStream1, new ClassTime(DayOfWeek.Wednesday, new TimeOnly(17, 0), new TimeOnly(18, 30)), clown, 111);

        GroupExtra group = CreateGroupWithTimetable();
        StudentExtra student = _isuServiceExtra.AddStudent(group, "Lisa");
        _isuServiceExtra.AddStudentToOgnp(student, ognp, csStream1, poStream1);

        Assert.Contains(student, csStream1.Students);
        Assert.Contains(student, poStream1.Students);
        Assert.Equal(csStream1, student.OGNPStream1);
        Assert.Equal(poStream1, student.OGNPStream2);
        Assert.Contains(student, _isuServiceExtra.FindOGNPStreamStudents(csStream1));

        return student;
    }

    [Fact]
    public void RemoveStudentFromOgnp()
    {
        StudentExtra student = EnrollStudentOnTheOGNP();

        _isuServiceExtra.RemoveStudentFromOgnp(student);

        Assert.Null(student.OGNPStream1);
        Assert.Null(student.OGNPStream2);
    }
}