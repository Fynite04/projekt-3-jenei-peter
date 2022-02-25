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

        public KvizKezelo(List<KvizFeladat> feladatok, TextBlock kerdes_Lbl, Label kerdes_BG, Button aValasz, Button bValasz, Button cValasz, Button dValasz, Label index_Lbl)
        {
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

            kerdes_BG.Visibility = System.Windows.Visibility.Visible;
            valaszGombok[0].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[1].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[2].Visibility = System.Windows.Visibility.Visible;
            valaszGombok[3].Visibility = System.Windows.Visibility.Visible;
            index_Lbl.Visibility = System.Windows.Visibility.Visible;
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
    }
}
