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
        string[] valaszok = new string[4];

        public KvizFeladat(string sor, string tantargy)
        {
            this.tantargy = tantargy;

            var sorSplitArr = sor.Split(';');

            this.temakor = sorSplitArr[0];
            this.kerdes = sorSplitArr[1];
            this.valaszok[0] = sorSplitArr[2];
            this.valaszok[1] = sorSplitArr[3];
            this.valaszok[2] = sorSplitArr[4];
            this.valaszok[3] = sorSplitArr[5];
        }

        public string Tantargy { get => tantargy; }
        public string Temakor { get => temakor; }
        public string Kerdes { get => kerdes; }
        public string[] Valaszok { get => valaszok; }
    }
}
