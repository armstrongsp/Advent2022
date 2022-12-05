﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent2022
{
    class Program
    {
        static void Main(string[] args)
        {
            //Day1();
            //Day2();
            //Day3();
            //Day4();
            Day5();
        }

        private static void Day1()
        {
            List<int> calories = new List<int>();
            string lineText;
            int line;
            int total = 0;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day1.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                { 
                    if (!int.TryParse(lineText, out line))
                    {
                        calories.Add(total);
                        total = 0;
                    }
                    else
                    {
                        total += line;
                    }
                    lineText = data.ReadLine();
                }
                calories.Add(total);
            }
            calories.Sort();
            Console.WriteLine("Max = " + calories[calories.Count - 1]); 
            Console.WriteLine("2nd = " + calories[calories.Count - 2]); 
            Console.WriteLine("3rd = " + calories[calories.Count - 3]);
            Console.WriteLine("Total Backup = " + (calories[calories.Count - 1] + calories[calories.Count - 2] + calories[calories.Count - 3]));
        }

        private static void Day2()
        {
            int TotalScore_A = 0;
            int TotalScore_B = 0;
            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day2.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    int them = lineText.Split(" ")[0].ToCharArray()[0] - 64;
                    int me = lineText.Split(" ")[1].ToCharArray()[0] - 87;
                    
                    //Part A Logic
                    TotalScore_A += me;
                    if (them == 1 && me == 2 || them == 2 && me == 3 || them == 3 && me == 1)
                    {
                        TotalScore_A += 6;
                    }
                    else if (them == me)
                    {
                        TotalScore_A += 3;
                    }

                    //Part B Logic
                    if (me == 1) //lose
                    {
                        switch (them){
                            case 1: me = 3; break;
                            case 2: me = 1; break;
                            case 3: me = 2; break;
                        }
                        TotalScore_B += me;
                    }
                    else if (me == 2) //draw
                    {
                        TotalScore_B += them + 3;
                    }
                    else if (me == 3) //win
                    {
                        switch (them)
                        {
                            case 1: me = 2; break;
                            case 2: me = 3; break;
                            case 3: me = 1; break;
                        }
                        TotalScore_B += me + 6;
                    }


                    lineText = data.ReadLine();
                }
            }

            Console.WriteLine("Total Score Part A = " + TotalScore_A);
            Console.WriteLine("Total Score Part B = " + TotalScore_B);
        }

        private static void Day3()
        {
            int totalPriority = 0;
            int totalBadges = 0;
            string[] groupItems = { "", "", "" };
            int groupIndex = 0;
            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day3.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    //Part A
                    string partA = lineText.Substring(0, lineText.Length / 2);
                    string partB = lineText.Substring(lineText.Length / 2, lineText.Length / 2);
                    for(int i = 0; i < partA.Length; i++)
                    {
                        if (partB.Contains(partA[i]))
                        {
                            int tmp = partA[i] - (partA[i] >= 97 ? 96 : 38);
                            Console.WriteLine($"Dup = {partA[i]} val = {tmp}");
                            totalPriority += tmp;
                            break;
                        }
                    }

                    //Part B
                    groupItems[groupIndex] = lineText;
                    if (groupIndex == 2)
                    {
                        groupIndex = -1;
                        for (int i = 0; i < groupItems[0].Length; i++)
                        {
                            if (groupItems[1].Contains(groupItems[0][i]) && groupItems[2].Contains(groupItems[0][i]))
                            {
                                int tmp = groupItems[0][i] - (groupItems[0][i] >= 97 ? 96 : 38);
                                Console.WriteLine($"badge = {groupItems[0][i]} val = {tmp}");
                                totalBadges += tmp;
                                break;
                            }
                        }
                    }
                    groupIndex++;

                    lineText = data.ReadLine();
                }
            }

            Console.WriteLine("Total Priority nums = " + totalPriority);
            Console.WriteLine("Total Badge nums = " + totalBadges);
        }

        private static void Day4()
        {
            int TotalPartA = 0;
            int TotalPartB = 0;
            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day4.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    int Amin = int.Parse(lineText.Split(",")[0].Split("-")[0]);
                    int Amax = int.Parse(lineText.Split(",")[0].Split("-")[1]);
                    int Bmin = int.Parse(lineText.Split(",")[1].Split("-")[0]);
                    int Bmax = int.Parse(lineText.Split(",")[1].Split("-")[1]);
                    Console.Write(lineText);
                    if ((Amin >= Bmin && Amax <= Bmax) || (Bmin >= Amin && Bmax <= Amax)) {
                        Console.Write(" contains");
                        TotalPartA++; 
                    }
                    if ((Amin >= Bmin && Amin <= Bmax) || (Amax >= Bmin && Amax <= Bmax) || (Bmin >= Amin && Bmin <= Amax) || (Bmin >= Amin && Bmin <= Amax))
                    {
                        Console.Write(" overlaps");
                        TotalPartB++;
                    }
                    Console.WriteLine();

                    lineText = data.ReadLine();
                }
            }
            Console.WriteLine("Total contains = " + TotalPartA);
            Console.WriteLine("Total overlaps = " + TotalPartB);
        }

        #region "Day 5"
        private static void Day5()
        {
            char[,] boxesA = new char[9, 100];
            char[,] boxesB = new char[9, 100];
            for(int x = 0; x < 9 ; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    boxesA[x, y] = ' ';
                    boxesB[x, y] = ' ';
                }
            }
            Day5_Load(ref boxesA, ref boxesB, 7, " P    Q T");
            Day5_Load(ref boxesA, ref boxesB, 6, "FN   PL M");
            Day5_Load(ref boxesA, ref boxesB, 5, "HTH  MH Z");
            Day5_Load(ref boxesA, ref boxesB, 4, "MCP QRC J");
            Day5_Load(ref boxesA, ref boxesB, 3, "TJMFLGR Q");
            Day5_Load(ref boxesA, ref boxesB, 2, "VGDVGDNWL");
            Day5_Load(ref boxesA, ref boxesB, 1, "LQSBHBMLD");
            Day5_Load(ref boxesA, ref boxesB, 0, "DHRLNWGCR");

            Day5_Display(ref boxesB);

            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day5.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    int moveCount = int.Parse(lineText.Split(' ')[1]);
                    int moveFrom = int.Parse(lineText.Split(' ')[3]) - 1;
                    int moveTo = int.Parse(lineText.Split(' ')[5]) - 1;
                    Day5_MoveA(ref boxesA, moveCount, moveFrom, moveTo);
                    Day5_MoveB(ref boxesB, moveCount, moveFrom, moveTo);

                    lineText = data.ReadLine();
                }
            }

            Console.WriteLine("Part A Results = " + Day5_Top(ref boxesA));
            Console.WriteLine("Part B Results = " + Day5_Top(ref boxesB));
        }

        private static void Day5_Load(ref char[,] dataA, ref char[,] dataB, int line, string lineText)
        {
            for(int c = 0; c < 9; c++)
            {
                dataA[c, line] = lineText[c];
                dataB[c, line] = lineText[c];
            }
        }

        private static void Day5_MoveA(ref char[,] data, int moveCount, int moveFrom, int moveTo)
        {
            for (int c = moveCount; c > 0; c--)
            {
                //find the first box in the col
                int F = 99;
                while (F >= 0 && data[moveFrom, F] == ' ') { F--; }
                //find the first empty space in the dest
                int T = 99;
                while (T >= 0 && data[moveTo, T] == ' ') { T--; }
                T++;

                //move the box
                data[moveTo, T] = data[moveFrom, F];
                data[moveFrom, F] = ' ';
            }
        }

        private static void Day5_MoveB(ref char[,] data, int moveCount, int moveFrom, int moveTo)
        {
            //find the first box in the col
            int F = 99;
            while (F >= 0 && data[moveFrom, F] == ' ') { F--; }
            //find the first empty space in the dest
            int T = 99;
            while (T >= 0 && data[moveTo, T] == ' ') { T--; }
            T++;

            for (int c = 0; c < moveCount; c++)
            {
                //move the box
                data[moveTo, T + ((moveCount - 1) - c)] = data[moveFrom, F - c];
                data[moveFrom, F - c] = ' ';
            }
        }

        private static string Day5_Top(ref char[,] data)
        {
            string output = "";
            for (int c = 0; c < 9; c++)
            {
                //find the first box in the col
                int F = 99;
                while (F >= 0 && data[c, F] == ' ') { F--; }
                output += data[c, F];
            }
            return output;
        }

        private static void Day5_Display(ref char[,] data)
        {
            string lineOutput = "";
            for (int r = 99; r >= 0; r--)
            {
                lineOutput = "";
                for (int c = 0; c < 9; c++)
                {
                    lineOutput += data[c, r] + " ";
                }
                Console.WriteLine(lineOutput);
            }
                Console.WriteLine("1 2 3 4 5 6 7 8 9");
        }
        #endregion 
    }
}
