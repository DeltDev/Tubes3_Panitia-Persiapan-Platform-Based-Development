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
        if (!isWordLengthSame(text, pattern))
        {
            return "Not match";
        }
        else 
        {
            if (ContainsAllAlphabets(textCompared, patternCompared))
            {
                return text;
            }
            else
            {
                return "Not match";
            }
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

    private static bool isWordLengthSame(string text, string pattern)
    {
        static int CountWords(string text)
        {
            int wordCount = 0;
            bool isWord = false;

            foreach (char c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (!isWord)
                    {
                        wordCount++;
                        isWord = true;
                    }
                }
                else if (char.IsWhiteSpace(c))
                {
                    isWord = false;
                }
            }
            return wordCount;
        }
        return CountWords(text) == CountWords(pattern);
    }
}

// class Program
// {
//       static void Main(string[] args)
//     {
//         string text = "Bintang Dwi Marthen";
//         string pattern = "ab1ntN6 Dw mrthn";

//         Console.WriteLine("Match Name: " + BahasaAlay.MatchName(text, pattern));
//     }
// }
