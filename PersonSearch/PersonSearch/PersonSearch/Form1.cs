using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.ObjectModel;

namespace PersonSearch
{
    public partial class Form1 : Form
    {
        List<PersonInfo> perinfo = new List<PersonInfo>();
        List<PersonInfo> showList = new List<PersonInfo>();

        public Form1()
        {
            InitializeComponent();

           
            using (StreamReader sr = new StreamReader("person_data.csv"))
            {

                string rdLine;

                while (!sr.EndOfStream)
                {

                    rdLine = sr.ReadLine();
                    string[] info = rdLine.Split(',');

                    perinfo.Add(new PersonInfo(info[0], info[1], int.Parse(info[2]), info[3], info[4]));
                    
                }

            }

        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "氏名(漢字)")
            {

                searchingBykanji();
                

            }
            if (comboBox1.Text == "氏名(ひらがな)")
            {

                searchingKana();

            }
            
            if (comboBox1.Text == "年齢(半角英数字)")
            {

                searchingByOld();

            }
            if (comboBox1.Text == "出身地(都道府県名)")
            {

                searchingByPref();

            }
            if (comboBox1.Text == "誕生日(〇月〇日)")
            {

                searchingBybirthDay();

            }

            listView1.Items.Clear();

            label1.Text = $":{showList.Count()}件";

            //string showString;

            
            
            ObservableCollection<PersonInfo> show = new ObservableCollection<PersonInfo>();
            for (int i = 0; i < showList.Count(); i++)
            {
                listView1.Items.Add(new ListViewItem(new String[] { showList[i].nameKanji, showList[i].nameKana, showList[i].old.ToString(), showList[i].birthPre, showList[i].birthDay}) ) ;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "氏名(漢字)") {

                radioButton1.Text = "完全一致";
                radioButton2.Text = "部分一致";
                radioButton3.Text = "前方一致";

            }

            if (comboBox1.Text == "氏名(ひらがな)")
            {

                radioButton1.Text = "完全一致";
                radioButton2.Text = "部分一致";
                radioButton3.Text = "前方一致";

            }

            if (comboBox1.Text == "年齢(半角英数字)")
            {

                radioButton1.Text = "一致";
                radioButton2.Text = "以下";
                radioButton3.Text = "以上";

            }

            if (comboBox1.Text == "出身地(都道府県名)")
            {

                radioButton1.Text = "完全一致";
                radioButton2.Text = "部分一致";
                radioButton3.Text = "前方一致";

            }

            if (comboBox1.Text == "誕生日")
            {

                radioButton1.Text = "完全一致";
                radioButton2.Text = "部分一致";
                radioButton3.Text = "前方一致";

            }


        }
        
        private void searchingBykanji()
        {
            string serchWord = textBox1.Text;
            showList.Clear();

            if (radioButton1.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKanji == serchWord)
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton2.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKanji.Contains(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton3.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKanji.StartsWith(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }

        }

        private void searchingKana()
        {
            string serchWord = textBox1.Text;
            showList.Clear();

            if (radioButton1.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKana == serchWord)
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton2.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKana.Contains(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton3.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].nameKana.StartsWith(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }

        }

        private void searchingByOld()
        {
            string serchWord = textBox1.Text;
            showList.Clear();

            if (radioButton1.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].old == int.Parse(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton2.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].old <= int.Parse(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton3.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].old >= int.Parse(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }

        }

        private void searchingByPref()
        {
            string serchWord = textBox1.Text;
            showList.Clear();

            if (radioButton1.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthPre == serchWord)
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton2.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthPre.Contains(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton3.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthPre.StartsWith(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }

        }
        private void searchingBybirthDay()
        {
            string serchWord = textBox1.Text;
            showList.Clear();

            if (radioButton1.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthDay == serchWord)
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton2.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthDay.Contains(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }
            else if (radioButton3.Checked)
            {

                for (int i = 0; i < perinfo.Count(); i++)
                {

                    if (perinfo[i].birthDay.StartsWith(serchWord))
                    {

                        showList.Add(perinfo[i]);


                    }

                }


            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    class PersonInfo
    {

        public string nameKanji;
        public string nameKana;
        public int old;
        public string birthPre;
        public string birthDay;

        public PersonInfo(string Kanji,string Kana, int o, string Pre, string day)
        {
            nameKanji = Kanji;
            nameKana = Kana;
            old = o;
            birthPre = Pre;
            birthDay = day;


        }

        public void SetInfo(string Kanji, string Kana, int o, string Pre, string day)
        {
            nameKanji = Kanji;
            nameKana = Kana;
            old = o;
            birthPre = Pre;
            birthDay = day;

        }



        

    }
        





}
