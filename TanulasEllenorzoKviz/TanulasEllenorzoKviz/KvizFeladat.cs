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
        }

        public string Tantargy { get => tantargy; }
        public string Temakor { get => temakor; }
        public string Kerdes { get => kerdes; }
        public List<string> Valaszok { get => valaszok; }
        public string JoValasz { get => valaszok[0]; }
    }
}
