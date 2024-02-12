namespace Isu.Extra.Models;

public class MegaFaculty
{
    private List<string> _faculties;

    public MegaFaculty(string name, List<string> faculties)
    {
        Name = name;
        _faculties = faculties;
    }

    public string Name { get; }
    public IReadOnlyCollection<string> Faculties => _faculties.AsReadOnly();

    public void AddFaculty(string faculty)
    {
        _faculties.Add(faculty);
    }
}