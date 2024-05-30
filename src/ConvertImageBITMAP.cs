using System.Drawing;
using System.Text;

public class ConvertImage {
    /**
     *  Konversi gambar menjadi array ASCII.
     *  
     *  @param   {string} path - path ke gambar yang ingin dikonversi.
     *  @param   {string} filename - nama file gambar yang ingin dikonversi.
     *  @returns {List<string>} Hasil konversi gambar menjadi array ASCII.
     */
    public static List<string> convertImage(string path, string filename) 
    {
        // Load gambar.
        Bitmap image = new Bitmap(path + filename);

        // Konversi gambar menjadi gambar hitam putih.
        Bitmap bnw = ConvertImageToBnW(image);

        // Konversi gambar hitam putih menjadi array biner dua dimensi.
        bool[,] binaryArr = ConvertBnWToBinary(bnw);

        // Konversi array biner menjadi array ASCII.
        List<string> asciiArr = ConvertBinaryToASCII(binaryArr);

        return asciiArr;
    }

    /**
     *  Konversi gambar menjadi gambar hitam putih.
     *  
     *  @param   {Bitmap} image - Gambar yang ingin dikonversi.
     *  @returns {Bitmap} Hasil konversi image menjadi gambar hitam putih.
     */
    public static Bitmap ConvertImageToBnW(Bitmap image) 
    {
        Bitmap bnw = new Bitmap(image.Width, image.Height);
        for (int y = 0; y < image.Height; y++)
            for (int x = 0; x < image.Width; x++) 
            {
                Color colorAwal = image.GetPixel(x, y);

                // Konversi ke grayscale menggunakan luminosity method.
                int grayScale = (int) (colorAwal.R * 0.21 + colorAwal.G * 0.72 + colorAwal.B * 0.05); 

                // Konversi ke hitam putih dg threshold 128 (0 -> hitam, 255 -> putih).
                bnw.SetPixel(x, y, grayScale < 128 ? Color.Black : Color.White);
            }
        return bnw;
    }

    /**
     *  Konversi gambar hitam putih menjadi array biner dua dimensi.
     *  
     *  @param   {Bitmap} bnw - Gambar hitam putih yang ingin dikonversi.
     *  @returns {bool[,]} Hasil konversi gambar menjadi array dua dimensi.
     */
    public static bool[,] ConvertBnWToBinary(Bitmap bnw) 
    {
        bool[,] binaryArr = new bool[bnw.Height, bnw.Width];
        for (int y = 0; y < bnw.Height; y++)
            for (int x = 0; x < bnw.Width; x++)
                // Pixel hitam direpresentasikan dengan True pada array.
                binaryArr[y, x] = bnw.GetPixel(x, y).R == 0;
        return binaryArr;
    }

    /**
     *  Konversi array biner dua dimensi menjadi list ASCII. Setiap elemen
     *  pada list ASCII merepresentasikan 32 elemen pada array biner. 
     *
     *  @param   {bool[,]} binaryArr - array biner dua dimensi yang ingin dikonversi.
     *  @returns {List<string>} Hasil konversi array biner menjadi array ASCII.
     */
     public static List<string> ConvertBinaryToASCII(bool[,] binaryArr)
     {
        // Konversi list 32 karakter menjadi string ASCII.
        string convert(List<char> binaryGroup) {
            StringBuilder sb = new StringBuilder();

            sb.Append((char)Convert.ToInt32(new string(binaryGroup.GetRange(0, 8).ToArray()), 2));
            sb.Append((char)Convert.ToInt32(new string(binaryGroup.GetRange(8, 8).ToArray()), 2));
            sb.Append((char)Convert.ToInt32(new string(binaryGroup.GetRange(16, 8).ToArray()), 2));
            sb.Append((char)Convert.ToInt32(new string(binaryGroup.GetRange(24, 8).ToArray()), 2));

            return sb.ToString();
        }

        List<string> asciiArr = [];

        for (int y = 0; y < binaryArr.GetLength(0); y++)
        {
            List<char> binaryGroup = [];
            for (int x = 0; x < binaryArr.GetLength(1); x++)
            {
                if (binaryGroup.Count == 32)
                {
                    asciiArr.Add(convert(binaryGroup));
                    binaryGroup.Clear();
                }
                binaryGroup.Add(binaryArr[y, x] ? '1' : '0');
            }

            if (binaryGroup.Count > 0)
            {
                while (binaryGroup.Count != 32)
                    binaryGroup.Add('0');
                asciiArr.Add(convert(binaryGroup));
                binaryGroup.Clear();
            }
        }
        return asciiArr;
     }
}

// public class Test {
//     public static void Main(string[] args)
//     {
//         List<string> fg1 = ConvertImage.convertImage("./../../ImageDataset/", "100__M_Left_index_finger.bmp");
//         List<string> fg2 = ConvertImage.convertImage("./../../ImageDataset/", "100__M_Left_index_finger.bmp");
//         List<string> fg3 = ConvertImage.convertImage("./../../ImageDataset/", "102__M_Right_little_finger.bmp");
//         List<string> fg4 = ConvertImage.convertImage("./../../ImageDataset/", "100__M_Right_middle_finger.bmp");

//         AlgoritmaPatternMatching kmpTest = new KMP();

//         Console.WriteLine("100__M_Left_index_finger.bmp: 100__M_Left_index_finger.bmp");
//         kmpTest.Start(fg1, fg2);
//         Console.WriteLine("100__M_Left_index_finger.bmp: 102__M_Right_little_finger.bmp");
//         kmpTest.Start(fg1, fg3);
//         Console.WriteLine("100__M_Left_index_finger.bmp: 100__M_Right_middle_finger.bmp");
//         kmpTest.Start(fg1, fg4);
//         Console.WriteLine("102__M_Right_little_finger.bmp: 100__M_Right_middle_finger.bmp");
//         kmpTest.Start(fg3, fg4);
//         Console.WriteLine("102__M_Right_little_finger.bmp: 102__M_Right_little_finger.bmp");
//         kmpTest.Start(fg3, fg3);

//     }
// }