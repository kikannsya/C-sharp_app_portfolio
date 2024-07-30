using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using InoueLab;


namespace template
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// PictureBoxにはりつける画像
        /// </summary>
        
        Bitmap bmp;
        Graphics g;
        Brush black = new SolidBrush(Color.Black);
        Brush white = new SolidBrush(Color.White);
        Brush cyan = new SolidBrush(Color.Cyan);
        Brush blue = new SolidBrush(Color.Blue);
        Brush orange = new SolidBrush(Color.Orange);

        List<List<int>> map = new List<List<int>>(); //壁なら1、道なら0
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            //pictureBoxに，bmpをはりつける
            pictureBox1.Image = bmp;
            textBox1.Text = "35";
            textBox2.Text = "35";

            comboBox1.SelectedIndex = 0;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        /// <summary>
        /// 棒倒し法
        /// </summary>
        private void CutDown()
        {
            bool colorMode = false;
            int width =  int.Parse(textBox1.Text);
            int height = int.Parse(textBox2.Text);

            map.Clear();
            RandomMT r = new RandomMT();

            List<int> wth = new List<int>();

            for (int i = 0; i < height; i++)
            {
                wth.Clear();
                for (int j = 0; j < width; j++)
                {

                    wth.Add(1);

                }

                map.Add(new List<int>(wth));
            }

            for (int i = 0; i < height; i++)
            {
                
                for (int j = 0; j < width; j++)
                {

                    if((i%2==1 && i != height -1) && (j != 0 && j != width -1)) map[i][j] = 0;


                    if ((i % 2 == 0 && i != height - 1 && i != 0) && (j%2 == 1 && j != 0 && j != width - 1)) map[i][j] = 0;

                }

                
            }


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (i != 0 && i != height - 1 && j != 0 && j != width-1 && i % 2 == 0 && j % 2 == 0)
                    {

                        if (j == 2)
                        {

                            switch (r.Int(4))
                            {

                                case 0:

                                    map[i - 1][j] = 1;
                                    break;

                                case 1:

                                    map[i][j + 1] = 1;
                                    break;

                                case 2:

                                    map[i + 1][j] = 1;
                                    break;

                                case 3:

                                    map[i][j - 1] = 1;
                                    break;

                            }
                        }
                        else
                        {
                            switch (r.Int(3))
                            {

                                case 0:
                                    map[i][j + 1] = 1;
                                    break;

                                case 1:
                                    map[i + 1][j] = 1;
                                    break;

                                case 2:
                                    map[i - 1][j] = 1;
                                    break;

                            }
                        }
                    }
                }
            }
            
            while (true)
                {

                //画面を白または黒で塗りつぶす
                for (int x = 0; x < pictureBox1.Width; x++)
                {
                    for (int y = 0; y < pictureBox1.Height; y++)
                    {
                         bmp.SetPixel(x, y, Color.White);
                        
                    }
                }
               
                draw(new int[] {1,1 }, new int[] { height-2, width -2}, height,width,map);

                solveMaze(new int[] { 1, 1 }, new int[] { height - 2, width - 2 }, height, width);
                break;
                //500ミリ秒=0.5秒待機する
                Thread.Sleep(500);
            }
        }

        ///<summary>
        ///穴掘り法
        /// </summary>
        private void Digging()
        {
            
            int width = int.Parse(textBox1.Text);
            int height = int.Parse(textBox2.Text);

            map.Clear();
            RandomMT r = new RandomMT();
            List<int> wth = new List<int>();
            for (int i = 0; i < height + 2; i++)
            {
                wth.Clear();
                for (int j = 0; j < width + 2; j++)
                {

                    if (i == 0 || i == height + 1)
                    {
                        wth.Add(0);
                    }
                    else if (j == 0 || j == width + 1)
                    {
                        wth.Add(0);
                    }
                    else
                    {
                        wth.Add(1);
                    }

                   

                }

                map.Add(new List<int>(wth));
            }

           
            List<int[]> already = new List<int[]>();
            already.Clear();

            map[2][2] = 0;

            int[] now = new int[] { 2, 2 };
            already.Add(new int[] { now[0], now[1] });
            


            while (true)
            {

                //画面を白または黒で塗りつぶす
                for (int x = 0; x < pictureBox1.Width; x++)
                {
                    for (int y = 0; y < pictureBox1.Height; y++)
                    {
                        bmp.SetPixel(x, y, Color.White);
                        
                    }
                }


                if (map[now[0]][now[1]] == 0 )
                { 

                    switch (r.Int(4))
                    {

                        case 0:
                            if (map[now[0] - 2][now[1]] == 1)
                            {
                                map[now[0] - 1][now[1]] = 0;
                                map[now[0] - 2][now[1]] = 0;
                                now[0] -= 2;

                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }else if (map[now[0]][now[1] + 2] == 1)
                                {
                                    map[now[0]][now[1] + 1] = 0;
                                    map[now[0]][now[1] + 2] = 0;
                                    now[1] += 2;
                                    if (!already.Contains(now))
                                        already.Add(new int[] { now[0], now[1] });

                                }
                                else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 1:
                            if (map[now[0] + 2][now[1]] == 1)
                            {
                                map[now[0] + 1][now[1]] = 0;
                                map[now[0] + 2][now[1]] = 0;
                                now[0] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }else  if (map[now[0]][now[1] - 2] == 1)
                                {
                                    map[now[0]][now[1] - 1] = 0;
                                    map[now[0]][now[1] - 2] = 0;
                                    now[1] -= 2;
                                    if (!already.Contains(now))
                                        already.Add(new int[] { now[0], now[1] });

                                }
                            
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 2:
                            if (map[now[0]][now[1] - 2] == 1)
                            {
                                map[now[0]][now[1] - 1] = 0;
                                map[now[0]][now[1] - 2] = 0;
                                now[1] -= 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else if (map[now[0] - 2][now[1]] == 1)
                            {
                                map[now[0] - 1][now[1]] = 0;
                                map[now[0] - 2][now[1]] = 0;
                                now[0] -= 2;

                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 3:
                            if (map[now[0]][now[1] + 2] == 1)
                            {
                                map[now[0]][now[1] + 1] = 0;
                                map[now[0]][now[1] + 2] = 0;
                                now[1] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }

                            else if (map[now[0] + 2][now[1]] == 1)
                            {
                                map[now[0] + 1][now[1]] = 0;
                                map[now[0] + 2][now[1]] = 0;
                                now[0] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                    }

                
                }else
                {
                    now[0] = already[r.Int(already.Count())][0];
                    now[1] = already[r.Int(already.Count())][1];
                }
                if (map[now[0] + 2][now[1]] == 0 && map[now[0] - 2][now[1]] == 0 && map[now[0]][now[1] + 2] == 0 && map[now[0]][now[1] - 2] == 0)
                {

                    deleteFromList(already ,now);
                    
                }
                draw(new int[] {2, 2}, new int[] {height-1, width-1 }, height+3, width+3 , map);

                if (already.Count() == 0)
                {
                    solveMaze(new int[] { 2, 2 }, new int[] { height - 1, width - 1 }, height + 3, width + 3);
                    break;
                }

                //500ミリ秒=0.5秒待機する
                //Thread.Sleep(5);
            }
        }

        ///<summary>
        ///壁のばし法
        /// </summary>
        private void WallGrowth()
        {

            int width = int.Parse(textBox1.Text);
            int height = int.Parse(textBox2.Text);

            map.Clear();
            RandomMT r = new RandomMT();
            List<int> wth = new List<int>();
            for (int i = 0; i < height + 4; i++)
            {
                wth.Clear();
                for (int j = 0; j < width + 4; j++)
                {

                    if (i < 3 ||  height  < i)
                    {
                        wth.Add(1);
                    }
                    else if (j < 3 || width  < j)
                    {
                        wth.Add(1);
                    }
                    else
                    {
                        wth.Add(0);
                    }



                }

                map.Add(new List<int>(wth));
            }

            List<int[]> already = new List<int[]>();
            already.Clear();

            for(int i=4; i < height + 2; i += 2)
            {
                for (int j = 4; j <width + 2; j += 2)
                {

                    already.Add(new int[] { i, j});


                }

            }

            int[] now = new int[] { already[r.Int(already.Count())][0], already[r.Int(already.Count())][1] };


            while (true)
            {

                //画面を白または黒で塗りつぶす
                for (int x = 0; x < pictureBox1.Width; x++)
                {
                    for (int y = 0; y < pictureBox1.Height; y++)
                    {
                        bmp.SetPixel(x, y, Color.White);

                    }
                }

                if (map[now[0]][now[1]] == 1)
                {

                    switch (r.Int(4))
                    {

                        case 0:
                            if (map[now[0] - 2][now[1]] == 0)
                            {
                                map[now[0] - 1][now[1]] = 1;
                                map[now[0] - 2][now[1]] = 1;
                                now[0] -= 2;

                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else if (map[now[0]][now[1] + 2] == 0)
                            {
                                map[now[0]][now[1] + 1] = 1;
                                map[now[0]][now[1] + 2] = 1;
                                now[1] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 1:
                            if (map[now[0] + 2][now[1]] == 0)
                            {
                                map[now[0] + 1][now[1]] = 1;
                                map[now[0] + 2][now[1]] = 1;
                                now[0] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                           
                            else if (map[now[0]][now[1] - 2] == 0)
                            {
                                map[now[0]][now[1] - 1] = 1;
                                map[now[0]][now[1] - 2] = 1;
                                now[1] -= 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 2:
                            if (map[now[0]][now[1] - 2] == 0)
                            {
                                map[now[0]][now[1] - 1] = 1;
                                map[now[0]][now[1] - 2] = 1;
                                now[1] -= 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else if(map[now[0] - 2][now[1]] == 0)
                            {
                                map[now[0] - 1][now[1]] = 1;
                                map[now[0] - 2][now[1]] = 1;
                                now[0] -= 2;

                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                        case 3:
                            if (map[now[0]][now[1] + 2] == 0)
                            {
                                map[now[0]][now[1] + 1] = 1;
                                map[now[0]][now[1] + 2] = 1;
                                now[1] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }else if (map[now[0] + 2][now[1]] == 0)
                            {
                                map[now[0] + 1][now[1]] = 1;
                                map[now[0] + 2][now[1]] = 1;
                                now[0] += 2;
                                if (!already.Contains(now))
                                    already.Add(new int[] { now[0], now[1] });

                            }
                            else
                            {
                                now[0] = already[r.Int(already.Count())][0];
                                now[1] = already[r.Int(already.Count())][1];
                            }

                            break;

                    }


                }
                else
                {
                    now[0] = already[r.Int(already.Count())][0];
                    now[1] = already[r.Int(already.Count())][1];
                }
                if (map[now[0] + 2][now[1]] == 1 && map[now[0] - 2][now[1]] == 1 && map[now[0]][now[1] + 2] == 1 && map[now[0]][now[1] - 2] == 1)
                {

                    deleteFromList(already, now);

                }

                draw(new int[] {3,3}, new int[] { height, width  }, height + 4, width +4 , map);

                if (already.Count() == 0)
                {
                    solveMaze(new int[] { 3, 3 }, new int[] { height, width }, height + 4, width + 4);
                    break;
                }

                //500ミリ秒=0.5秒待機する
                //Thread.Sleep(500);
            }
        }




        /// <summary>
        /// ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
            int number;
            if (!int.TryParse(textBox1.Text, out number) || !int.TryParse(textBox2.Text, out number) || int.Parse(textBox1.Text)%2 == 0 || int.Parse(textBox1.Text) % 2 == 0)
            {
                MessageBox.Show("1以上の奇数，整数値で幅・高さを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //スレッドを分割し，mainProcess関数を実行する
            if (comboBox1.SelectedIndex == 0)
            {
                ThreadSeparate(ref drawThread, CutDown);
                

            }
            if (comboBox1.SelectedIndex == 1)
            {
                ThreadSeparate(ref drawThread, Digging);
                
            }
            if (comboBox1.SelectedIndex == 2)
            {
                ThreadSeparate(ref drawThread, WallGrowth);
                
            }


            ////スレッド分割なしの場合を体験してみよう
            //mainProcess();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void draw(int[] start, int[] goal,int height, int width,List<List<int>> list)
        {
            

            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = 0; j < list[1].Count(); j++)
                {
                    if (list[i][j] != 0 && list[i][j] != 1 && list[i][j] != 3)
                    {
                        g.FillRectangle(cyan, (int)(pictureBox1.Height / height) * i, (int)(pictureBox1.Width / width) * j, (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));



                    }
                    else
                    {
                        switch (list[i][j])
                        {

                            case 0:
                                g.FillRectangle(white, (int)(pictureBox1.Height / height) * i, (int)(pictureBox1.Width / width) * j, (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));
                                break;

                            case 1:
                                g.FillRectangle(black, (int)(pictureBox1.Height / height) * i, (int)(pictureBox1.Width / width) * j, (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));
                                break;

                            case 3:
                                g.FillRectangle(blue, (int)(pictureBox1.Height / height) * i, (int)(pictureBox1.Width / width) * j, (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));
                                break;
                        }
                    }

                }
            }

            g.FillRectangle(orange, (int)(pictureBox1.Height / height) * start[0], (int)(pictureBox1.Width / width) * start[1], (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));
            g.FillRectangle(blue, (int)(pictureBox1.Height / height) * goal[0], (int)(pictureBox1.Width / width) * goal[1], (int)(pictureBox1.Height / height), (int)(pictureBox1.Width / width));
            //pictureBoxの中身を塗り替える
            InterThreadRefresh(this.pictureBox1.Refresh);
        }

        public List<int[]> deleteFromList(List<int[]> list, int[] target)
        {
            bool Flag ;

            for(int i=0; i < list.Count(); i++)
            {
                Flag = true;

                for(int j=0; j < target.Count(); j++)
                {

                    if (list[i][j] != target[j]) Flag = false;

                }

                if (Flag) list.RemoveAt(i);

            }

            return new List<int[]> (list);

        }

        public void solveMaze(int[] start, int[] goal, int height,int width) { 

            var MazeAnswer = new List<List<int>>(map);
            Queue<int[]> solveMaze = new Queue<int[]>();
            solveMaze.Enqueue(goal);
            int[] now = new int[] { goal[0], goal[1] };

            while (true)
            {
                if (map[now[0] + 1][now[1]] == 0) { 
                    solveMaze.Enqueue(new int[] { now[0] + 1, now[1] });
                    MazeAnswer[now[0] + 1][now[1]] = 7;
                }


                if (map[now[0] - 1][now[1]] == 0)
                {
                    solveMaze.Enqueue(new int[] { now[0] - 1, now[1] });
                    MazeAnswer[now[0] - 1][now[1]] = 6;
                }

                if (map[now[0]][now[1] + 1] == 0)
                {
                    solveMaze.Enqueue(new int[] { now[0], now[1] + 1 });
                    MazeAnswer[now[0]][now[1] + 1] = 5;
                }
                if (map[now[0]][now[1] - 1] == 0)
                {
                    solveMaze.Enqueue(new int[] { now[0], now[1] - 1 });
                    MazeAnswer[now[0]][now[1] - 1] = 4;
                }

                now = solveMaze.Dequeue();
                if (now[0] == start[0] && now[1] == start[1]) break;
                draw(start, goal, height, width, MazeAnswer);

                Thread.Sleep(5);
            }

            now = new int[] { start[0], start[1] };
            int buffer;

            while (true)
            {
                buffer = MazeAnswer[now[0]][now[1]];
                MazeAnswer[now[0]][now[1]] = 3;

                if (buffer == 7) {

                    now[0] -= 1;


                }

                if (buffer == 6)
                {

                    now[0] += 1;


                }

                if (buffer == 5)
                {

                    now[1] -= 1;


                }

                if (buffer == 4)
                {

                    now[1] += 1;


                }

                draw(start, goal, height, width, MazeAnswer);
                Thread.Sleep(5);
                if (now[0] == goal[0] && now[1] == goal[1])
                {
                    break;

                }

            }



        }

    }
}
