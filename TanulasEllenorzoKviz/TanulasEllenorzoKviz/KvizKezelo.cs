using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TanulasEllenorzoKviz
{
    class KvizKezelo
    {
        List<KvizFeladat> osszesFeladat;
        KvizFeladat feladat;
        int index;
        List<string> valaszok;

        Label index_Lbl;
        TextBlock kerdes_Lbl;
        Label kerdes_BG;
        List<Button> valaszGombok = new List<Button>();
        Button prev_Btn;
        Button next_Btn;

        public KvizKezelo(List<KvizFeladat> feladatok, TextBlock kerdes_Lbl, Label kerdes_BG, Button aValasz, Button bValasz, Button cValasz, Button dValasz, Label index_Lbl, Button prev_Btn, Button next_Btn)
        {
            // Értékek beállítása
            this.osszesFeladat = feladatok;
            this.feladat = feladatok[0];
            this.index = 1;
            this.valaszok = ValaszKevero(feladat.Valaszok);
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

            // Láthatóság
            kerdes_BG.Visibility = System.Windows.Visibility.Visible;
            valaszGombok[0].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[1].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[2].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[3].Visibility = System.Windows.Visibility.Visible;
            index_Lbl.Visibility = System.Windows.Visibility.Visible;
            prev_Btn.Visibility = System.Windows.Visibility.Visible;
            next_Btn.Visibility = System.Windows.Visibility.Visible;
        }

        public string Kerdes { get => feladat.Kerdes; }
        public List<string> Valaszok { get => valaszok; } 

        private List<string> ValaszKevero(List<string> valaszok)
        {
            Random rng = new Random();

            var kevert = valaszok.OrderBy(x => rng.Next()).ToList();

            return kevert;
        }

        public void Mutasd()
        {
            kerdes_Lbl.Text = Kerdes;

            valaszGombok[0].Content = valaszok[0];
            valaszGombok[1].Content = valaszok[1];
            valaszGombok[2].Content = valaszok[2];
            valaszGombok[3].Content = valaszok[3];

            index_Lbl.Content = index + "/10";
        }

        public void IranyGomb(bool elore)
        {
            if (elore)
            {
                index++;
                feladat = osszesFeladat[index - 1];
                valaszok = ValaszKevero(feladat.Valaszok);
            }
            else
            {
                index--;
                feladat = osszesFeladat[index - 1];
                valaszok = ValaszKevero(feladat.Valaszok);
            }
            next_Btn.IsEnabled = (index == 10) ? false : true;
            prev_Btn.IsEnabled = (index == 1) ? false : true;
        }
    }
}
