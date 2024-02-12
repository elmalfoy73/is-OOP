namespace Isu.Models;

public class IdCounter
{
    private int _idCounter = 100000;

    public int GetId()
    {
        return _idCounter++;
    }
}