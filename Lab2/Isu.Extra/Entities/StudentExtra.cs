using Isu.Entities;

namespace Isu.Extra.Entities;

public class StudentExtra : Student
{
    public StudentExtra(string name, GroupExtra group, int id)
        : base(name, group, id)
    {
        Group = group;
    }

    public new GroupExtra Group { get; }
    public OGNPStream OGNPStream1 { get; private set; }
    public OGNPStream OGNPStream2 { get; private set; }

    public void AddOGNP(OGNP ognp, OGNPStream ognpStream1, OGNPStream ognpStream2)
    {
        ognp.AddStudent(this, ognpStream1, ognpStream2);
        OGNPStream1 = ognpStream1;
        OGNPStream2 = ognpStream2;
    }

    public void RemoveOGNP()
    {
        if (OGNPStream1 == null | OGNPStream2 == null)
            throw new IsuException("Student already hasn't OGNP");
        OGNPStream1.RemoveStudent(this);
        OGNPStream2.RemoveStudent(this);
        OGNPStream1 = null;
        OGNPStream2 = null;
    }
}