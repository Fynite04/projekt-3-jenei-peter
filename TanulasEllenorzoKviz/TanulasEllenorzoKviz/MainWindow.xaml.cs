using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TanulasEllenorzoKviz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> tantargyakNevLista = new List<string>();
        List<string> temakorokLista = new List<string>();
        List<KvizFeladat> osszesFeladatLista = new List<KvizFeladat>();
        List<Button> valaszGombok = new List<Button>();
        List<Button> lapGombok = new List<Button>();
        KvizKezelo kviz;

        public MainWindow()
        {
            InitializeComponent();
            FajlokBeolvasasa();

            kerdes_BG.Visibility = Visibility.Hidden;
            aValasz_Btn.Visibility = Visibility.Hidden;
            bValasz_Btn.Visibility = Visibility.Hidden;
            cValasz_Btn.Visibility = Visibility.Hidden;
            dValasz_Btn.Visibility = Visibility.Hidden;
            index_Lbl.Visibility = Visibility.Hidden;
            prev_Btn.Visibility = Visibility.Hidden;
            next_Btn.Visibility = Visibility.Hidden;
            kiertekeles_Btn.Visibility = Visibility.Hidden;

            tantargy_CBx.ItemsSource = tantargyakNevLista;

            valaszGombok.Add(aValasz_Btn);
            valaszGombok.Add(bValasz_Btn);
            valaszGombok.Add(cValasz_Btn);
            valaszGombok.Add(dValasz_Btn);

            lapGombok.Add(lap1_Btn);
            lapGombok.Add(lap2_Btn);
            lapGombok.Add(lap3_Btn);
            lapGombok.Add(lap4_Btn);
            lapGombok.Add(lap5_Btn);
            lapGombok.Add(lap6_Btn);
            lapGombok.Add(lap7_Btn);
            lapGombok.Add(lap8_Btn);
            lapGombok.Add(lap9_Btn);
            lapGombok.Add(lap10_Btn);
        }

        void FajlokBeolvasasa()
        {
            string[] fajlok = Directory.GetFiles(@".\Feladatok\", "*.txt");
            for (int i = 0; i < fajlok.Length; i++)
            {
                string tantargyNev = fajlok[i].Substring(12, fajlok[i].Length - 16);
                tantargyakNevLista.Add(tantargyNev);

                var sorok = File.ReadAllLines(fajlok[i], System.Text.Encoding.GetEncoding("iso-8859-1"));
                for (int s = 0; s < sorok.Length; s++)
                {
                    osszesFeladatLista.Add(new KvizFeladat(sorok[s], tantargyNev));
                }
            }
        }

        private void tantargy_CBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            temakor_CBx.IsEnabled = true;
            string kijeloltTantargy = (string)tantargy_CBx.SelectedItem;

            foreach (KvizFeladat f in osszesFeladatLista)
            {
                if (!temakorokLista.Contains(f.Temakor))
                    temakorokLista.Add(f.Temakor);
            }

            temakor_CBx.ItemsSource = temakorokLista;
        }

        private void temakor_CBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            tesztInditasa_Btn.IsEnabled = true;
        }

        // START
        private void tesztInditasa_Btn_Click(object sender, RoutedEventArgs e)
        {
            var tizFeladat = RandomKvizFeladatok((string)tantargy_CBx.SelectedItem, (string)temakor_CBx.SelectedItem);

            kviz = new KvizKezelo(tizFeladat, lapGombok, kerdes_Lbl, kerdes_BG, aValasz_Btn, bValasz_Btn, cValasz_Btn, dValasz_Btn, index_Lbl, prev_Btn, next_Btn, kiertekeles_Btn);
            kviz.Mutasd();

            tantargy_CBx.IsEnabled = false;
            temakor_CBx.IsEnabled = false;
            tesztInditasa_Btn.IsEnabled = false;
        }

        List<KvizFeladat> RandomKvizFeladatok(string tantargy, string temakor)
        {
            List<KvizFeladat> tmpFeladatLista = new List<KvizFeladat>();
            foreach (KvizFeladat f in osszesFeladatLista)
            {
                if (f.Tantargy == tantargy && f.Temakor == temakor)
                    tmpFeladatLista.Add(f);
            }

            List<KvizFeladat> tizFeladat = new List<KvizFeladat>();

            Random rng = new Random();
            for (int i = 0; i < 10; i++)
            {
                int index = rng.Next(tmpFeladatLista.Count);
                tizFeladat.Add(tmpFeladatLista[index]);
                tmpFeladatLista.RemoveAt(index);
            }

            return tizFeladat;
        }

        // NEXT
        private void next_Btn_Click(object sender, RoutedEventArgs e)
        {
            kviz.IranyGomb(true);
            kviz.Mutasd();
        }

        // PREVIOUS
        private void prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            kviz.IranyGomb(false);
            kviz.Mutasd();
        }

        private void valasz_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button valasztottGomb = (Button)sender;
            int gombIndex = valaszGombok.IndexOf(valasztottGomb);
            kviz.Feladat.KivalasztottIndex = gombIndex;
            kviz.LapGombok[kviz.TizFeladat.IndexOf(kviz.Feladat)].Background = Brushes.LightGreen;

            kviz.Mutasd();
        }

        private void kiertekeles_Btn_Click(object sender, RoutedEventArgs e)
        {
            kviz.EredmenyAblak();
        }

        private void lapGomb_Click(object sender, RoutedEventArgs e)
        {
            int lapGombIndex = lapGombok.IndexOf((Button)sender);
            kviz.IranyGomb(lapGombIndex);
            kviz.Mutasd();
        }
    }
}
