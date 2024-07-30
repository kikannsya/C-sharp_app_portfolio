using InoueLab;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System;

namespace LifeGame
{
    public partial class Form1 : Form
    {
        static int width = 50;　　　　　//一辺の長さ
        static int height = 50;
        static int RandomNumber = 100;
        int pictuerNum;
        int[,] map = new int[width, height];        //map上での扱い　0:空白 1:植物　2:動物
        enum Life { None, Plant, Animal}
        List<int[,]> mapMemory = new List<int[,]>();
        Bitmap bmp = new Bitmap(10 * width, 10 * height);
        Graphics g;
        RandomMT rand = new RandomMT();
        public Form1()
        {
            g = Graphics.FromImage(bmp);
            InitializeComponent();
            pictureBox1.Width = 10 * width;
            pictureBox1.Height = 10 *height;
            pictureBox1.Image = bmp;

            reset();
            draw();
        }

        public void calcNextGeneration()
        {
            int subi;
            int addi;
            int subj;
            int addj;
            int animalCount;
            int  plantCount;
            int  spaceCount;

            List<int[]> array = new List<int[]>();
            
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    animalCount = 0;
                    plantCount = 0;
                    spaceCount = 0;
                    subi = i - 1;
                    addi = i + 1;
                    subj = j - 1;
                    addj = j + 1;

                    if (subi < 0) subi += width;
                    if (addi > width - 1) addi -= width;
                    if (subj < 0) subj += height;
                    if (addj > height - 1) addj -= height;

                    array.Clear();

                    array.Add(new int[2] { subi, subj});
                    array.Add(new int[2] { subi, j });
                    array.Add(new int[2] { subi, addj });
                    array.Add(new int[2] { i, subj });
                    array.Add(new int[2] { i, addj });
                    array.Add(new int[2] { addi, subj });
                    array.Add(new int[2] { addi, j });
                    array.Add(new int[2] { addi, addj });

                    for(int k = 0; k < 8; k++)
                    {
                        if (mapMemory.Last()[array[k][0], array[k][1]] == 0) spaceCount++;
                        if (mapMemory.Last()[array[k][0], array[k][1]] == 1) plantCount++;
                        if (mapMemory.Last()[array[k][0], array[k][1]] == 2) animalCount++;

                    }

                    switch (mapMemory.Last()[i, j])
                    {
                        case 0:

                            if (animalCount == 0 && plantCount >= 2) map[i, j] = 1;
                            else if (animalCount == 1 && plantCount >= 5) map[i, j] = 1;
                            else if (animalCount >= 2 && plantCount >= animalCount) map[i, j] = 2;

                            break;

                        case 1:

                            if (plantCount / 2 <= animalCount) map[i, j] = 0;
                            if (plantCount - animalCount >= 6) map[i, j] = 0;

                            break;

                        case 2:

                            if (animalCount == 0) map[i, j] = 0;
                            else if (animalCount / 2 >= plantCount) map[i, j] = 0;

                            break;
                    }

                    
                }
            }

            {
                int[,] dst = new int[50, 50];
                Array.Copy(map, dst, map.Length);
                mapMemory.Add(dst);
                pictuerNum++;
            }
        }
        
        public void draw()
        {
            
            g.Clear(Color.Black);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 1) g.FillRectangle(new SolidBrush(Color.FromArgb(unchecked((int)0xFF03AF7A))), 10 * i, 10 * j, 10, 10);
                    if (map[i, j] == 2) g.FillRectangle(new SolidBrush(Color.FromArgb(unchecked((int)0xFFFF4B00))), 10 * i, 10 * j, 10, 10);
                }
            }

            label1.Text = $"t = {pictuerNum}";
            pictureBox1.Refresh();
        }

        public void reset()
        {
            int random;                     //ランダム変数の定義
            mapMemory = new List<int[,]>(); // mapMemoryのリセット

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    random = rand.Int(RandomNumber);

                    if (random < RandomNumber * 0.1)
                    {
                        map[i, j] = 2;                 // 2:動物

                    }
                    else if (RandomNumber * 0.8 <= random)
                    {

                        map[i, j] = 1;                  // 1:植物

                    }
                    else
                    {

                        map[i, j] = 0;                  //0:空白

                    }

                }
            }

            {
                int[,] dst = new int[50, 50];
                Array.Copy(map, dst, map.Length);
                mapMemory.Add(dst);
                pictuerNum=0;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            timer1.Enabled = false;

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

            if (pictuerNum == mapMemory.Count() - 1) calcNextGeneration();
            else { pictuerNum++; map = mapMemory[pictuerNum]; }
            draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
            draw();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (pictuerNum == mapMemory.Count()-1) calcNextGeneration();
            else { pictuerNum++; map = mapMemory[pictuerNum]; }
            draw();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            pictuerNum--;
            if (pictuerNum >= 0) map = mapMemory[pictuerNum];
            else reset();

            draw();


        }
    }
}