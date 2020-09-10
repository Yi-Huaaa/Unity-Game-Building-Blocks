using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using final_project___Building_Blocks;

namespace UnitTestProject1
{
    public class Program
    {
        public static void print(int[,] board,ref int counter)
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
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if (j <= 4)
                        {
                            plate[(10 - 2 * j) + 4 * i, 2 * j] = '#';
                            counter++;
                        }
                        else if (j > 4)
                        {
                            plate[(10 - 2 * (j - 4)) + 4 * i, 2 * j] = '#';
                            counter++;
                        }
                    }
                }
            }
        }
        [TestClass]
        public class UnitTest1
        {
            [TestMethod]
            public void TestMethod1()
            {
                int counter = 0;
                int[,] board = new int[9, 9];
                for(int i = 0; i <= 8; i++)
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        board[i, j] = 0;
                    }
                }
                board[1, 1] = 1;                                             //NO.1 
                board[1, 2] = 1;
                board[1, 3] = 1;
                board[1, 4] = 1;
                print(board, ref counter);
                Assert.AreEqual(counter, 4);                                 //檢查是否4格有值
            }
        }
    }
}
