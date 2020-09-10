using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace final_project___Building_Blocks
{
    class Program
    {
        //set a fuc 2 check if z cin x,y r legal
        public static bool Check(int x, int y)
        {
            //if 0 run again
            if (y - x > 4 || x - y > 4 || x < 0 || x > 8 || y < 0 || y > 8)
                return false;
            return true;
        }

        static void Main(string[] args)
        {
            //   bool start = true;
            int start;
            Console.WriteLine("\n遊戲名稱：消消樂");
            Console.WriteLine("遊戲規則：\n1.將拼圖移至棋盤後若形成完整直線或橫線抑或是大斜線即可消掉一條\n2.指定有@之積木至棋盤上之(x,y)處\n3.x,y座標均由0算起而非1算起www");
            Console.Write("start the game or not, if yes please cin 1\n");
            start = Convert.ToInt32(Console.ReadLine());
            if (start != 1)
            {
                return;
            }

            int grade=0;
            int[,,] bb = new int[25, 3, 2]
            {{ { 0, 0}, { 0, 0}, { 0, 0} },//no.0 block
             { {-1, 0}, {-2, 0}, {-3, 0} },//no.1 block
             { {-1,-1}, {-2,-2}, {-3,-3} },
             { { 0, 1}, { 0, 2}, { 0, 3} },
             { {-1, 0}, {-1,-1}, {-2,-2} },
             { { 0, 1}, { 1, 2}, { 0, 2} },
             { { 0, 2}, { 0, 1}, {-1, 0} },//6
             { { 1, 2}, { 0, 1}, {-1, 0} },
             { { 0, 1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, {-1, 1} },
             { {-2,-1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, {-2, 0} },//11
             { {-1, 0}, {-1, 1}, {-2, 0} },
             { {-1, 0}, {-2,-1}, {-2, 0} },//13
             { {-1,-1}, {-1, 0}, {-2, 0} },
             { { 0, 1}, {-1, 0}, {-2,-1} },
             { { 0, 1}, { 0, 2}, {-1, 1} },//16
             { {-1,-1}, {-1, 0}, {-1, 1} },
             { {-1,-1}, {-2,-2}, {-2,-1} },//18
             { { 0, 1}, {-1, 1}, {-2, 0} },//19
             { {-1,-1}, {-2,-1}, {-2, 0} },//20
             { {-1,-1}, { 0, 1}, {-1, 1} },//21
             { {-1, 0}, {-2,-1}, {-2,-2} },//22
             { { 0, 1}, {-1,-1}, {-2,-1} },//23
             { {-1, 0}, {-1, 1}, { 0, 2} },//24
            };
            // a 8*8 board, gif ou 1-1 num 
            int[,] board = new int[9, 9];
            int[,] copy = new int[9, 9];

            for (int i = 0; i <= 8; i++)
                for (int j = 0; j <= 8; j++)
                    board[i, j] = 0;
            Console.WriteLine(board);
            Program bp = new Program();
            bp.Print(board);
            //3 type blocks on screen
            int[] typ = new int[3];
            Random rd = new Random();
            //for user's part
            int x = 0, y = 0, which = 0;
            //random printf blocks, r 3 blocks on screen 
            for (int i = 0; i < 3; i++)
                typ[i] = rd.Next(0, 25);
            //for testing
            //typ[0] = 1;
            bool legal;
            char [,] choice = new char[29, 8];

            while (true)
            {
                legal = false;
                //print typ[which]
                for(int i = 0; i < 3; i++)
                {
                    Console.Write("this is " + i + " blocks\n\n");
                    bp.choice(typ[i]);
                }
                Console.Write("\n累積分數！！！ = " + grade +　"\n\n");
                while (!legal)
                {
                    //get x, y, which
                    Console.WriteLine("輸入你想移動的積木編號～");
                    which = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("輸入被標記之積木要被移動到的板子上之x座標");
                    x = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("輸入被標記之積木要被移動到的板子上之y座標");
                    y = Convert.ToInt32(Console.ReadLine());
                    //before key in z x,y, test for if under loop is t ot f
                    //x = y = which = 2;

                    for (int i = 0; i < 4; i++)
                    {
                        //second part , run z marked block
                        // z last oe to check, if not true legal still == false
                        //then i == 4, break z  loop
                        //if (i == 3 && Check(x, y) && board[x, y] == 0)
                        //    legal = true;
                        if ( i == 3 )
                        {
                            if (Check(x, y) && board[x, y] == 0)
                                legal = true;
                        }
                        // first part, run z unmarked blocks if legal or not 
                        // as i = 0,1,2 check whether legal
                        // if  legal continue, if not break
                        else if (which > 2 || !Check(x,y) || !Check(x + bb[typ[which], i, 0],
                            y + bb[typ[which], i, 1]) || board[x + bb[typ[which], i, 0], 
                            y + bb[typ[which], i, 1]] == 1)
                            break;
                    }
                    //cout wrong mention
                    if (!legal)
                    {
                        Console.WriteLine("\n\n\n輸入之積木不能被配置在棋盤上＞＜，請重新選擇你要移動到的位置～");
                        Console.WriteLine("\n----------我是分隔線----------");
                        Console.WriteLine("\n");
                    }
                }

                board[x, y] = 1;
                for (int i = 0; i < 3; i++)
                    board[x + bb[typ[which], i, 0], y + bb[typ[which], i, 1]] = 1;
                
                //檢查積木消掉情形
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        copy[i, j] = board[i, j];
                for(int k = 0; k < 2; k++)
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        int i;
                        if (j <= 4)
                            i = 0;
                        else
                            i = j - 4;
                        bool ok = true;
                        while (k == 0 && Check(i, j) || k == 1 && Check(j, i))
                        {
                            if (k == 0 && board[i, j] == 0 || k == 1 && board[j, i] == 0)
                            {
                                ok = false;
                                break;
                            }
                            i++;
                        }
                        if (ok)
                        {
                            if (j <= 4)
                                i = 0;
                            else
                                i = j - 4;
                            while (k == 0 && Check(i, j) || k == 1 && Check(j, i))
                            {
                                if (k == 0)
                                    copy[i, j] = 0;
                                else
                                    copy[j, i] = 0;
                                i++;
                            }
                        }
                        if(j <= 4)
                        {
                            int a = 0, b = j;
                            ok = true;
                            while(k == 0 && Check(a, b) || k == 1 && Check(b, a))
                            {
                                if (k == 0 && board[a, b] == 0 || k == 1 && board[b, a] == 0)
                                {
                                    ok = false;
                                    break;
                                }
                                a++;
                                b++;
                            }
                            if (ok)
                            {
                                a = 0;
                                b = j;
                                while (k == 0 && Check(a, b) || k == 1 && Check(b, a))
                                {
                                    if (k == 0)
                                        copy[a, b] = 0;
                                    else
                                        copy[b, a] = 0;
                                    a++;
                                    b++;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                        board[i, j] = copy[i, j];
                if (typ[which] == 0)
                    grade++;
                else
                    grade += 4;
                //傳入新積木
                typ[which] = rd.Next(0, 25);
                bp.Print(board);

            }
        }


        //學長的部分
        void Print(int[,] board)
        {
            int begin = 0;
            char[,] plate = new char[37, 20];
            for (int i = 0; i < 37; i++)                                              //設矩陣初始值為' '
            {
                for (int j = 0; j < 20; j++)
                {
                    plate[i, j] = ' ';
                }
            }
            for (int i = 0; i <= 9; i++)                                             //畫空白的底盤
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 9 - i / 2; j++)                              //從中間開始上下對稱的畫底盤，兩層格子之間夾有一層分隔線
                    {
                        plate[4 * j + begin, 10 + i] = '|';                          //4個char組成一格底盤'|',' ',' ',' '   
                        plate[4 * j + 1 + begin, 10 + i] = ' ';
                        plate[4 * j + 2 + begin, 10 + i] = ' ';
                        plate[4 * j + 3 + begin, 10 + i] = ' ';
                        if (j == 9 - i / 2 - 1)
                        {
                            plate[4 * (j + 1) + begin, 10 + i] = '|';
                            plate[4 * (j + 1) + begin, 10 - i] = '|';
                        }
                        plate[4 * j + begin, 10 - i] = '|';
                        plate[4 * j + 1 + begin, 10 - i] = ' ';
                        plate[4 * j + 2 + begin, 10 - i] = ' ';
                        plate[4 * j + 3 + begin, 10 - i] = ' ';
                    }
                }
                else if (i % 2 == 1)
                {
                    for (int j = begin; j <= 4 * (9 - (i - 1) / 2) + begin; j++)
                    {
                        plate[j, 10 + i] = '-';
                        plate[j, 10 - i] = '-';
                    }
                    begin += 2;
                }
            }
            for (int i = 0; i <= 8; i++)                     //PRINT輸入的值
                for (int j = 0; j <= 8; j++)
                    if (board[i, j] != 0)
                        plate[10 - 2 * j + 4 * i, 2 * j + 2] = '#';
            for (int j = 19; j >= 0; j--)
            {
                for (int i = 0; i < 37; i++)
                {
                    Console.Write(plate[i, j]);
                }
                Console.Write("\n");
            }
            
        }
        void choice(int t)
        {
            if (t == 0)
                Console.Write("#\n");
            else if (t == 1)
                Console.Write(" #   #   #   @ \n");
            else if (t == 2)
                Console.Write("       @\n\n     #\n\n   #\n\n #\n");
            else if (t == 3)
                Console.Write(" #\n\n   #\n\n     #\n\n       @\n");
            else if (t == 4)
                Console.Write(" #   @\n\n   #\n\n #\n");
            else if (t == 5)
                Console.Write(" #   #\n\n   #\n\n     @\n");
            else if (t == 6)
                Console.Write(" #\n\n   #\n\n #   @\n");
            else if (t == 7)
                Console.Write("     #\n\n   #\n\n #   @\n");
            else if (t == 8)
                Console.Write("   #\n\n #   @\n\n   #\n");
            else if (t == 9)
                Console.Write(" #   #\n\n   #   @\n");
            else if (t == 10)
                Console.Write("   #   @\n\n #   #\n");
            else if (t == 11)
                Console.Write("       #\n\n #   #   @\n");
            else if (t == 12)
                Console.Write("   #\n\n #   #   @\n");
            else if (t == 13)
                Console.Write(" #   #   @\n\n   #\n");
            else if (t == 14)
                Console.Write(" #   #   @\n\n       #\n");
            else if (t == 15)
                Console.Write("     #\n\n   #   @\n\n #\n");
            else if (t == 16)
                Console.Write("   #\n\n #   #\n\n       @\n");
            else if (t == 17)
                Console.Write(" #\n\n   #   @\n\n     #\n");
            else if (t == 18)
                Console.Write("       @\n\n #   #\n\n   #\n");
            else if (t == 19)
                Console.Write("   #   #\n\n #       @\n");
            else if (t == 20)
                Console.Write(" #       @\n\n   #   #\n");
            else if (t == 21)
                Console.Write(" #   #\n\n       @\n\n     #\n");
            else if (t == 22)
                Console.Write("   #   @\n\n #\n\n   #\n");
            else if (t == 23)
                Console.Write("     #\n\n       @\n\n #   #\n");
            else
                Console.Write("   #\n\n #\n\n   #   @\n");


        }
    }
}