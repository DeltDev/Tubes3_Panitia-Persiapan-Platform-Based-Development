﻿using Microsoft.Win32;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
namespace src
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string chosenMethod;
        private string chosenIMG;
        private TextBlock chosenMethodLBL;
        private TextBlock EstTimeLBL;
        private TextBlock MatchPercentLBL;
        public MainWindow()
        {
            InitializeComponent();
            chosenMethodLBL = (TextBlock)FindName("ChosenMethodLabel");
            chosenMethodLBL.Text = "";
            EstTimeLBL = (TextBlock)FindName("EstimatedTimeLabel");
            EstTimeLBL.Text = "";
            MatchPercentLBL = (TextBlock)FindName("MatchPercentLabel");
            MatchPercentLBL.Text = "";
        }

        private void ChooseIMGButton_Click(object sender, RoutedEventArgs e)
        {
            //ganti logic buat pilih citra di sini
            OpenFileDialog imgSelect = new OpenFileDialog();
            imgSelect.Filter = ".bmp Image Files|*.bmp";
            bool? result = imgSelect.ShowDialog();

            if (result == true)
            {
                string imgSelectedName = imgSelect.FileName;
                string imageName = System.IO.Path.GetFileName(imgSelectedName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imgSelectedName);
                bitmap.EndInit();
                InputImage.Source = bitmap;
                System.IO.File.Copy(imgSelectedName, "../../../../temp/"+imageName,true);
            }
        }

        private void BMRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            //ini cuma buat dapetin nilai metode yang dipilih
            RadioButton BMButton = (RadioButton)FindName("BMRadioBtn");
            chosenMethod = BMButton.Content.ToString();
        }

        private void KMPRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            //ini cuma buat dapetin nilai metode yang dipilih
            RadioButton KMPButton = (RadioButton)FindName("KMPRadioBtn");
            chosenMethod = KMPButton.Content.ToString();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //tombol buat mulai search
            if (chosenMethod == "" || chosenMethod == null)
            {
                MessageBox.Show("Anda belum memilih metode pencarian!\nSilakan pilih metode pencarian.", "Belum pilih metode pencarian", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            chosenMethodLBL.Text = chosenMethod;
        }
    }
}