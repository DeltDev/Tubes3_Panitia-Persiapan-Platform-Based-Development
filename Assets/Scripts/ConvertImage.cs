using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

// Nuget add --> ImageSharp
public class ImageConverter
{  
    public bool[][] imageToBinary(Image<Rgba32> image) 
    {   
        // Init 2d array
        bool[][] binaryArr = new bool[image.Height][];
        for (int i = 0 ; i < image.Height; i++)
            binaryArr[i] = new bool[image.Width];
        
        // Isi array
        for (int y = 0; y < image.Height; y++)
            for (int x = 0; x  < image.Width; x++)
                // Kalau pixel[i, j] hitam, isi array[j][i] dengan nilai True
                binaryArr[y][x] = image[x, y].R == 0;   
        return binaryArr;
    }

    public string binaryToASCII(bool[,] binaryArr)
    {
        int row = binaryArr.GetLength(0);
        int col = binaryArr.GetLength(1);

        List<char> asciiList = new List<char>();
        for (int i = 0; i < row; i++)
        {
            List<char> binaryList = new List<char>();
            for (int j = 0; j < col; j++)
            {
                if (binaryList.Count == 8)
                {
                    asciiList.Add((char)Convert.ToInt32(new string(binaryList.ToArray()), 2));
                    binaryList.Clear();
                }
                binaryList.Add(binaryArr[i, j] ? '1' : '0');
            }

            // Pad dengan '0' kalau jumlah binary < 8
            while (binaryList.Count != 8)
                binaryList.Add('0');
            asciiList.Add((char)Convert.ToInt32(new string(binaryList.ToArray()), 2));
        }
        return new string(asciiList.ToArray());
    }
    
    public string convertImage(string path, string filename)
    {
        // Path dimulai dari bin\Debug\net8.0\ ... (kalo di .NET)
        // Asumsikan semua image yang dimasukkan sudah dalam bentuk 'Grayscale'
        try {
            using (Image<Rgba32> image = Image.Load<Rgba32>(path + filename))
            {   
                // Convert gambar menjadi 'Binary Image' (hitam putih)
                image.Mutate(x => x.BinaryThreshold(0.5f));

                bool[][] binaryArr = imageToBinary(image);
                Console.WriteLine(binaryArr.Length);
                Console.WriteLine(binaryArr[0].Length);

                // Pilih area 8 x 15 di tengah gambar
                int areaWidth = 8;
                int areaHeight = 15;
                int startX = (image.Width - areaWidth) / 2;
                int startY = (image.Height - areaHeight) / 2;

                bool[,] binaryArea = new bool[areaHeight, areaWidth];
                int i = 0, j = 0;
                for (int y = startY; y < startY + areaHeight; y++)
                {
                    for (int x = startX; x < startX + areaWidth; x++)
                    {
                        binaryArea[j++, i] = binaryArr[y][x];
                        if (j == areaHeight) {
                            j = 0;
                            i++;
                        }
                    }
                }    
            
                string ascii = binaryToASCII(binaryArea);
                return ascii;
            }
        } catch (Exception e) {
            Console.WriteLine($"{e.Message}");
        }
        return "";
    }
}

// public class Test {
//     public static void Main(string[] args)
//     {
//         ImageConverter ic = new ImageConverter();
//         Console.WriteLine(ic.convertImage("../../../", "100__M_Left_index_finger.bmp"));
//     }
// }