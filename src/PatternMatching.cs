using System.Drawing;

public abstract class AlgoritmaPatternMatching
{
    public int HammingDistance(string text, string pattern)
    {
        // Mengembalikan nilai Hamming Distance dari pattern ke text,
        // Asumsi panjang text s.d. panjang pattern.

        int cnt = 0;
        for (int i = 0; i < text.Length; i++)
            if (text[i] != pattern[i])
                cnt++;  
        return cnt;
    }

    // public int LevenshteinDistance(string text, string pattern)
    // {
    //     // Kalo kedua string adalah sama, kembalikan 0.
    //     if (text.Equals(pattern))
    //         return 0;
        
    //     // Kalo ada string yang kosong, kembalikan panjang string yang lain.
    //     if (text.Length == 0 || pattern.Length == 0)
    //         return text.Length == 0 ? pattern.Length : text.Length;

    //     if (text[^1] == pattern[^1])
    //         return LevenshteinDistance(text[..^1], pattern[..^1]);

    //     int insert = LevenshteinDistance(text, pattern[..^1]);
    //     int remove = LevenshteinDistance(text[..^1], pattern);    
    //     int replace = LevenshteinDistance(text[..^1], pattern[..^1]);

    //     return 1 + Math.Min(Math.Min(insert, remove), replace);   
    // }

    // public int LCS(string text, string pattern)
    // {
    //     if (text.Equals(pattern))
    //         return text.Length;
        
    //     if (text.Length == 0 || pattern.Length == 0)
    //         return 0;
        
    //     if (text[text.Length - 1] == pattern[pattern.Length - 1])
    //         return 1 + LCS(text[..^1], pattern[..^1]);
        
    //     return Math.Max(LCS(text[..^1], pattern), LCS(text, pattern[..^1]));
    // }

    public double HitungTingkatKemiripan(string text, string pattern) { return 1 - ((double)HammingDistance(text, pattern) / pattern.Length); }

    public abstract List<int> Match(string text, string pattern);

    public void Start(List<string> sidik_jari_db, List<string> sidik_jari_input) 
    {   
        string sidik_jari_db_str = string.Join("", sidik_jari_db);
        string sidik_jari_input_str = string.Join("", sidik_jari_input);
        
        if (sidik_jari_db_str.Length != sidik_jari_input_str.Length)
            throw new BedaLengthException("Besar ukuran sidik jari berbeda!");

        double tingkatKemiripan = 1;
        for (int i = 0; i < sidik_jari_input.Count; i++)
        {
            string currentStr = sidik_jari_input[i];
            List<int> match = Match(sidik_jari_db_str, currentStr);

            // Asumsikan ukuran ASCII fingerprint sama semua
            if (match.Count == 0)
            {
                // bukan total match
                tingkatKemiripan = HitungTingkatKemiripan(sidik_jari_db_str, sidik_jari_input_str);
                break;
            }
        }
        Console.WriteLine(tingkatKemiripan);
    }
}   

public class KMP : AlgoritmaPatternMatching {
    public override List<int> Match(string text, string pattern)
    {
        // Mengembalikan array yang berisi index 
        // awal kemunculan pattern dalam text
        List<int> matches = new List<int>();

        // Isi border function
        List<int> borderFunction = new List<int>();
        for (int k = 0; k < pattern.Length; k++)
            borderFunction.Add(0);

        int i1 = 1, j1 = 0;
        while (i1 < pattern.Length)
        {
            if (pattern[j1] != pattern[i1])
            {
                if (j1 > 0)
                    j1 = borderFunction[j1 - 1];
                else
                    i1++;
            }
            else if (pattern[i1] == pattern[j1])
            {
                i1++;
                j1++;
                borderFunction[i1 - 1] = j1;
            }
        }
    
        int i = 0, j = 0;
        while (i < text.Length)
        {
            if (pattern[j] != text[i])
            {
                if (j > 0)
                {
                    j = borderFunction[j - 1];
                }
                else
                    i++;
            }
            else
            {
                if (j == pattern.Length - 1)
                {
                    matches.Add(i - j);
                    j = 0;
                }
                i++;
                j++;
            }
        }
        return matches;
    }
}

public class BM : AlgoritmaPatternMatching 
{ 
    public override List<int> Match(string text, string pattern) 
    {
        // Mengembalikan array yang berisi index 
        // awal kemunculan pattern dalam text
        List<int> matches = new List<int>();

        Dictionary<char, int> charLastIdx = new Dictionary<char, int>(); 
        for (int i = 0; i < pattern.Length; i++)
            charLastIdx[pattern[i]] = i;

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

public class BedaLengthException : Exception
{
    public BedaLengthException() : base() {}
    public BedaLengthException(string str) : base(str) {}
    public BedaLengthException(string str, Exception innerException) : base(str, innerException) {}
}   

// public class Test {
//     public static void Main(string[] args)
//     {
//         BM kmpTest = new("ABCABC");

//         List<string> lst = ["ABCDEG", "ABCABC", "ABCDEF"];

//         kmpTest.Start(lst);
//         // for (int c = 0; c <= 9; c++)
//         // {
//         //     char d = (char) (c + '0');
//         //     Console.WriteLine(d);
//         // }
//     }
// }

// TODO: Throw exception jika panjang text != panjang pattern
//       Testing lebih lanjut
//       Rapihin, tambahin komen