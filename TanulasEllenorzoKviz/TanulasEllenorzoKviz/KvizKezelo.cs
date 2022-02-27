using System;
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
        // Mezők
        List<KvizFeladat> osszesFeladat;
        KvizFeladat feladat;
        int index;
        Label index_Lbl;
        TextBlock kerdes_Lbl;
        Label kerdes_BG;
        List<Button> valaszGombok = new List<Button>();
        List<Button> lapGombok;
        Button prev_Btn;
        Button next_Btn;
        Button kiertekeles_Btn;

        // Konstruktor
        public KvizKezelo(List<KvizFeladat> tizFeladat, List<Button> lapGombok, TextBlock kerdes_Lbl, Label kerdes_BG, Button aValasz, Button bValasz, Button cValasz, Button dValasz, Label index_Lbl, Button prev_Btn, Button next_Btn, Button kiertekeles_Btn)
        {
            // Értékek beállítása
            this.lapGombok = lapGombok;
            this.osszesFeladat = tizFeladat;
            this.feladat = tizFeladat[0];
            this.index = 1;
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
            foreach (Button btn in lapGombok)
            {
                btn.Visibility = Visibility.Visible;
            }
        }

        // Tulajdonságok
        public KvizFeladat Feladat { get => feladat; }
        public List<KvizFeladat> TizFeladat { get => osszesFeladat; }
        public List<Button> LapGombok { get => lapGombok; }

        // Elemek mutatása, frissítése
        public void Mutasd()
        {
            // Kérdések, válaszok, index értékének frissítése
            kerdes_Lbl.Text = feladat.Kerdes;
            valaszGombok[0].Content = feladat.Valaszok[0];
            valaszGombok[1].Content = feladat.Valaszok[1];
            valaszGombok[2].Content = feladat.Valaszok[2];
            valaszGombok[3].Content = feladat.Valaszok[3];
            index_Lbl.Content = index + "/10";

            // Gombok színének alapértelmezettre állítása
            valaszGombok[0].Background = Brushes.LightGray;
            valaszGombok[1].Background = Brushes.LightGray;
            valaszGombok[2].Background = Brushes.LightGray;
            valaszGombok[3].Background = Brushes.LightGray;

            // Kiválasztott válasz gomb színének állítása
            if (feladat.KivalasztottIndex != -1)
                valaszGombok[feladat.KivalasztottIndex].Background = Brushes.LightGreen;

            // Ha kész az összes feladat, a Kiértékelés gombot engedélyezi
            if (OsszesKesz())
                kiertekeles_Btn.IsEnabled = true;
        }

        // A 2 oldalsó iránygombok megnyomása esetén változtatja a jelenlegi feladatot
        public void IranyGomb(bool elore)
        {
            // Tovább gomb
            if (elore)
            {
                index++;
                feladat = osszesFeladat[index - 1];
            }
            // Vissza gomb
            else
            {
                index--;
                feladat = osszesFeladat[index - 1];
            }

            // Kikapcsolja / bekapcsolja a gombokat az index szerint, hogy ne
            // lehessen min. alá vagy a max. felé menni
            next_Btn.IsEnabled = (index == 10) ? false : true;
            prev_Btn.IsEnabled = (index == 1) ? false : true;
        }

        // A 10 középső iránygombok alapján változtatja a jelenlegi feladatot
        public void IranyGomb(int lapIndex)
        {
            index = lapIndex + 1;
            feladat = osszesFeladat[lapIndex];

            next_Btn.IsEnabled = (index == 10) ? false : true;
            prev_Btn.IsEnabled = (index == 1) ? false : true;
        }

        // Visszaküldi, hogy kész van e az összes feladat
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

        // A Kiértékelés gomb megnyomása esetén új ablakot nyit az eredménnyel
        public void EredmenyAblak()
        {
            string szoveg = "";
            int pont = 0;

            KiertekelesAblak ablak2 = new KiertekelesAblak();

            // A 10 random kérdésen végigmegy
            foreach (KvizFeladat f in osszesFeladat)
            {
                bool joLett = f.JoIndex == f.KivalasztottIndex;

                pont += f.Pont;

                // Index szám
                szoveg += osszesFeladat.IndexOf(f) + 1 + ".\n";
                // Kérdés
                szoveg += f.Kerdes + "\n";
                // 4 válasz
                for (int i = 0; i < f.Valaszok.Count; i++)
                { 
                    // Ha jól sikerült
                    if (joLett)
                    {
                        if (i == f.JoIndex)
                            szoveg += f.Valaszok[i] + " ✔\n";
                        else
                            szoveg += f.Valaszok[i] + "\n";
                    }
                    // Ha nem sikerült jól
                    else
                    {
                        if (i == f.KivalasztottIndex)
                            szoveg += f.Valaszok[i] + " ✖\n";
                        else if (i == f.JoIndex)
                            szoveg += f.Valaszok[i] + " ━\n";
                        else
                            szoveg += f.Valaszok[i] + "\n";
                    }
                }
                // Pont
                szoveg += "                                   " + f.Pont + "p.\n";
                szoveg += "\n";
            }

            // Összes pont (X/10)
            szoveg += $"\n{pont}/10p.";
            ablak2.eredmenySzoveg_TB.Text = szoveg;

            ablak2.ShowDialog();
        }
    }
}
