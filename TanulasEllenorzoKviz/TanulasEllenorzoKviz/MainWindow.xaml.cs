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
        // Mezők
        List<string> tantargyakNevLista = new List<string>();
        List<string> temakorokLista;
        List<KvizFeladat> osszesFeladatLista = new List<KvizFeladat>();
        List<Button> valaszGombok = new List<Button>();
        List<Button> lapGombok = new List<Button>();
        KvizKezelo kviz;

        // Konstruktor
        public MainWindow()
        {
            InitializeComponent();
            FajlokBeolvasasa();

            tantargy_CBx.ItemsSource = tantargyakNevLista;

            // Válasz gombok
            valaszGombok.Add(aValasz_Btn);
            valaszGombok.Add(bValasz_Btn);
            valaszGombok.Add(cValasz_Btn);
            valaszGombok.Add(dValasz_Btn);

            // 10 lapgomb
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
            
            ElemekEltuntetese();
        }

        // Feladat fájlok beolvasása
        private void FajlokBeolvasasa()
        {
            // Feladat fájlok
            string[] fajlok = Directory.GetFiles(@".\Feladatok\", "*.txt");

            // Minden feladat fájl...
            for (int i = 0; i < fajlok.Length; i++)
            {
                string tantargyNev = fajlok[i].Substring(12, fajlok[i].Length - 16);
                tantargyakNevLista.Add(tantargyNev);

                var sorok = File.ReadAllLines(fajlok[i]);
                // ...minden elemét hozzáadja egy listához mint újan példányosított
                // KvizFeladat objektumokat
                for (int s = 0; s < sorok.Length; s++)
                {
                    osszesFeladatLista.Add(new KvizFeladat(sorok[s], tantargyNev));
                }
            }
        }

        // Bizonyos elemek eltüntetése (ha jelenleg nincs futó kvíz)
        private void ElemekEltuntetese()
        {
            kerdes_BG.Visibility = Visibility.Hidden;
            aValasz_Btn.Visibility = Visibility.Hidden;
            bValasz_Btn.Visibility = Visibility.Hidden;
            cValasz_Btn.Visibility = Visibility.Hidden;
            dValasz_Btn.Visibility = Visibility.Hidden;
            index_Lbl.Visibility = Visibility.Hidden;
            prev_Btn.Visibility = Visibility.Hidden;
            next_Btn.Visibility = Visibility.Hidden;
            kiertekeles_Btn.Visibility = Visibility.Hidden;

            foreach (Button btn in lapGombok)
            {
                btn.Visibility = Visibility.Hidden;
            }
        }

        // Tantárgy változtatása / választása esetén
        private void tantargy_CBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            temakorokLista = new List<string>();
            // Témakör választó ComboBox bekapcsolása
            temakor_CBx.IsEnabled = true;
            string kijeloltTantargy = (string)tantargy_CBx.SelectedItem;

            // A kiválaszott tantárgy alapján beállítja a kiválasztható témaköröket
            foreach (KvizFeladat f in osszesFeladatLista)
            {
                if (kijeloltTantargy == f.Tantargy && !temakorokLista.Contains(f.Temakor))
                    temakorokLista.Add(f.Temakor);
            }
            temakor_CBx.ItemsSource = temakorokLista;
        }

        // Témakör változtatása / választása esetén
        private void temakor_CBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // A Teszt indítása gomb engedélyezése
            tesztInditasa_Btn.IsEnabled = true;
        }

        // Teszt indítása gomb megnyomása esetén
        private void tesztInditasa_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Kiválaszt 10 feladatot
            var tizFeladat = RandomKvizFeladatok((string)tantargy_CBx.SelectedItem, (string)temakor_CBx.SelectedItem);

            // Új KvízKezelőt indít és láthatóvá teszi az eddig rejtett elemeket
            kviz = new KvizKezelo(tizFeladat, lapGombok, kerdes_Lbl, kerdes_BG, aValasz_Btn, bValasz_Btn, cValasz_Btn, dValasz_Btn, index_Lbl, prev_Btn, next_Btn, kiertekeles_Btn);
            kviz.Mutasd();

            // Kikapcsolja a tantárgy és témakör választót, valamint a Teszt indítása gombot
            tantargy_CBx.IsEnabled = false;
            temakor_CBx.IsEnabled = false;
            tesztInditasa_Btn.IsEnabled = false;

            // Ablak címét megváltoztatja
            this.Title = $"Kvíz: {tantargy_CBx.Text} - {temakor_CBx.Text}";
        }

        // Random 10 feladat kiválasztása a kiválasztott tantárgy és a témakör alapján
        List<KvizFeladat> RandomKvizFeladatok(string tantargy, string temakor)
        {
            // Összes adott tantárgyú és témakörű feladat
            List<KvizFeladat> tmpFeladatLista = new List<KvizFeladat>();
            foreach (KvizFeladat f in osszesFeladatLista)
            {
                if (f.Tantargy == tantargy && f.Temakor == temakor)
                    tmpFeladatLista.Add(f);
            }

            // 10 random feladat
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

        // Következő gomb megnyomása esetén
        private void next_Btn_Click(object sender, RoutedEventArgs e)
        {
            kviz.IranyGomb(true);
            kviz.Mutasd();
        }

        // Előző gomb megnyomása esetén
        private void prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            kviz.IranyGomb(false);
            kviz.Mutasd();
        }

        // 4 válaszgomb megnyomása esetén
        private void valasz_Btn_Click(object sender, RoutedEventArgs e)
        {
            Button valasztottGomb = (Button)sender;
            int gombIndex = valaszGombok.IndexOf(valasztottGomb);
            kviz.Feladat.KivalasztottIndex = gombIndex;

            // Lap gomb háttérszín megváltoztatása
            kviz.LapGombok[kviz.TizFeladat.IndexOf(kviz.Feladat)].Background = Brushes.LightGreen;

            kviz.Mutasd();
        }

        // Kiértékelés gomb megyomása esetén
        private void kiertekeles_Btn_Click(object sender, RoutedEventArgs e)
        {
            // Eredmény ablak nyitása
            kviz.EredmenyAblak();

            // Alapértékek visszaállítása
            tantargy_CBx.SelectedIndex = -1;
            tantargy_CBx.IsEnabled = true;
            temakor_CBx.SelectedIndex = -1;
            temakor_CBx.IsEnabled = false;
            tesztInditasa_Btn.IsEnabled = false;
            next_Btn.IsEnabled = true;
            kiertekeles_Btn.IsEnabled = false;
            this.Title = "Tanulás Ellenőrző Kvíz";
            ElemekEltuntetese();
            foreach (Button btn in lapGombok)
            {
                btn.Background = Brushes.LightGray;
            }
            foreach (KvizFeladat f in kviz.TizFeladat)
            {
                f.KivalasztottIndex = -1;
            }
        }

        // Lap gombok megnyomása esetén
        private void lapGomb_Click(object sender, RoutedEventArgs e)
        {
            int lapGombIndex = lapGombok.IndexOf((Button)sender);
            kviz.IranyGomb(lapGombIndex);
            kviz.Mutasd();
        }
    }
}
