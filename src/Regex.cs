using System;
using System.Text.RegularExpressions;

public class Regex
{
    public static string Translate(string input)
    {
        string result = input;
        result = result.Replace("4", "a");
        result = result.Replace("2", "z");
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

    public static double LevenshteinDistance (string text1, string text2)
    {
        int n = text1.Length;
        int m = text2.Length;
        int[,] dp = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= m; j++)
            {
                if (i == 0)
                {
                    dp[i, j] = j;
                }
                else if (j == 0)
                {
                    dp[i, j] = i;
                }
                else if (text1[i - 1] == text2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = 1 + Math.Min(dp[i, j - 1], Math.Min(dp[i - 1, j], dp[i - 1, j - 1]));
                }
            }
        }

        return dp[n, m];
    }

    public static bool SimilarityRate(string text1, string text2)
    {
        double distance = LevenshteinDistance(text1, text2);
        double maxLength = Math.Max(text1.Length, text2.Length);
        double rate = 1 - distance / maxLength;

        Console.WriteLine(rate);
        return rate >= 0.6;
    }

    public static bool MatchName(string text, string pattern)
    {
        string textCompared = text.ToLower();
        string patternCompared = Translate(pattern);
        if (!isWordLengthSame(text, pattern))
        {
            return false;
        }
        else 
        {
            if (ContainsAllAlphabets(textCompared, patternCompared))
            {
                return SimilarityRate(textCompared, patternCompared);
            }
            else
            {
                return false;
            }
        }
    }

    private static bool ContainsAllAlphabets(string text, string pattern)
    {
        foreach (char letter in pattern)
        {
            // Console.WriteLine(letter);
            if (!text.Contains(letter.ToString()))
            {
                return false;
            }
        }
        return true;
    }

    public static string ResultText(List<string> dataname, string pattern)
    {
        foreach (string name in dataname)
        {
            if (MatchName(name, pattern))
            {
                return name;
            }
        }
        return "Not found";
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
//         // string connectionString = "Server=localhost;Database=stima;Uid=root;Pwd=Radhita7*;";
//         // DatabaseManager dbManager = new DatabaseManager(connectionString);
//         // List<string> data = dbManager.getAllNamaFromBiodata();
//         // // foreach (string berkas in data) {
//         // //     // Console.WriteLine(berkas);
//         // // }

//         // string getname = dbManager.getNameFromSidikJari(@"test\1717073090681.bmp");
//         // string realname = Regex.ResultText(data, getname);
//         // dbManager.printDataFromName(realname);

//         List<string> data = new List<string>();
//         data.Add("Radhita Rahma");
//         data.Add("Diero");
//         data.Add("Bintang Dwi Marthen");

//         string pattern = "d3R";
//         Console.WriteLine("Match Name: " + Regex.ResultText(data, pattern));
//     }
// }