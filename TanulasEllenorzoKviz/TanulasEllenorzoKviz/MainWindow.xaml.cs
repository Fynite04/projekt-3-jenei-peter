using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace TanulasEllenorzoKviz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> tantargyakNevLista = new List<string>();
        List<KvizFeladat> feladatokLista = new List<KvizFeladat>();
        List<string> temakorokLista = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            FajlokBeolvasasa();

            tantargy_CBx.ItemsSource = tantargyakNevLista;
        }

        void FajlokBeolvasasa()
        {
            string[] fajlok = Directory.GetFiles(@".\Feladatok\", "*.txt");
            for (int i = 0; i < fajlok.Length; i++)
            {
                string tantargyNev = fajlok[i].Substring(12, fajlok[i].Length - 16);
                tantargyakNevLista.Add(tantargyNev);

                var sorok = File.ReadAllLines(fajlok[i]);
                for (int s = 0; s < sorok.Length; s++)
                {
                    feladatokLista.Add(new KvizFeladat(sorok[s], tantargyNev));
                }
            }


        }

        private void tantargy_CBx_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            temakor_CBx.IsEnabled = true;
            string kijeloltTantargy = (string)tantargy_CBx.SelectedItem;

            foreach (KvizFeladat f in feladatokLista)
            {
                if (!temakorokLista.Contains(f.Temakor))
                    temakorokLista.Add(f.Temakor);
            }

            temakor_CBx.ItemsSource = temakorokLista;
        }
    }
}
