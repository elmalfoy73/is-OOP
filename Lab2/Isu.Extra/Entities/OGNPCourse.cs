using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNPCourse
{
    private List<OGNPStream> _streams;

    public OGNPCourse(string subject)
    {
        Subject = subject;
        _streams = new List<OGNPStream>();
    }

    public string Subject { get; }
    public IReadOnlyCollection<OGNPStream> Streams => _streams.AsReadOnly();

    public void AddStream(OGNPStream stream)
    {
        _streams.Add(stream);
    }
}