/**
 *  Kelas abstrak untuk algoritma pencocokan string.
 */
public abstract class AlgoritmaPatternMatching
{
    /**
     *  Mencari pola dalam sebuah teks.
     *  
     *  @param   {string} text - tempat dilakukannya pencarian pola.
     *  @param   {string} pattern - pola yang ingin dicari dalam text.
     *  @returns {List<int>} array yang berisi indeks awal kemunculan pola dalam text.
     */
    public abstract List<int> Match(string text, string pattern);
}   

/**
 *  Kelas untuk algoritma pencocokan string Knuth-Morris-Pratt (KMP).
 */
public class KMP : AlgoritmaPatternMatching {
    /**
     *  Mencari border function dari pattern.
     *  
     *  @param   {string} pattern - teks yang ingin diproses menjadi border function.
     *  @returns {List<int>} border function dari pattern.
     */
    public List<int> calcBorderFunction(string pattern)
    {
        List<int> borderFunction = new List<int>(pattern.Length);
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
            else
                borderFunction[i++] = ++j;
        }
        return borderFunction;
    }

    /**
     *  Mencari pola dalam sebuah teks menggunakan algoritma Knuth-Morris-Pratt.
     *  
     *  @param   {string} text - tempat dilakukannya pencarian pola.
     *  @param   {string} pattern - pola yang ingin dicari dalam text.
     *  @returns {List<int>} array yang berisi indeks awal kemunculan pola dalam text.
     */
    public override List<int> Match(string text, string pattern)
    {
        // Isi border function.
        List<int> borderFunction = calcBorderFunction(pattern);

        // Cari pattern dalam text.
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
                {
                    matches.Add(i - j);
                    j = 0;
                } 
                else
                {
                    i++;
                    j++;
                }
            }
        }
        return matches;
    }
}

/**
 *  Kelas untuk algoritma pencocokan string Boyer-Moore (BM).
 */
public class BM : AlgoritmaPatternMatching 
{ 
    /**
     *  Mencari last occurence function dari pattern.
     *  
     *  @param   {string} pattern - teks yang ingin diproses menjadi last occurence function.
     *  @returns {Dictionary<char, int>} last occurence function dari pattern.
     */
    public Dictionary<char, int> calcLastOccurenceFunction(string pattern) 
    {
        Dictionary<char, int> lof = new Dictionary<char, int>(); 
        for (int i = 0; i < pattern.Length; i++)
            lof[pattern[i]] = i;
        return lof;
    }

    /**
     *  Mencari pola dalam sebuah teks menggunakan algoritma Boyer-Moore.
     *  
     *  @param   {string} text - tempat dilakukannya pencarian pola.
     *  @param   {string} pattern - pola yang ingin dicari dalam text.
     *  @returns {List<int>} array yang berisi indeks awal kemunculan pola dalam text.
     */
    public override List<int> Match(string text, string pattern) 
    {
        // Isi last occurence function.
        Dictionary<char, int> charLastIdx = calcLastOccurenceFunction(pattern); 

        // Cari pattern dalam text.
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