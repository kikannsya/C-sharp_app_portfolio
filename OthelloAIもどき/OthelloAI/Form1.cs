using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloAI
{
    public partial class Form1 : Form
    {
        //変数定義
        static int wth = 8; //盤面の幅
        static int hei = 8; //盤面の高さ
        static int grid = 50;//盤面のマスの大きさ
        static int size = 40;//盤面の駒の大きさ
        Pen pB = new Pen(Color.Black, 2);　//盤面の線の太さと色
        bool turn = true;　// 手番　true 黒　false 白

        //静的評価関数
        int[,] score = new int[,] { { 120, -20, 20,  5,  5, 20, -20, 120},
                                     { -20, -40, -5, -5, -5, -5, -40, -20},
                                     {  20,  -5, 15,  3,  3, 15,  -5, -20},
                                     {   5,  -5,  3,  3,  3,  3,  -5,   5},
                                     {   5,  -5,  3,  3,  3,  3,  -5,   5},
                                     {  20,  -5, 15,  3,  3, 15,  -5, -20},
                                     { -20, -40, -5, -5, -5, -5, -40, -20},
                                     { 120, -20, 20,  5,  5, 20, -20, 120}};
        //盤面
        int[,] map = new int[wth, hei];
        //次指せる手
        List<int[]> choice = new List<int[]>();

        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            //初期化
            InitializeComponent();
            Reset();
        }
        public void show()      //表示関数
        {
            //bitmapセットアップ
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
            g.Clear(Color.Green);

            //表示座標変数
            int x, y;

            //線の描画
            for (int i = 0; i < wth; i++)
                g.DrawLine(pB, 0, i * grid, 400, i * grid);
            for (int i = 0; i < hei; i++)
                g.DrawLine(pB, i * grid, 0, i * grid, 400);


            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {
                    //表示座標設定
                    x = i * grid + (grid - size) / 2;
                    y = j * grid + (grid - size) / 2;

                    //mapの値で表示を変える
                    switch (map[i, j])
                    {
                        case 1:
                            g.FillEllipse(Brushes.Black, x, y, size, size);
                            break;
                        case 2:
                            g.FillEllipse(Brushes.White, x, y, size, size);
                            break;
                    }
                }
            }

            //表示する列の文字を設定
            string s = "";
            //試合が終了したかのフラグ
            bool flg = true;

            //終了判定を実行し返り値によって表示する文字切り替え
            switch (checkEnd())
            {
                case 1:
                    s = "黒の勝利";
                    flg = false;
                    break;
                case 2:
                    s = "白の勝利";
                    flg = false;
                    break;

                case 3:
                    s = "引き分け";
                    flg = false;
                    break;
                //パスの場合もう一度checkEnd()を実行して候補手更新
                case 20:
                    checkEnd();
                    break;
                case 10:
                    checkEnd();
                    break;

            }

            if (turn && flg)
                s = "手番: 黒";
            else if (!turn && flg)
                s = "手番: 白";

            //文字の表示
            label21.Text = s;
            //指手候補のリスト
            listBox1.Items.Clear();

            //指し手候補の表示
            for (int i = 0; i < choice.Count; i++)
            {

                x = choice[i][0] * grid + (grid - 10) / 2;
                y = choice[i][1] * grid + (grid - 10) / 2;

                if (turn)
                    g.FillEllipse(Brushes.Black, x, y, 10, 10);
                else
                    g.FillEllipse(Brushes.White, x, y, 10, 10);

                listBox1.Items.Add((char)(65 + choice[i][0]) + (choice[i][1] + 1).ToString() + "         " + score[choice[i][0], choice[i][1]].ToString());
            }


            pictureBox1.Refresh();

        }

        public void Reset()　//初期化
        {
            //手番の初期化
            turn = true;

            //盤面配置の初期化
            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {

                    map[i, j] = 999;

                }
            }

            map[3, 3] = 2;
            map[4, 4] = 2;
            map[3, 4] = 1;
            map[4, 3] = 1;

            show();

        }
        private bool turnOver(bool tn, int[,] board, int idx, int idy, bool check) // オセロの判定関数
        {
            //tn 手番がどちらか
            //board 判定するmap配列コピーすること推奨
            //idx,idy 判定する座標
            //check 実際にひっくり返す場合false 置けるかの確認はtrue
            //ひっくり返せるかどうかの返り値
            bool rtn = false;
            //1回のループでリスト内の候補をひっくり返すかどうかの判定用フラグ　もし自分の駒で挟まれていたらひっくり返す
            bool tO;
            //ひっくり返す駒の候補
            List<int[]> Stones = new List<int[]>();

            //判定する次のマス
            int x, y;

            //配列外の引数が入力された場合falseを返す
            if (!((0 <= idx && idx < wth) && (0 <= idy && idy < hei)))
                return false;
            //すでに置かれている場合もfalseを返す
            if (board[idx, idy] != 999)
                return false;

            //周りのマスを判定
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    //ひっくり返す候補を初期化
                    Stones.Clear();
                    tO = false;

                    //同じマスの場合飛ばす
                    if (i == 0 && j == 0)
                        continue;

                    //次に判定するマス
                    x = idx + i;
                    y = idy + j;

                    //盤面外なら飛ばす
                    if (!((0 <= x && x < wth) && (0 <= y && y < hei)))
                        continue;


                    //手番と異なる駒の限りループを繰り返す
                    while (((tn && board[x, y] == 2) || (!tn && board[x, y] == 1)))
                    {
                        //ひっくり返す候補に追加
                        Stones.Add(new int[] { x, y });
                        //次のマスに注目
                        x += i;
                        y += j;

                        //範囲外もしくは駒がない場合ループを抜ける
                        if (!((0 <= x && x < wth) && (0 <= y && y < hei)) || board[x, y] == 999)
                            break;

                        //手番と同じ駒が来た場合ひっくり返せるフラグを設定してループを抜ける
                        if (((tn && board[x, y] == 1) || (!tn && board[x, y] == 2)))
                        {
                            tO = true;
                            rtn = true;
                            break;
                        }


                    }
                    //引数によってひっくり返す判定がされている場合実際に与えられた配列を書き換える
                    if (tO && !check)
                    {
                        while (Stones.Count != 0)
                        {
                            if (turn)
                                board[Stones.Last()[0], Stones.Last()[1]] = 1;

                            else
                                board[Stones.Last()[0], Stones.Last()[1]] = 2;

                            Stones.RemoveAt(Stones.Count - 1);

                        }
                    }

                }

            }

            return rtn;
        }

        private int checkEnd()
        {   //終了判定


            //カウント変数の設定
            int countBlack = 0;
            int countWhite = 0;
            int countBlackToPlace = 0;
            int countWhiteToPlace = 0;
            //候補手の初期化
            choice.Clear();

            bool black = true;
            bool white = true;

            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {

                    switch (map[i, j])
                    {
                        case 1:
                            countBlack++;

                            if (black)
                                black = false;
                            break;

                        case 2:

                            countWhite++;

                            if (white)
                                white = false;
                            break;

                    }

                    //i,jに黒が置けるかどうかの判定
                    if (turnOver(true, map, i, j, true))
                    {
                        countBlackToPlace++;
                        //黒の手番の場合候補手に追加
                        if (turn)
                            choice.Add(new int[2] { i, j });
                    }

                    //i,jに白が置けるかどうかの判定
                    if (turnOver(false, map, i, j, true))
                    {
                        countWhiteToPlace++;
                        //白の手番の場合候補手に追加
                        if (!turn)
                            choice.Add(new int[2] { i, j });
                    }

                }

            }


            //勝利判定
            if (countWhite == 0)
                return 1;
            if (countBlack == 0)
                return 2;

            if (countWhite + countBlack == 64 || (countBlackToPlace == 0 && countWhiteToPlace == 0))
                if (countBlack > countWhite)
                    return 1;
                else if (countWhite > countBlack)
                    return 2;
                else if (countBlack == countWhite)
                    return 3;

            //パス判定
            if (countBlackToPlace == 0)
            {
                turn = false;
                return 10;
            }


            if (countWhiteToPlace == 0)
            {
                turn = true;
                return 20;
            }

            //終了しない場合-1を返す
            return -1;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            bool LeftClick = MouseButtons.None != (e.Button & MouseButtons.Left);

            var getXY = e.Location;
            //クリックされたマスの計算
            int idx = (int)getXY.X * wth / pictureBox1.Width;
            int idy = (int)getXY.Y * hei / pictureBox1.Height;

            //空白マスがクリックされた場合
            if (LeftClick && map[idx, idy] == 999)
            {
                //ひっくり返す判定をしてtrueの場合そのマスにも自分の駒を置きturnを相手に渡す
                if (turnOver(turn, map, idx, idy, false))
                    if (turn)
                    {
                        map[idx, idy] = 1;
                        turn = false;
                    }
                    else
                    {
                        map[idx, idy] = 2;
                        turn = true;
                    }
            }

            //AIが使用された場合AIの手番の場合にスキップでないなら実行

            if (checkBox1.Checked && turn && checkEnd() != 10)
            {
                //AI の　最善手
                var v = alphaBeta(turn, 0, map, new int[2] { -1, -1 });
                //ひっくり返す判定
                turnOver(turn, map, v[0], v[1], false);
                //候補手が存在しない場合 -1,-1が帰ってくるのでそうでない場合指す
                if (v[0] != -1 && v[1] != -1)
                    map[v[0], v[1]] = 1;
                //手番の切り替え
                turn = false;

            }

            //白の手番においても同様
            if (checkBox2.Checked && !turn && checkEnd() != 20)
            {
                var v = alphaBeta(turn, 0, map, new int[2] { -1, -1 });
                turnOver(turn, map, v[0], v[1], false);
                if (v[0] != -1 && v[1] != -1)
                    map[v[0], v[1]] = 2;
                turn = true;

            }

            show();
        }

        public int[] alphaBeta(bool tn, int d, int[,] board, int[] Place)
        {

            //tu 手番
            //d 深さ
            //盤面
            //返り値の手

            //深さが設定と同じならその手を返す
            if (d == int.Parse(comboBox1.Text))
                return Place;

            int alpha = -9999;
            int beta = 9999;

            //boardの非参照コピー先
            int[,] parameter = new int[wth, hei];
    
            //次の候補手
            List<int[]> next = new List<int[]>();

            //候補手探索
            for (int i = 0; i < wth; i++)
            {
                for (int j = 0; j < hei; j++)
                {

                    if (turnOver(tn, board, i, j, true) && board[i, j] == 999)
                    {
                        next.Add(new int[2] { i, j });
                    }
                }
            }

            //候補手が存在しない場合そのままの手を返す
            if (next.Count == 0)
                return Place;

            //最善手のインデックス
            int bestChoice = 0;

            for (int i = 0; i < next.Count; i++)
            {
                //非参照コピーをしてひっくり返して見る
                Array.Copy(board, parameter, board.Length);
                turnOver(tn, parameter, next[i][0], next[i][1], false);
                //ひっくり返した盤面における最善手探索
                var v = alphaBeta(!tn, d + 1, parameter, next[i]);

                //alphaBeta アルゴリズム
                if (d % 2 == 0 && (score[v[0], v[1]] > alpha))
                {
                    alpha = score[v[0], v[1]];
                    bestChoice = i;

                }
                else if (d % 2 == 1 && (score[v[0], v[1]] < beta))
                {
                    beta = score[v[0], v[1]];
                    bestChoice = i;

                }

            }

            //最善手を返す
            return next[bestChoice];


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (turn && checkBox1.Checked && checkEnd() != 10)
            {
                var v = alphaBeta(turn, 0, map, new int[2] { -1, -1 });
                turnOver(turn, map, v[0], v[1], false);
                if (v[0] != -1 && v[1] != -1)
                    map[v[0], v[1]] = 1;
                turn = false;
                show();
            }


        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (!turn && checkBox2.Checked && checkEnd() != 20)
            {

                var v = alphaBeta(turn, 0, map, new int[2] { -1, -1 });
                turnOver(turn, map, v[0], v[1], false);
                if (v[0] != -1 && v[1] != -1)
                    map[v[0], v[1]] = 2;
                turn = true;
                show();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
        }

    }
}

