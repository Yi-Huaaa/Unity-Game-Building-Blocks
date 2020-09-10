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
            int[,,] bb = new int[25, 3, 2]
            {{ { 0, 0}, { 0, 0}, { 0, 0} },//no.0 block
             { {-1, 0}, {-2, 0}, {-3, 0} },//no.1 block
             { {-1,-1}, {-2,-2}, {-3,-3} },
             { { 0, 1}, { 0, 2}, { 0, 3} },
             { {-1, 0}, {-1,-1}, {-1,-2} },
             { { 0, 1}, { 1, 2}, { 0, 2} },
             { { 0, 2}, { 0, 1}, {-1, 0} },//6
             { { 1, 2}, { 0, 1}, {-1, 0} },
             { { 0, 1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, { 1, 1} },
             { { 0,-1}, {-1, 0}, {-1,-1} },
             { { 0, 1}, {-1, 0}, {-2, 0} },//11
             { { 0,-1}, {-1, 1}, {-2, 0} },
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
            
            for (int i = 0; i <= 8; i++)
                for (int j = 0; j <= 8; j++)
                    board[i, j] = 0;
            Console.WriteLine( board );
            //3 type blocks on screen
            int[] typ = new int[3];
            Random rd = new Random();
            //for user's part
            int x=0, y=0, which=0;
            //random printf blocks, r 3 blocks on screen 
            for (int i = 0; i < 3; i++)
                typ[i] = rd.Next(0, 25);
            bool legal;
            while (true)
            {
                legal = false;
               
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
                        if (i == 3 && Check(x, y) && board[x,y] ==0 )
                            legal = true;
                        // first part, run z unmarked blocks if legal or not 
                        // as i = 0,1,2 check whether legal
                        // if  legal continue, if not break
                        else if (which > 2 || !Check(x + bb[typ[which], i, 0], y + bb[typ[which], i, 1]) || board[x + bb[typ[which], i, 1],y + bb[typ[which], i, 1]] == 1)
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
                //傳入新積木
                typ[which] = rd.Next(0, 25);


            }
        }  

        void print(int[,] board)
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
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 37; i++)
                {
                    Console.Write(plate[i, j]);
                }
                Console.Write("\n");
            }
            for(int i=0;i <= 8; i++)                                                  //PRINT輸入的值
            {
                for(int j = 0; j <= 8; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if (j <= 4)
                        {
                            plate[(10 - 2 * j)+4*i, 2 * j] = '#';
                        }
                        else if (j > 4)
                        {
                            plate[(10 - 2 * (j - 4)) + 4 * i, 2 * j] = '#';
                        }
                    }
                }
            }
        }  
    }
}
