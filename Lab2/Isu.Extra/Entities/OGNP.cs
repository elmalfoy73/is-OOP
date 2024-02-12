using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNP
{
   public OGNP(string name, MegaFaculty megaFaculty, OGNPCourse course1, OGNPCourse course2)
    {
        Name = name;
        MegaFaculty = megaFaculty;
        Course1 = course1;
        Course2 = course2;
    }

   public string Name { get;  }
   public MegaFaculty MegaFaculty { get;  }

   public OGNPCourse Course1 { get;  }

   public OGNPCourse Course2 { get;  }

   public void AddStudent(StudentExtra student, OGNPStream stream1, OGNPStream stream2)
   {
       if (!Course1.Streams.Contains(stream1))
           throw new IsuException("This OGNP stream doesn't exist");
       if (!Course2.Streams.Contains(stream2))
           throw new IsuException("This OGNP stream  doesn't exist");
       stream1.AddStudent(student);
       stream2.AddStudent(student);
   }
}