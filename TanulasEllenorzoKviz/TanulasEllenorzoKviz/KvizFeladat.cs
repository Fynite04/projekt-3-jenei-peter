using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanulasEllenorzoKviz
{
    class KvizFeladat
    {
        string tantargy;
        string temakor;
        string kerdes;
        List<string> valaszok = new List<string>();
        int kivalaszottIndex = -1;
        int joIndex;

        public KvizFeladat(string sor, string tantargy)
        {
            this.tantargy = tantargy;

            var sorSplitArr = sor.Split(';');

            this.temakor = sorSplitArr[0];
            this.kerdes = sorSplitArr[1];
            this.valaszok.Add(sorSplitArr[2]);
            this.valaszok.Add(sorSplitArr[3]);
            this.valaszok.Add(sorSplitArr[4]);
            this.valaszok.Add(sorSplitArr[5]);
            this. joIndex = valaszok.IndexOf(sorSplitArr[2]);

            valaszok = ValaszKevero(valaszok);
        }

        public string Tantargy { get => tantargy; }
        public string Temakor { get => temakor; }
        public string Kerdes { get => kerdes; }
        public List<string> Valaszok { get => valaszok; }
        public string JoValasz { get => valaszok[0]; }
        public int KivalasztottIndex { get => kivalaszottIndex; set => kivalaszottIndex = value; }

        private List<string> ValaszKevero(List<string> valaszok)
        {
            Random rng = new Random();

            var kevert = valaszok.OrderBy(x => rng.Next()).ToList();

            return kevert;
        }
    }
}
