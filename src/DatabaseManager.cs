using System;
using Dapper;
using MySql.Data.MySqlClient;

// DEPENDENCY
// dotnet add package Dapper --version 2.1.35
// dotnet add package MySql.Data --version 8.4.0

public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int getBiodataCount()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * FROM biodata");
            return users.Count();
        }
    }

    public int getSidikJariCount()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * FROM sidik_jari");
            return users.Count();
        }
    }

    public string getNameFromSidikJari(string berkas_citra)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from sidik_jari WHERE berkas_citra = @berkas_citra", new { berkas_citra });
            if (users.Count() == 0)
            {
                return "No data found with the inserted fingerprint";
            }
            return users.First().nama;
        }
    
    }

    public string getNIKFromName(string Name)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE nama = @nama", new { nama = Name });
            if (users.Count() == 0)
            {
                return "No data found with the inserted name";
            }
            return users.First().NIK;
        }
    }

    public string getNameFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().nama;
        }
    }

    public string getTempatLahirFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().tempat_lahir;
        }
    }

    public string getTanggalLahirFromNIK(string NIK)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var result = connection.QueryFirstOrDefault<DateTime?>(
                "SELECT tanggal_lahir FROM biodata WHERE NIK = @NIK", 
                new { NIK }
            );

            if (result == null)
            {
                return "No data found with the inserted NIK";
            }

            // ubah date type tanggal lahir ke string
            return result.Value.ToString("yyyy-MM-dd");
        }
    }


    public string getJenisKelaminFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().jenis_kelamin;
        }
    }

    public string getGolonganDarahFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().golongan_darah;
        }
    }

    public string getAlamatFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().alamat;
        }
    }

    public string getAgamaFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().agama;
        }
    }

    public string getStatusPerkawinanFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().status_perkawinan;
        }
    }

    public string getPekerjaanFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().pekerjaan;
        }
    }

    public string getKewarganegaraanFromNIK (string NIK) {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * from biodata WHERE NIK = @NIK", new { NIK });
            if (users.Count() == 0)
            {
                return "No data found with the inserted NIK";
            }
            return users.First().kewarganegaraan;
        }
    }

    public void printDataFromName(string Name)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * FROM biodata WHERE nama = @nama", new { nama = Name });
            foreach (var user in users)
            {
                Console.WriteLine(user.NIK);
                Console.WriteLine(user.nama);
                Console.WriteLine(user.tempat_lahir);
                Console.WriteLine(user.tanggal_lahir);
                Console.WriteLine(user.jenis_kelamin);
                Console.WriteLine(user.golongan_darah);
                Console.WriteLine(user.alamat);
                Console.WriteLine(user.agama);
                Console.WriteLine(user.status_perkawinan);
                Console.WriteLine(user.pekerjaan);
                Console.WriteLine(user.kewarganegaraan);
            }
        }
    
    }

    public void printDataFromNIK(string NIK)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            var users = connection.Query("SELECT * FROM biodata WHERE NIK = @NIK", new { NIK });
            foreach (var user in users)
            {
                Console.WriteLine(user.NIK);
                Console.WriteLine(user.nama);
                Console.WriteLine(user.tempat_lahir);
                Console.WriteLine(user.tanggal_lahir);
                Console.WriteLine(user.jenis_kelamin);
                Console.WriteLine(user.golongan_darah);
                Console.WriteLine(user.alamat);
                Console.WriteLine(user.agama);
                Console.WriteLine(user.status_perkawinan);
                Console.WriteLine(user.pekerjaan);
                Console.WriteLine(user.kewarganegaraan);
            }
        }
    
    }

    public void insertDataToSidikJari(){
        string insertDataQuery = @"
            INSERT INTO sidik_jari (berkas_citra, nama)
            VALUES (@FilePath, 'DUMMY_NAME')";
            
        string filePath = @"CreateDatabase\test\100__M_Left_index_finger.bmp";

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(insertDataQuery, new { FilePath = filePath });
        }

    }

    public void insertDataToBiodata(){
        string insertDataQuery = @"
            INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan)
            VALUES ('0123456701234567', 'DUMMY_NAME', 'DUMMY_PLACE_OF_BIRTH', '2000-01-01', 'Laki-Laki', 'O', 'DUMMY_ADDRESS', 'DUMMY_RELIGION', 'Belum Menikah', 'DUMMY', 'DUMMY')";
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            connection.Execute(insertDataQuery);
        }

    }
}

class Program
{
       static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=stima;Uid=;Pwd=;";
        DatabaseManager dbManager = new DatabaseManager(connectionString);
        string name = dbManager.getNameFromSidikJari(@"CreateDatabase\test\100__M_Left_index_finger.bmp");
        // Console.WriteLine(name);
        // dbManager.printDataFromName(name);
        string NIK = dbManager.getNIKFromName(name);
        string pekerjaan = dbManager.getTanggalLahirFromNIK(NIK);
        Console.WriteLine(pekerjaan);
        // dbManager.insertData();
    }
}