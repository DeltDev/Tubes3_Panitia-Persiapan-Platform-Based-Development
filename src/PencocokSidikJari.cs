public class PencocokSidikJari 
{
    /**
     *  Mengembalikan tingkat kemiripan antara dua sidik jari.
     *
     *  @param   {string} path_sidik_jari_db - path file sidik jari dari database.
     *  @param   {string} filename_sidik_jari_db - nama file sidik jari dari database.
     *  @param   {string} path_sidik_jari_masukan - path file sidik jari masukan pengguna.
     *  @param   {string} filename_sidik_jari_masukan - nama file sidik jari masukan pengguna.
     *  @param   {AlgoritmaPatternMatching} algoritma - algoritma yang digunakan untuk pencocokan sidik jari.
     *  @returns {double} tingkat kemiripan kedua sidik jari.
     */
    public static double Cocok(string path_sidik_jari_db, string filename_sidik_jari_db,
                               string path_sidik_jari_masukan, string filename_sidik_jari_masukan,
                               AlgoritmaPatternMatching algoritma)
    {
        // Konversi citra sidik jari menjadi ASCII.
        List<string> sidik_jari_db = ImageConverter.convertImage(path_sidik_jari_db, filename_sidik_jari_db);
        List<string> sidik_jari_masukan = ImageConverter.convertImage(path_sidik_jari_masukan, filename_sidik_jari_masukan);
        string sidik_jari_db_str = string.Join("", sidik_jari_db);
        string sidik_jari_masukan_str = string.Join("", sidik_jari_masukan);

        // Lempar exception jika panjang string berbeda karena
        // perhitungan tingkat kemiripan menggunakan hamming distance.
        if (sidik_jari_db_str.Length != sidik_jari_masukan_str.Length)
            throw new Exception("Ukuran sidik jari berbeda!");

        // Cari tingkat kemiripan kedua sidik jari, jika "exact match" maka
        // tingkat kemiripan adalah 1, jika bukan, maka akan dihitung menggunakan
        // hamming distance.
        double tingkatKemiripan = 1;
        for (int i = 0; i < sidik_jari_masukan.Count && tingkatKemiripan == 1; i++)
            if (algoritma.Match(sidik_jari_db_str, sidik_jari_masukan[i]).Count == 0)
                tingkatKemiripan = HitungTingkatKemiripan(sidik_jari_db_str, sidik_jari_masukan_str);
        return tingkatKemiripan;
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

// Testing Class
// public class Test {
//     public static void Main(string[] args)
//     {
//         double tingkatKemiripan = PencocokSidikJari.Cocok("./../../../test/", "100__M_Right_middle_finger.bmp",
//                                                           "./../../../test/", "100__M_Left_index_finger.bmp",
//                                                           new KMP());
//         Console.WriteLine($"Tingkat kemiripan: {tingkatKemiripan}");
//     }
// }