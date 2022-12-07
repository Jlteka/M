namespace ASystem.RandomMessageGeneration;

public class PrioritizedMessage
{
    public string MessageString { get; set; }
    public int Priority { get; set; }

    public PrioritizedMessage(string messageString, int priority)
    {
        MessageString = messageString;
        Priority = priority;
    }
}