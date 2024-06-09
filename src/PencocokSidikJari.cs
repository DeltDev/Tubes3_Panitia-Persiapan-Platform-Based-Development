using System.IO;

/**
 *  Kelas untuk melakukan pencocokan sidik jari.
 */
public class PencocokSidikJari 
{
    private static List<string> sidik_jari_masukan = new List<string>();
    private static List<string> list_sidik_jari_db = new List<string>();

    /**
     *  Melakukan pencocokan antara sidik jari masukan dengan salah satu sidik jari dalam database.
     *
     *  @param   {string} path_sidik_jari_db - path menuju ke sidik jari dalam database
     *  @param   {AlgoritmaPatternMatching} algoritma - algoritma yang digunakan untuk pencocokan sidik jari.
     *  @returns {double} tingkat kemiripan kedua sidik jari.
     */
    public static double Cocok(string path_sidik_jari_db, AlgoritmaPatternMatching algoritma)
    {
        // Konversi citra sidik jari menjadi ASCII.
        List<string> sidik_jari_db = ImageConverter.convertImage(path_sidik_jari_db);
        string sidik_jari_db_str = string.Join("", sidik_jari_db);
        string sidik_jari_masukan_str = string.Join("", sidik_jari_masukan);
        
        // Lempar exception jika panjang string berbeda karena perhitungan tingkat kemiripan 
        // menggunakan hamming distance.
        if (sidik_jari_db_str.Length != sidik_jari_masukan_str.Length)
            throw new Exception("Ukuran sidik jari berbeda!");

        // Cari tingkat kemiripan kedua sidik jari, jika "exact match" maka tingkat kemiripan
        // adalah 1, jika bukan exact match, maka akan dihitung menggunakan hamming distance.
        double tingkatKemiripan = 1;
        for (int i = 0; i < sidik_jari_masukan.Count && tingkatKemiripan == 1; i++)
            if (!algoritma.Match(sidik_jari_db_str, sidik_jari_masukan[i]))
                tingkatKemiripan = HitungTingkatKemiripan(sidik_jari_db_str, sidik_jari_masukan_str);

        return tingkatKemiripan;
    }

    /**
     *  Melakukan pencocokan antara sidik jari masukan dengan seluruh sidik jari dari database.
     *
     *  @param   {string} input - path menuju ke file sidik jari masukan.
     *  @param   {List<string>} db - list yang berisi path file sidik jari dari database. 
     *  @param   {AlgoritmaPatternMatching} algoritma - algoritma yang digunakan untuk pencocokan sidik jari.
     *  @returns {KeyValuePair<string, double>} pasangan sidik jari dengan tingkat kemiripan dengan nilai
     *                                          tingkat kemiripan yang paling tinggi.
     */
    public static KeyValuePair<string, double> MulaiPencocokan(string input, List<string> db, AlgoritmaPatternMatching algoritma)
    {
        // Inisialisasi
        sidik_jari_masukan = ImageConverter.convertImage(input);

        for (int i = 0; i < db.Count; i++)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string tempPath = System.IO.Path.Combine(directory, "./../../../../test", db[i]);   
                                                                                                    
            tempPath = System.IO.Path.GetFullPath(tempPath);

            db[i] = tempPath;
        }
        list_sidik_jari_db = new List<string>(db);

        // Bandingin sidik jari masukan ke semua sidik jari dalam database
        Dictionary<string, double> hasil_akhir = new Dictionary<string, double>();
        foreach (string path_sidik_jari_db in list_sidik_jari_db) 
            hasil_akhir.Add(path_sidik_jari_db, Cocok(path_sidik_jari_db, algoritma));

        // Cari tingkat kemiripan tertinggi
        var maxKvp = hasil_akhir.FirstOrDefault();
        foreach (var kvp in hasil_akhir)
        {
            double tingkatKemiripan = kvp.Value;
            if (tingkatKemiripan > maxKvp.Value)
                maxKvp = kvp;
        }

        return maxKvp;
    }

    // --------------- Tingkat Kemiripan ---------------

    /**
     *  Menghitung nilai hamming distance antara dua string.
     *  Berasumsi bahwa kedua string memiliki panjang sama.
     *  
     *  @param   {string} t1 - string pertama.
     *  @param   {string} t2 - string kedua.
     *  @returns {int} nilai hamming distance t1 dengan t2.
     */
    public static int HammingDistance(string t1, string t2)
    {
        if (t1 == t2)
            return 0;

        int cnt = 0;
        for (int i = 0; i < t1.Length; i++)
            if (t1[i] != t2[i])
                cnt++;  
        return cnt;
    }

    /**
     *  Menghitung tingkat kemiripan antara dua string.
     *  
     *  @param   {string} t1 - string pertama.
     *  @param   {string} t2 - string kedua.
     *  @returns {int} tingkat kemiripan t1 dengan t2.
     */
    public static double HitungTingkatKemiripan(string t1, string t2) { return 1 - ((double) HammingDistance(t1, t2) / t2.Length); }
}