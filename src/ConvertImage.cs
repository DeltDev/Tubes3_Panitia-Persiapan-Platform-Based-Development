using System.ComponentModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// Nuget add --> ImageSharp
public class ImageConverter 
{  
    public bool[,] imageToBinary(Image<Rgba32> image) 
    {   
        // Mengembalikan representasi gambar dalam bentuk array biner

        // Init 2d array
        bool[,] binaryArr = new bool[image.Height, image.Width];
        
        // Isi array
        for (int y = 0; y < image.Height; y++)
            for (int x = 0; x  < image.Width; x++)
                // Kalau pixel[i, j] hitam, isi array[j][i] dengan nilai True
                binaryArr[y, x] = image[x, y].R == 0;   
        return binaryArr;
    }

    public List<string> binaryToASCII(bool[,] binaryArr)
    {
        // Mengembalikan array yang berisi string 
        // hasil konversi 32 pixel --> 4 karakter ascii

        int row = binaryArr.GetLength(0);
        int col = binaryArr.GetLength(1);
        
        List<string> ret = new List<string>();
        List<char> asciiList = new List<char>();
        for (int i = 0; i < row; i++)
        {
            List<char> binaryList = new List<char>();
            for (int j = 0; j < col; j++)
            {
                if (binaryList.Count == 8)
                {
                    // 32 pixel
                    if (asciiList.Count == 4)
                    {
                        ret.Add(new string(asciiList.ToArray()));
                        asciiList.Clear();
                    }
                    asciiList.Add((char)Convert.ToInt32(new string(binaryList.ToArray()), 2));
                    binaryList.Clear();
                }
                binaryList.Add(binaryArr[i, j] ? '1' : '0');
            }

            // Pad dengan '0' kalau jumlah binary < 8
            while (binaryList.Count != 8)
                binaryList.Add('0');
            asciiList.Add((char)Convert.ToInt32(new string(binaryList.ToArray()), 2));

            if (asciiList.Count >= 4)
            {
                ret.Add(new string(asciiList.ToArray()));
                asciiList.RemoveRange(0, 4);
            }

            if (asciiList.Count > 0)
            {
                // Pad dengan '00000000' kalau jumlah karakter ascii < 4
                while (asciiList.Count < 4)
                    asciiList.Add((char) Convert.ToInt32(new string("00000000"), 2));
                ret.Add(new string(asciiList.ToArray()));
            }
        }
        return ret;
    }
    
    public List<string> convertImage(string path, string filename)
    {
        // Path dimulai dari bin\Debug\net8.0\ ...
        try {
            using (Image<Rgba32> image = Image.Load<Rgba32>(path + filename))
            {   
                // Convert gambar ke dalam bentuk 'Grayscale'
                image.Mutate(x => x.Grayscale());

                // Convert gambar menjadi 'Binary Image' (hitam putih)
                image.Mutate(x => x.BinaryThreshold(0.5f));

                bool[,] binaryArr = imageToBinary(image);

                // Kembalikan list berisi potongan 4 ascii
                List<string> ascii = binaryToASCII(binaryArr);
                return ascii;
            }
        } catch (Exception e) {
            Console.WriteLine($"{e.Message}");
        }
        return [];
    }
}

/*
public class Test {
    public static void Main(string[] args)
    {
        ImageConverter ic = new ImageConverter();
        List<string> fg1 = ic.convertImage("../../../", "100__M_Left_index_finger.bmp");
        List<string> fg2 = ic.convertImage("../../../", "100__M_Left_index_finger.bmp");
        List<string> fg3 = ic.convertImage("../../../", "102__M_Right_little_finger.bmp");
        List<string> fg4 = ic.convertImage("../../../", "100__M_Right_middle_finger.bmp");
        AlgoritmaPatternMatching kmpTest = new KMP();

        kmpTest.Start(fg1, fg2);
        kmpTest.Start(fg1, fg3);
        kmpTest.Start(fg1, fg4);

        // Console.WriteLine(kmpTest.LevenshteinDistance("kitten", "smitten"));
        // Console.WriteLine("TEST");
    }
}
*/