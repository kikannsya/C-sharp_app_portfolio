using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.LinkLabel;

namespace ShotestRoute
{
    public partial class Form1 : Form
    {

        Dictionary<string, int> Marunouchi = new Dictionary<string, int>();
        Dictionary<string, int> KeihinTohoku = new Dictionary<string, int>();
        Dictionary<string, int> Ginza = new Dictionary<string, int>();
        Dictionary<string, int> Saikyou = new Dictionary<string, int>();
        Dictionary<string, int> Yamanote = new Dictionary<string, int>();
        Dictionary<string, int> Chiyoda = new Dictionary<string, int>();
        Dictionary<string, int> Soubu = new Dictionary<string, int>();
        Dictionary<string, int> TokyuToyoko = new Dictionary<string, int>();
        Dictionary<string, int> Tozai = new Dictionary<string, int>();
        Dictionary<string, int> Nanboku = new Dictionary<string, int>();
        Dictionary<string, int> Hibiya = new Dictionary<string, int>();
        Dictionary<string, int> Hanzoumon = new Dictionary<string, int>();
        Dictionary<string, int> Fukutoshin = new Dictionary<string, int>();
        Dictionary<string, int> Yurakutyo = new Dictionary<string, int>();

        List<Node> stations = new List<Node>();
        public Form1()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader("./linedata/丸ノ内線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Marunouchi.Add(datas[0], int.Parse(datas[1]));


                }
            }

            

            using (StreamReader sr = new StreamReader("./linedata/京浜東北線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    KeihinTohoku.Add(datas[0], int.Parse(datas[1]));


                }
            }

            

            using (StreamReader sr = new StreamReader("./linedata/銀座線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Ginza.Add(datas[0], int.Parse(datas[1]));


                }
            }

            

            using (StreamReader sr = new StreamReader("./linedata/埼京線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Saikyou.Add(datas[0], int.Parse(datas[1]));


                }
            }



            using (StreamReader sr = new StreamReader("./linedata/山手線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    if (!Yamanote.ContainsKey(datas[0])) {
                        Yamanote.Add(datas[0], int.Parse(datas[1]));
                    }
                    else
                    {
                        Yamanote.Add(datas[0]+'2', int.Parse(datas[1]));
                    }

                }
            }

            using (StreamReader sr = new StreamReader("./linedata/千代田線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Chiyoda.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/総武線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Soubu.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/東急東横線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    TokyuToyoko.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/東西線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Tozai.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/南北線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Nanboku.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/日比谷線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Hibiya.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/半蔵門線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Hanzoumon.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/副都心線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Fukutoshin.Add(datas[0], int.Parse(datas[1]));


                }
            }

            using (StreamReader sr = new StreamReader("./linedata/有楽町線.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();

                    string[] datas = s.Split(',');

                    Yurakutyo.Add(datas[0], int.Parse(datas[1]));


                }
            }

            List<string> alreadyAdded = new List<string>();
            int i;

            foreach (string Key in Marunouchi.Keys) {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {
                    if (!(Marunouchi.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key) - 1] + ",丸ノ内線", Math.Abs(Marunouchi[Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key) - 1]] - Marunouchi[Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Marunouchi.Keys.ToList().IndexOf(Key) + 1 != Marunouchi.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key) + 1] + ",丸ノ内線", Math.Abs(Marunouchi[Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key) + 1]] - Marunouchi[Marunouchi.Keys.ToList()[Marunouchi.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }
            foreach (string Key in Yamanote.Keys)
            {
                if (!alreadyAdded.Contains(Key) && !Key.Contains('2'))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Yamanote.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        if (!Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) - 1].Contains('2')) stations[i].nextTo.Add(Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) - 1] + ",山手線", Math.Abs(Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) - 1]] - Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key)]]));

                    }
                    else
                    {

                        if (!Yamanote.Keys.ToList()[Yamanote.Keys.ToList().Count - 2].Contains('2')) stations[i].nextTo.Add(Yamanote.Keys.ToList()[Yamanote.Keys.ToList().Count - 2] + ",山手線", Math.Abs(Yamanote[Yamanote.Keys.ToList().Last()] - Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().Count - 2]]));

                    }

                    if (Yamanote.Keys.ToList().IndexOf(Key) + 1 != Yamanote.Keys.ToList().Count)
                    {

                        if (!Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) + 1].Contains('2')) stations[i].nextTo.Add(Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) + 1] + ",山手線", Math.Abs(Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) + 1]] - Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key)]]));
                        else stations[i].nextTo.Add(Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) + 1].Replace("2", "") + ",山手線", Math.Abs(Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key) + 1]] - Yamanote[Yamanote.Keys.ToList()[Yamanote.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }
            foreach (string Key in KeihinTohoku.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {
                    if (!(KeihinTohoku.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key) - 1] + ",京浜東北線", Math.Abs(KeihinTohoku[KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key) - 1]] - KeihinTohoku[KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key)]]));

                    }

                    if (KeihinTohoku.Keys.ToList().IndexOf(Key) + 1 != KeihinTohoku.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key) + 1] + ",京浜東北線", Math.Abs(KeihinTohoku[KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key) + 1]] - KeihinTohoku[KeihinTohoku.Keys.ToList()[KeihinTohoku.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Ginza.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Ginza.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key) - 1] + ",銀座線", Math.Abs(Ginza[Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key) - 1]] - Ginza[Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key)]]));

                    }

                    if (Ginza.Keys.ToList().IndexOf(Key) + 1 != Ginza.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key) + 1] + ",銀座線", Math.Abs(Ginza[Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key) + 1]] - Ginza[Ginza.Keys.ToList()[Ginza.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Saikyou.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Saikyou.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key) - 1] + ",埼京線", Math.Abs(Saikyou[Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key) - 1]] - Saikyou[Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key)]]));

                    }

                    if (Saikyou.Keys.ToList().IndexOf(Key) + 1 != Saikyou.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key) + 1] + ",埼京線", Math.Abs(Saikyou[Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key) + 1]] - Saikyou[Saikyou.Keys.ToList()[Saikyou.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }



            foreach (string Key in Chiyoda.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Chiyoda.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key) - 1] + ",千代田線", Math.Abs(Chiyoda[Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key) - 1]] - Chiyoda[Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Chiyoda.Keys.ToList().IndexOf(Key) + 1 != Chiyoda.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key) + 1] + ",千代田線", Math.Abs(Chiyoda[Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key) + 1]] - Chiyoda[Chiyoda.Keys.ToList()[Chiyoda.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Soubu.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Soubu.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key) - 1] + ",総武線", Math.Abs(Soubu[Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key) - 1]] - Soubu[Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Soubu.Keys.ToList().IndexOf(Key) + 1 != Soubu.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key) + 1] + ",総武線", Math.Abs(Soubu[Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key) + 1]] - Soubu[Soubu.Keys.ToList()[Soubu.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in TokyuToyoko.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(TokyuToyoko.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key) - 1] + ",総武線", Math.Abs(TokyuToyoko[TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key) - 1]] - TokyuToyoko[TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (TokyuToyoko.Keys.ToList().IndexOf(Key) + 1 != TokyuToyoko.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key) + 1] + ",総武線", Math.Abs(TokyuToyoko[TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key) + 1]] - TokyuToyoko[TokyuToyoko.Keys.ToList()[TokyuToyoko.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Tozai.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
               i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Tozai.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key) - 1] + ",東西線", Math.Abs(Tozai[Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key) - 1]] - Tozai[Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Tozai.Keys.ToList().IndexOf(Key) + 1 != Tozai.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key) + 1] + ",東西線", Math.Abs(Tozai[Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key) + 1]] - Tozai[Tozai.Keys.ToList()[Tozai.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Nanboku.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Nanboku.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key) - 1] + ",南北線", Math.Abs(Nanboku[Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key) - 1]] - Nanboku[Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Nanboku.Keys.ToList().IndexOf(Key) + 1 != Nanboku.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key) + 1] + ",南北線", Math.Abs(Nanboku[Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key) + 1]] - Nanboku[Nanboku.Keys.ToList()[Nanboku.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Hibiya.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Hibiya.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key) - 1] + ",日比谷線", Math.Abs(Hibiya[Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key) - 1]] - Hibiya[Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Hibiya.Keys.ToList().IndexOf(Key) + 1 != Hibiya.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key) + 1] + ",日比谷線", Math.Abs(Hibiya[Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key) + 1]] - Hibiya[Hibiya.Keys.ToList()[Hibiya.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Hanzoumon.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }
                i = serchStationName(Key);

                if (i != -1)
                {

                    if (!(Hanzoumon.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key) - 1] + ",半蔵門線", Math.Abs(Hanzoumon[Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key) - 1]] - Hanzoumon[Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Hanzoumon.Keys.ToList().IndexOf(Key) + 1 != Hanzoumon.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key) + 1] + ",半蔵門線", Math.Abs(Hanzoumon[Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key) + 1]] - Hanzoumon[Hanzoumon.Keys.ToList()[Hanzoumon.Keys.ToList().IndexOf(Key)]]));

                    }
                }
            }

            foreach (string Key in Fukutoshin.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }

                i = serchStationName(Key);

                if (i != -1)
                {
                    if (!(Fukutoshin.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key) - 1] + ",副都心線", Math.Abs(Fukutoshin[Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key) - 1]] - Fukutoshin[Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Fukutoshin.Keys.ToList().IndexOf(Key) + 1 != Fukutoshin.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key) + 1] + ",副都心線", Math.Abs(Fukutoshin[Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key) + 1]] - Fukutoshin[Fukutoshin.Keys.ToList()[Fukutoshin.Keys.ToList().IndexOf(Key)]]));

                    }

                }
            }

            foreach (string Key in Yurakutyo.Keys)
            {
                if (!alreadyAdded.Contains(Key))
                {
                    stations.Add(new Node(Key));
                    alreadyAdded.Add(Key);

                }

                i = serchStationName(Key);

                if (i != -1)
                {
                    if (!(Yurakutyo.Keys.ToList().IndexOf(Key) - 1 < 0))
                    {

                        stations[i].nextTo.Add(Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key) - 1] + ",有楽町線", Math.Abs(Yurakutyo[Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key) - 1]] - Yurakutyo[Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key)]]));

                    }


                    if (Yurakutyo.Keys.ToList().IndexOf(Key) + 1 != Yurakutyo.Keys.ToList().Count)
                    {

                        stations[i].nextTo.Add(Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key) + 1] + ",有楽町線", Math.Abs(Yurakutyo[Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key) + 1]] - Yurakutyo[Yurakutyo.Keys.ToList()[Yurakutyo.Keys.ToList().IndexOf(Key)]]));

                    }

                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (comboBox1.Text == "丸ノ内線") { 
            
                foreach(string Key in Marunouchi.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            
            }
            if (comboBox1.Text == "京浜東北線")
            {
                foreach (string Key in KeihinTohoku.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "銀座線")
            {
                foreach (string Key in Ginza.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "埼京線")
            {
                foreach (string Key in Saikyou.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "山手線")
            {
                foreach (string Key in Yamanote.Keys)
                {
                    if (!Key.Contains('2'))
                    {
                        listBox1.Items.Add(Key);
                    }
                }
            }
            if (comboBox1.Text == "千代田線")
            {
                foreach (string Key in Chiyoda.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "総武線")
            {
                foreach (string Key in Soubu.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "東急東横線")
            {
                foreach (string Key in TokyuToyoko.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "東西線")
            {
                foreach (string Key in Tozai.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "南北線")
            {
                foreach (string Key in Nanboku.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "日比谷線")
            {
                foreach (string Key in Hibiya.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "半蔵門線")
            {
                foreach (string Key in Hanzoumon.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "副都心線")
            {
                foreach (string Key in Fukutoshin.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }
            if (comboBox1.Text == "有楽町線")
            {
                foreach (string Key in Yurakutyo.Keys)
                {

                    listBox1.Items.Add(Key);

                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            if (comboBox2.Text == "丸ノ内線")
            {

                foreach (string Key in Marunouchi.Keys)
                {

                    listBox2.Items.Add(Key);

                }

            }
            if (comboBox2.Text == "京浜東北線")
            {
                foreach (string Key in KeihinTohoku.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "銀座線")
            {
                foreach (string Key in Ginza.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "埼京線")
            {
                foreach (string Key in Saikyou.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "山手線")
            {
                foreach (string Key in Yamanote.Keys)
                {
                    if (!Key.Contains('2'))
                    {
                        listBox2.Items.Add(Key);
                    }
                }
            }
            if (comboBox2.Text == "千代田線")
            {
                foreach (string Key in Chiyoda.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "総武線")
            {
                foreach (string Key in Soubu.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "東急東横線")
            {
                foreach (string Key in TokyuToyoko.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "東西線")
            {
                foreach (string Key in Tozai.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "南北線")
            {
                foreach (string Key in Nanboku.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "日比谷線")
            {
                foreach (string Key in Hibiya.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "半蔵門線")
            {
                foreach (string Key in Hanzoumon.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "副都心線")
            {
                foreach (string Key in Fukutoshin.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }
            if (comboBox2.Text == "有楽町線")
            {
                foreach (string Key in Yurakutyo.Keys)
                {

                    listBox2.Items.Add(Key);

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string start="";
            string destination="";
            try
            {
                start = listBox1.SelectedItem.ToString();
            }
            catch (Exception)
            {
                return;
            }

            try
            {
                destination = listBox2.SelectedItem.ToString();

            }
            catch(Exception)
            {
                return;

            }

            

            int startIndex = serchStationName(start);
            int destinationIndex = serchStationName(destination);

            // initialize
            for (int i = 0; i < stations.Count; i++) stations[i].reset();

            stations[destinationIndex].costToDestination = 0;
            stations[destinationIndex].beforeNodeIndex = destinationIndex;
            int minimumIndex = destinationIndex;
            int serchIndex;


            //until all flags become true

            while (checkAllFlag())
            {

                minimumIndex = serchMinimum();

                
                    foreach (string Key in stations[minimumIndex].nextTo.Keys)
                    {
                        string[] stationInfo = Key.Split(',');
                        serchIndex = serchStationName(stationInfo[0]);

                        if (stations[serchIndex].costToDestination >= stations[minimumIndex].costToDestination + stations[minimumIndex].nextTo[Key])
                        {
                            stations[serchIndex].costToDestination = stations[minimumIndex].costToDestination + stations[minimumIndex].nextTo[Key];
                            stations[serchIndex].beforeNodeIndex = minimumIndex;

                        }

                    }

                    stations[minimumIndex].flag = true;
                
            }

            // Result

            showResult(startIndex, destinationIndex);

        }

        public int serchStationName(string searchWord)
        {

            for(int i = 0; i < stations.Count ; i++)
            {

                if (stations[i].stationName == searchWord) return i;

            }

            return -1;
        }

        public int serchMinimum()
        {

            int minIndex = 0;
            int minCost = 1000; 
            

            for (int i = 0; i < stations.Count; i++)
            {
                    if (!stations[i].flag && stations[i].costToDestination < minCost)
                    {
                        minIndex = i;
                        minCost = stations[i].costToDestination;
                    }
                
            }

            return minIndex;
        }

        

        public bool checkAllFlag()
        {

            bool ret = false;
            int count = 0;

            for (int i = 0; i < stations.Count; i++) {

                if (!stations[i].flag) ret = true;
                else count++;

            }

            progressBar1.Value = count;

            return ret;
        }

        public void showResult(int startIndex, int destinationIndex)
        {
            listBox3.Items.Clear();


            int index = startIndex;
            listBox3.Items.Add($"総時間{stations[index].costToDestination}分");
            listBox3.Items.Add("");
            listBox3.Items.Add("");

            List<int> Line = new List<int>();
            Dictionary<string, int> linecost = new Dictionary<string, int>();
            string beforeLine ="";
            
            while (index != destinationIndex)
            {

                Line.Add(index);

                index = stations[index].beforeNodeIndex;


            }
            Line.Add(index);

            int Count = 0;

            for (int i = 0; i < Line.Count; i++)
            {


                if (0<i && i<Line.Count-1)
                {
                    linecost = searchLineName(Line[i], stations[Line[i + 1]].stationName);

                    if (MinLine(linecost) == beforeLine)
                    {

                        Count += linecost[beforeLine];

                    }
                    else
                    {
                        listBox3.Items.Add($"↓　{beforeLine}  {Count}分");
                        listBox3.Items.Add($"{stations[Line[i]].stationName}");
                        listBox3.Items.Add($"乗り換え");

                        
                        
                        
                        beforeLine = MinLine(linecost);
                        Count = 0;
                        Count += linecost[beforeLine];
                    }

                }
                else if (i == 0)
                {

                    listBox3.Items.Add($"{stations[Line[i]].stationName}    出発駅");

                    linecost = searchLineName(Line[0], stations[Line[1]].stationName);

                    beforeLine = MinLine(linecost);

                    Count += linecost[beforeLine];

                }

                else if(i == Line.Count-1){

                    listBox3.Items.Add($"↓　{beforeLine}  {Count}分");
                    listBox3.Items.Add($"{stations[Line[i]].stationName}    目的駅");
                
                
                }
                

            }

            return;
        }

       public Dictionary<string,int> searchLineName(int stationIndex, string stationName)
        {
            Dictionary<string, int> Lines = new Dictionary<string, int>();

            foreach (string key in stations[stationIndex].nextTo.Keys)
            {
                string[] keySplit = key.Split(',');

                if (keySplit[0] == stationName)
                {

                    Lines.Add(keySplit[1], stations[stationIndex].nextTo[key]);

                }


            }

            return Lines;

        }

        public string MinLine(Dictionary<string, int> Lines)
        {
            int min = 100;
            string minKey = "";

            foreach (string key in Lines.Keys)
            {

                if (Lines[key] < min)
                {
                    minKey = key;
                    min = Lines[key];

                }                


            }

            return minKey;

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    public class Node
    {

        public string stationName;
        public Dictionary<string, int> nextTo;
        public int costToDestination;
        public int beforeNodeIndex;
        public bool flag;

        public Node(string name) {

            stationName = name;
            nextTo = new Dictionary<string, int>();
            costToDestination = 1000;
            flag = false;

        }

        public void reset()
        {

            costToDestination = 1000;
            flag = false;

        }

    }

}
