using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MineSweeper
{
    public partial class Form1 : Form
    {

        int wth = 20;
        int hei = 10;
        double MineRate = 0.1;
        static int FontSize = 20;
        static int grid = 60;

        bool firstClick = true;
        

        int[,] map = new int[1000, 1000];
        bool[,] opned = new bool[1000, 1000];
        bool[,] flaged = new bool[1000, 1000];

        Bitmap bmp;
        Graphics g;
        Pen pB = new Pen(Color.Black, 2);
        Font fnt = new Font("MS Gothic", FontSize);

        public Form1()
        {
            InitializeComponent();

            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1_MouseDown(pictureBox1, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
            textBox2.Text = wth.ToString();
            textBox3.Text = hei.ToString();

            initializeMap();
            setMineNumber();

            show();

        }

        public void show()   // 表示関数
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);　//表示するビットマップの生成
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            double x;
            double y;
            
            for(int i=1;i < hei; i++)
                g.DrawLine(pB, 0, i * pictureBox1.Height / hei, pictureBox1.Width, i * pictureBox1.Height / hei);
            for (int i = 1; i < wth; i++)
                g.DrawLine(pB, i * pictureBox1.Width / wth, 0, i * pictureBox1.Width / wth, pictureBox1.Height);
            
            for (int i = 0; i < wth;i++)
            {
                for(int j = 0; j < hei; j++) {

                    x = i * pictureBox1.Width / wth + 4*FontSize/ wth;
                    y = j * pictureBox1.Height / hei + 2*FontSize / hei;

                    if (flaged[i, j] && !opned[i, j])
                        g.DrawString("F", fnt, Brushes.Red, (int)x, (int)y);
                    else if (map[i, j] != 999 && opned[i, j])
                        g.DrawString(map[i, j].ToString(), fnt, Brushes.Black, (int)x, (int)y);
                    else if (map[i, j] == 999 && opned[i, j])
                        g.DrawString("M", fnt, Brushes.Red, (int)x, (int)y);


                }


            }

            pictureBox1.Refresh();

        }

        public void initializeMap()
        {
            try
            {
                double.TryParse(textBox1.Text, out MineRate);
            }
            catch { MineRate = 0.1; }

            try
            {
                int.TryParse(textBox2.Text, out wth);
            }
            catch { wth = 20; }
            try
            {
                int.TryParse(textBox3.Text, out hei);
            }
            catch { hei = 10; }

            textBox1.Text = MineRate.ToString();
            textBox2.Text = wth.ToString();
            textBox3.Text = hei.ToString();

            pictureBox1.Width = grid * wth;
            pictureBox1.Height = grid * hei;

            this.Width = pictureBox1.Width + 400;
            this.Height = pictureBox1.Height + 400;


            Random r = new Random();

            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {
                    if (r.Next() % (int)(1/ MineRate) == 0)
                        map[i, j] = 999;
                    else
                        map[i, j] = 0;

                    opned[i, j] = false;
                    flaged[i, j] = false;

                }

            }

            label1.Visible = false;
            label2.Visible = false;

            firstClick = true;

            return;

        }

        public void setMineNumber()
        {
            int bombCount;
            
            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {
                    if (map[i, j] != 999)
                    {
                        bombCount = 0;

                        for(int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {

                                if ((i + k >= 0 && i + k < wth)  && (j + l >= 0 && j + l < hei))
                                {

                                    if( (k != 0 || l != 0) && map[i + k,j + l] == 999)
                                        bombCount++;

                                }

                            }
                        }

                        map[i, j] = bombCount;

                    }

                }

            }
        }

        public void setVisible(int x)
        {
            if(x == 0)
                label1.Visible = true;
            else if(x == 1)
                label2.Visible = true;
        }

        public void autoOpen(int idx, int idy) {
            for (int k = -1; k < 2; k++)
            {
                for (int l = -1; l < 2; l++)
                {
                    if ((idx + k >= 0 && idx + k < wth) && (idy + l >= 0 && idy + l < hei))
                        if ((k != 0 || l != 0) && map[idx + k, idy + l] != 999 && !opned[idx + k, idy + l])
                        {
                            opned[idx + k, idy + l] = true;
                            if (map[idx + k, idy + l] == 0)
                                autoOpen(idx + k, idy + l);

                        }
                }
            }

            show();
            return;
        }

        public void judgeGameOver() {
            bool flag = true;

            for(int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {
                    if (map[i, j] != 999 && !opned[i, j])
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag)
                setVisible(1);
   
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool LeftClick = MouseButtons.None != (e.Button & MouseButtons.Left);
            bool RightClick = MouseButtons.None != (e.Button & MouseButtons.Right);

            var getXY = e.Location;

            int idx = (int)getXY.X * wth / pictureBox1.Width;
            int idy = (int)getXY.Y * hei / pictureBox1.Height;

            if (LeftClick)
            {
                if (!opned[idx, idy] && !flaged[idx, idy])
                {
                    opned[idx, idy] = true;
                    if (map[idx, idy] == 999 )
                    {
                        if (!firstClick)
                        {
                            setVisible(0);
                            show();
                            return; // gameover
                        }
                        else
                        {

                            for(int i=0;i<wth;i++)
                            {
                                for (int j = 0; j < hei; j++)
                                {
                                    if (map[i, j] != 999)
                                    {
                                        (map[idx, idy], map[i, j]) = (map[i, j], map[idx, idy]);
                                        setMineNumber();
                                        break;
                                    }
                                }
                            }


                        }
                    }
                    if (map[idx, idy] == 0)
                        autoOpen(idx, idy);
                }

                if (!label1.Visible)
                    judgeGameOver();
               
            }else if (RightClick)
                if (!opned[idx, idy])
                    flaged[idx, idy] = !flaged[idx, idy];

            if (firstClick)
                firstClick = false;

            show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            initializeMap();
            setMineNumber();
            show();
        }
    }
}