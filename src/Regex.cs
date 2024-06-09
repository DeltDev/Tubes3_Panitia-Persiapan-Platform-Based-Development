using System;
using System.Text.RegularExpressions;

public class Regex
{
    // Mengonversi string masukan dengan mengubah nilai angka menjadi nilai huruf terkait serta membuat seluruh huruf menjadi huruf kecil
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

        result = result.ToLower();

        return result;
    }

    // Mencari nilai Levenshtein Distance dari dua string
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

    // Menentukan tingkat kemiripan antara dua string berdasarkan nilai yang didapat dari perhitungan Levendstein Distance
    public static bool SimilarityRate(string text1, string text2)
    {
        double distance = LevenshteinDistance(text1, text2);
        double maxLength = Math.Max(text1.Length, text2.Length);
        double rate = 1 - distance / maxLength;

        Console.WriteLine(rate);
        return rate >= 0.6;
    }

    // Mencocokkan nama dengan pola yang diberikan
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

    // Menentukan apakah teks mengandung seluruh huruf yang ada pada pattern
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

    // Mengimplementasikan fungsi MatchName pada List of string yang berisikan seluruh nama dari basis data untuk mencari nama yang cocok dengan pattern yang diberikan
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

    // Menentukan apakah panjang kata dari text dan pattern sama
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