using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public abstract class AlgoritmaPatternMatching
{
    protected string pattern;                       // Fingerprint input.
    protected Dictionary<string, double> texts;     // Fingerprint dari database & tingkat kemiripannya.

    public AlgoritmaPatternMatching(string pattern)
    {
        this.pattern = pattern;
        texts = new Dictionary<string, double>();
    }

    public string getPattern() { return pattern; }

    public Dictionary<string, double> getTexts() { return texts; } 
    
    public void AddText(string text, double val) { texts[text] = val; }

    public int HammingDistance(string text)
    {
        // Mengembalikan nilai Hamming Distance dari pattern ke text,
        // Asumsi panjang text s.d. panjang pattern.

        int cnt = 0;
        for (int i = 0; i < text.Length; i++)
            if (text[i] != pattern[i])
                cnt++;  
        return cnt;
    }

    public double HitungTingkatKemiripan(string text) { return 1 - ((double)HammingDistance(text) / pattern.Length); }

    public abstract List<int> Match(string text);

    public void Start(List<string> listText)
    {   
        // Menerima array ASCII fingerprint dari database
        // Menampilkan tingkat kemiripan masing-masing fingerprint
        Console.WriteLine("Pattern: " + pattern);
        foreach (string text in listText)
        {
            if (text.Length != pattern.Length)
                throw new BedaLenException("Text dan pattern harus memiliki panjang yang sama!");

            List<int> matches = Match(text);
            double tingkatKemiripan = 1;
            if (matches.Count <= 0)
                tingkatKemiripan = HitungTingkatKemiripan(text);
            AddText(text, tingkatKemiripan);

            Console.WriteLine(text + ": " + texts[text]);
        }
    }
}   

public class KMP : AlgoritmaPatternMatching {
    private List<int> borderFunction;
    public KMP(string pattern) : base(pattern) 
    {
        // Isi border function
        borderFunction = new List<int>();
        for (int k = 0; k < pattern.Length; k++)
            borderFunction.Add(0);

        int i = 1, j = 0;
        while (i < pattern.Length)
        {
            if (pattern[j] != pattern[i])
            {
                if (j > 0)
                    j = borderFunction[j - 1];
                else
                    i++;
            }
            else if (pattern[i] == pattern[j])
            {
                i++;
                j++;
                borderFunction[i - 1] = j;
            }
        }
    }
    
    public override List<int> Match(string text)
    {
        // Mengembalikan array yang berisi index 
        // awal kemunculan pattern dalam text
        List<int> matches = new List<int>();
    
        int i = 0, j = 0;
        while (i < text.Length)
        {
            if (pattern[j] != text[i])
            {
                if (j > 0)
                    j = borderFunction[j - 1];
                else
                    i++;
            }
            else
            {
                if (j == pattern.Length - 1)
                    matches.Add(i - j);
                i++;
                j++;
            }
        }
        return matches;
    }
}

public class BM : AlgoritmaPatternMatching 
{
    private Dictionary<char, int> charLastIdx;
    
    public BM(string pattern) : base(pattern) 
    { 
        charLastIdx = new Dictionary<char, int>(); 
        for (int i = 0; i < pattern.Length; i++)
            charLastIdx[pattern[i]] = i;
    }

    public override List<int> Match(string text) 
    {
        // Mengembalikan array yang berisi index 
        // awal kemunculan pattern dalam text
        List<int> matches = new List<int>();

        int shift = 0;
        while (shift + pattern.Length <= text.Length)
        {
            int idx = pattern.Length - 1;
            while (idx >= 0 && (pattern[idx] == text[shift + idx]))
                idx--;

            int val;
            if (idx == -1)
            {
                matches.Add(shift);
                
                if (shift + pattern.Length < text.Length)
                {
                    if (charLastIdx.TryGetValue(text[shift + pattern.Length], out val))
                        shift += pattern.Length - val;
                    else 
                        shift += pattern.Length + 1;
                }
                else
                    break;
            }
            else
            {
                if (charLastIdx.TryGetValue(text[shift + idx], out val))
                    shift += idx - val > 1 ? idx - val : 1;
                else 
                    shift += idx + 1;
            }
        }
        return matches;
    }
}

public class BedaLenException : Exception
{
    public BedaLenException() : base() {}
    public BedaLenException(string str) : base(str) {}
    public BedaLenException(string str, Exception innerException) : base(str, innerException) {}
}   

// public class Test {
//     public static void Main(string[] args)
//     {
//         KMP kmpTest = new("ABCABC");

//         List<string> lst = ["ABCADD", "ABCABC"];

//         kmpTest.Start(lst);
//     }
// }

