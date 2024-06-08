using System;
using System.Text.RegularExpressions;

public class Regex
{
    public static string Translate(string input)
    {
        string result = input;
        result = result.Replace("4", "a");
        result = result.Replace("1", "i");
        result = result.Replace("3", "e");
        result = result.Replace("6", "g");
        result = result.Replace("0", "o");
        result = result.Replace("5", "s");
        result = result.Replace("7", "t");
        result = result.Replace("8", "b");

        result = result.ToLower();

        return result;
    }

    public static string MatchName(string text, string pattern)
    {
        string textCompared = text.ToLower();
        string patternCompared = Translate(pattern);

        if (ContainsAllAlphabets(textCompared, patternCompared))
        {
            return text;
        }
        else
        {
            return "Not match";
        }
    }

    private static bool ContainsAllAlphabets(string text, string pattern)
    {
        foreach (char letter in pattern)
        {
            Console.WriteLine(letter);
            if (!text.Contains(letter.ToString()))
            {
                return false;
            }
        }
        return true;
    }

//     class Program
// {
//       static void Main(string[] args)
//     {
//         string text = "Bintang Dwi Marthen";
//         string pattern = "b1ntN6 Dw mrthn";

//         Console.WriteLine("Match Name: " + MatchName(text, pattern));
//     }
// }
}
