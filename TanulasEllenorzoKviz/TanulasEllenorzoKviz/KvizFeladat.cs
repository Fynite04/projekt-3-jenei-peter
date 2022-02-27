using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanulasEllenorzoKviz
{
    class KvizFeladat
    {
        // Mezők
        string tantargy;
        string temakor;
        string kerdes;
        List<string> valaszok = new List<string>();
        int kivalaszottIndex = -1;
        int joIndex;

        // Konstruktor
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

            valaszok = ValaszKevero(valaszok);

            this. joIndex = valaszok.IndexOf(sorSplitArr[2]);
        }

        // Tulajdonságok
        public string Tantargy { get => tantargy; }
        public string Temakor { get => temakor; }
        public string Kerdes { get => kerdes; }
        public List<string> Valaszok { get => valaszok; }
        public int JoIndex { get => joIndex; }
        public int KivalasztottIndex { get => kivalaszottIndex; set => kivalaszottIndex = value; }
        public int Pont 
        { 
            get 
            {
                if (kivalaszottIndex == joIndex)
                    return 1;
                else
                    return 0;
            } 
        }

        // Randomizálja a válaszokat a listában, hogy kiszámíthatatlan legyen a megjelenítése
        private List<string> ValaszKevero(List<string> valaszok)
        {
            Random rng = new Random();

            var kevert = valaszok.OrderBy(x => rng.Next()).ToList();

            return kevert;
        }
    }
}
