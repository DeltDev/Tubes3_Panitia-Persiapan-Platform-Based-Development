# Pemanfaatan _Pattern Matching_ dalam Membangun Sistem Deteksi Individu Berbasis Biometrik Melalui Citra Sidik Jari
Program untuk menemukan biodata dengan sidik jari yang paling mirip dalam basis data dengan sidik jari yang dimasukkan oleh pengguna.


## Table of Contents
* [General Info](#general-information)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Usage](#usage)
* [Contact](#contact)


## General Information
Program menggunakan algoritma Knuth-Morris-Pratt (KMP) atau algoritma Boyer-Moore (BM) untuk melakukan pencocokan sidik jari. Setelah ditemukan sidik jari dalam basis data dengan nilai tingkat kemiripan tertinggi, akan dilakukan pencocokan menggunakan ekspresi reguler (Regex) untuk mencocokkan nama yang terdapat pada tabel sidik jari dengan nama yang terdapat pada tabel biodata.  
- Algoritma KMP  
Algoritma ini akan mencari kemunculan pola dalam sebuah teks dengan arah _left-to-right_ dan menggunakan sebuah _array_ border function yang berfungsi untuk menentukan besar lompatan yang dapat diambil ketika terjadi _mismatch_.  
- Algoritma BM  
Algoritma ini akan mencari kemunculan pola dalam sebuah teks dengan arah _right-to-left_ dan menggunakan sebuah _array_ last occurence function yang berisi indeks kemunculan terakhir setiap karakter dalam pola untuk menentukan besar lompatan yang diambil ketika terjadi _mismatch_.  
- Regex  
Program memiliki kelas Regex yang bertujuan untuk mengonversi data korup yang mungkin terjadi di atribut nama pada tabel sidik_jari menjadi karakter alfabet seluruhnya. Kemudian, kelas Regex akan mencari nilai atribut nama pada tabel biodata yang memiliki nilai kemiripan terbesar dengan hasil konversi data korup nama pada tabel biodata menggunakan Levenhstein Distance. 


## Screenshots
![Example screenshot](./src/img/image.png)


## Setup
Berikut merupakan hal-hal yang perlu dilakukan untuk menjalankan program:
- _Clone_ repo github https://github.com/DeltDev/Tubes3_Panitia-Persiapan-Platform-Based-Development.
- Ganti atribut-atribut dalam file Config.cs agar sesuai dengan basis data yang ingin digunakan.
- Unduh _framework_ .NET (https://dotnet.microsoft.com/en-us/download/dotnet-framework)
- Unduh package MySQL.Data untuk menghubungkan program dengan database MySQL
```bash
dotnet add package MySql.Data --version 8.4.0
```
- Unduh package Dapper untuk menghubungkan program dengan database
```bash
dotnet add package Dapper --version 2.1.35
```
- Buka folder _src_ dalam terminal dan masukkan perintah 
```bash
dotnet run
```

## Contact
Dibuat oleh:
| NIM      | Nama                    | Kelas                                                                                                                                                                                                               |
|----------|-------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 13522036 | Akbar Al Fattah            | K-02                                                              |
| 13522056 | Diero Arga Purnama         | K-02                                                              |
| 13522060 | Andhita Naura Hariyanto    | K-02                                                              |
