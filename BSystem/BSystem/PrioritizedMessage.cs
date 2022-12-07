using System.ComponentModel.DataAnnotations;

namespace BSystem;

[Serializable]
public class PrioritizedMessage
{
    [Required(AllowEmptyStrings = false)]
    public string MessageString { get; set; }
    [Required]
    public int Priority { get; set; }

    public PrioritizedMessage(string messageString, int priority)
    {
        MessageString = messageString;
        Priority = priority;
    }
}