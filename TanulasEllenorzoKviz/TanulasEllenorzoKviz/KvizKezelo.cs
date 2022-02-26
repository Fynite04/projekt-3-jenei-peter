﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace TanulasEllenorzoKviz
{
    class KvizKezelo
    {
        List<KvizFeladat> osszesFeladat;
        KvizFeladat feladat;
        int index;
        //List<string> valaszok;

        Label index_Lbl;
        TextBlock kerdes_Lbl;
        Label kerdes_BG;
        List<Button> valaszGombok = new List<Button>();
        Button prev_Btn;
        Button next_Btn;
        Button kiertekeles_Btn;

        public KvizKezelo(List<KvizFeladat> tizFeladat, TextBlock kerdes_Lbl, Label kerdes_BG, Button aValasz, Button bValasz, Button cValasz, Button dValasz, Label index_Lbl, Button prev_Btn, Button next_Btn, Button kiertekeles_Btn)
        {
            // Értékek beállítása
            this.osszesFeladat = tizFeladat;
            this.feladat = tizFeladat[0];
            this.index = 1;
            //this.valaszok = ValaszKevero(feladat.Valaszok);
            this.index_Lbl = index_Lbl;
            this.kerdes_Lbl = kerdes_Lbl;
            this.kerdes_BG = kerdes_BG;

            this.valaszGombok.Add(aValasz);
            this.valaszGombok.Add(bValasz);
            this.valaszGombok.Add(cValasz);
            this.valaszGombok.Add(dValasz);
            this.prev_Btn = prev_Btn;
            this.next_Btn = next_Btn;
            prev_Btn.IsEnabled = false;
            this.kiertekeles_Btn = kiertekeles_Btn;

            // Láthatóság
            kerdes_BG.Visibility = Visibility.Visible;
            valaszGombok[0].Visibility = Visibility.Visible;
            valaszGombok[1].Visibility = Visibility.Visible;
            valaszGombok[2].Visibility = Visibility.Visible;
            valaszGombok[3].Visibility = Visibility.Visible;
            index_Lbl.Visibility = Visibility.Visible;
            prev_Btn.Visibility = Visibility.Visible;
            next_Btn.Visibility = Visibility.Visible;
            kiertekeles_Btn.Visibility = Visibility.Visible;
        }

        public KvizFeladat Feladat { get => feladat; }

        //private List<string> ValaszKevero(List<string> valaszok)
        //{
        //    Random rng = new Random();

        //    var kevert = valaszok.OrderBy(x => rng.Next()).ToList();

        //    return kevert;
        //}

        public void Mutasd()
        {
            kerdes_Lbl.Text = feladat.Kerdes;

            valaszGombok[0].Content = feladat.Valaszok[0];
            valaszGombok[1].Content = feladat.Valaszok[1];
            valaszGombok[2].Content = feladat.Valaszok[2];
            valaszGombok[3].Content = feladat.Valaszok[3];

            index_Lbl.Content = index + "/10";

            valaszGombok[0].Background = Brushes.LightGray;
            valaszGombok[1].Background = Brushes.LightGray;
            valaszGombok[2].Background = Brushes.LightGray;
            valaszGombok[3].Background = Brushes.LightGray;

            if (feladat.KivalasztottIndex != -1)
            {
                valaszGombok[feladat.KivalasztottIndex].Background = Brushes.LightBlue;
            }

            if (OsszesKesz())
            {
                kiertekeles_Btn.IsEnabled = true;
            }
        }

        public void IranyGomb(bool elore)
        {
            if (elore)
            {
                index++;
                feladat = osszesFeladat[index - 1];
            }
            else
            {
                index--;
                feladat = osszesFeladat[index - 1];
            }
            next_Btn.IsEnabled = (index == 10) ? false : true;
            prev_Btn.IsEnabled = (index == 1) ? false : true;
        }

        private bool OsszesKesz()
        {
            bool keszVagyNem = true;
            foreach (KvizFeladat f in osszesFeladat)
            {
                if (f.KivalasztottIndex == -1)
                    keszVagyNem = false;
            }

            return keszVagyNem;
        }

        public void EredmenyAblak()
        {
            string szoveg = "";

            KiertekelesAblak ablak2 = new KiertekelesAblak();

            foreach (KvizFeladat f in osszesFeladat)
            {
                szoveg += f.Kerdes + "\n";
                
            }

            ablak2.eredmenySzoveg_TB.Text = szoveg;

            ablak2.ShowDialog();
        }
    }
}
