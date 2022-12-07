using System.Text;

namespace ASystem.RandomMessageGeneration;

public class RandomMessageStringGenerator : IMessageStringGenerator
{
    private char _letter;
    private int _randNumber;
    private readonly Random _rand = new();
    public string Generate()
    {
        int strLength = _rand.Next(1, 10);

        StringBuilder sb = new();
        for (var i = 0; i < strLength; i++)
        {
            _randNumber = _rand.Next(0, 26);

            _letter = Convert.ToChar(_randNumber + 65);

            sb.Append(_letter);
        }

        return sb.ToString();
    }
}