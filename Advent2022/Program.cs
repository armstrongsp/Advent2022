using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;

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
            //Day5();
            //Day6();
            //Day7();
            //Day8();
            //Day9();
            Day10();
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

        #region "Day 6"
        private static void Day6()
        {
            int PartA = 0;
            int PartB = 0;
            string lineText = File.ReadAllText(AppContext.BaseDirectory + "day6.txt");

            int pos = 0;
            while (pos < lineText.Length - 14)
            {
                if (!Day6_CheckForDups(ref lineText, pos, 4) && PartA == 0) { PartA = pos + 4; }
                if (!Day6_CheckForDups(ref lineText, pos, 14) && PartB == 0) { PartB = pos + 14; }

                pos++;
            }
           
            Console.WriteLine("Part A = " + PartA);
            Console.WriteLine("Part B = " + PartB);
        }

        private static bool Day6_CheckForDups(ref string lineText, int startPos, int lengthToCheck)
        {
            for (int i = 1; i <= (lengthToCheck - 1); i++)
            {
                if (lineText.Substring(startPos + i, lengthToCheck - i).Contains(lineText[startPos + (i - 1)]))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region "Day 7"
        private static void Day7()
        {
            Dictionary<string, int> fileSizes = Day6_LoadData();
            Dictionary<string, int> totalFolderSizes = new Dictionary<string, int>();

            //Part A
            int TotalPartA = 0;
            foreach (string folder in fileSizes.Keys)
            {
                int totalFolderSize = 0;
                foreach (string searchKey in fileSizes.Keys)
                {
                    if (searchKey.Contains(folder)) { totalFolderSize += fileSizes[searchKey]; }
                }

                totalFolderSizes.Add(folder, totalFolderSize);
                if (totalFolderSize <= 100000) TotalPartA += totalFolderSize;
            }

            //Part B
            int spaceNeeded = 30000000 - (70000000 - fileSizes.Values.Sum());
            int TotalPartB = int.MaxValue;
            foreach (string folder in totalFolderSizes.Keys)
            {
                if (totalFolderSizes[folder] > spaceNeeded && totalFolderSizes[folder] < TotalPartB) {
                    TotalPartB = totalFolderSizes[folder]; 
                }
            }

            Console.WriteLine("Total Part A: " + TotalPartA);
            Console.WriteLine("Total Part B: " + TotalPartB);
        }

        private static Dictionary<string, int> Day6_LoadData()
        {
            Dictionary<string, int> fileSizes = new Dictionary<string, int>();
            string currentPath = "";

            string lineText;
            string[] lineParts;
            int fileSize;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day7.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    lineParts = lineText.Split(" ");
                    if (lineParts[0] == "$")
                    {
                        Day6_ParseCommand(lineParts, ref currentPath);
                    }
                    else if (int.TryParse(lineParts[0], out fileSize))
                    {
                        if (fileSizes.ContainsKey(currentPath))
                        {
                            fileSizes[currentPath] += fileSize;
                        }
                        else
                        {
                            fileSizes.Add(currentPath, fileSize);
                        }
                    }
                    else if (lineParts[0] == "dir")
                    {
                        fileSizes.Add(currentPath + " " + lineParts[1], 0);
                    }

                    lineText = data.ReadLine();
                }
            }

            return fileSizes;
        }

        private static void Day6_ParseCommand(string[] lineParts, ref string currentPath)
        {
            if (lineParts[1] == "ls")
            {
                return;
            }
            else if (lineParts[1] == "cd" && lineParts[2] == "..")
            {
                currentPath = currentPath.Substring(0, currentPath.LastIndexOf(' '));
            }
            else if (lineParts[1] == "cd" && lineParts[2] == "/")
            {
                currentPath = " /";
            }
            else if (lineParts[1] == "cd")
            {
                currentPath += " " + lineParts[2];
            }
        }
        #endregion

        #region "Day 8"
        private static void Day8()
        {
            int[,] treeData = Day8_Load();

            int TotalPartA = 0;
            for (int x = 0; x < 99; x++)
            {
                for (int y = 0; y < 99; y++)
                {
                    TotalPartA += (Day8_TreeVisible(ref treeData, x, y) ? 1 : 0);
                }
            }

            int TotalPartB = 0;
            for (int x = 0; x < 99; x++)
            {
                for (int y = 0; y < 99; y++)
                {
                    int treeScore = Day8_TreeScenicScore(ref treeData, x, y);
                    if (treeScore > TotalPartB)
                    {
                        TotalPartB = treeScore;
                        Console.WriteLine(x + "," + y + "=" + treeScore);
                    }
                }
            }

            Console.WriteLine("Part A Total = " + TotalPartA); 
            Console.WriteLine("Part B Total = " + TotalPartB); 
        }

        private static int[,] Day8_Load()
        {
            int[,] treeData = new int[99, 99];
            string lineText;
            int row = 0;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day8.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    for (int i = 0; i < lineText.Length; i++)
                    {
                        treeData[row, i] = int.Parse(lineText.Substring(i, 1));
                    }

                    lineText = data.ReadLine();
                    row++;
                }
            }

            return treeData;
        }

        private static bool Day8_TreeVisible(ref int[,] treeData, int x, int y)
        {
            if (Day8_TreeVisibleDir(ref treeData, x, y, -1, 0)) return true;
            if (Day8_TreeVisibleDir(ref treeData, x, y, 1, 0)) return true;
            if (Day8_TreeVisibleDir(ref treeData, x, y, 0, -1)) return true;
            if (Day8_TreeVisibleDir(ref treeData, x, y, 0, 1)) return true;
            return false;
        }

        private static bool Day8_TreeVisibleDir(ref int[,] treeData, int x, int y, int xOffset, int yOffset)
        {
            if (x == 0 || x == 98 || y == 0 || y == 98) return true;
            int curX = x; int curY = y;
            while (true)
            {
                curX += xOffset;
                curY += yOffset;
                if (treeData[curX, curY] >= treeData[x, y]) { return false; }
                if (curX == 0 || curX == 98 || curY == 0 || curY == 98) { return true; }
            }
        }

        private static int Day8_TreeScenicScore(ref int[,] treeData, int x, int y)
        {
            int score = 1;
            score *= Day8_TreeScenicScoreDir(ref treeData, x, y, -1, 0);
            score *= Day8_TreeScenicScoreDir(ref treeData, x, y, 1, 0);
            score *= Day8_TreeScenicScoreDir(ref treeData, x, y, 0, -1);
            score *= Day8_TreeScenicScoreDir(ref treeData, x, y, 0, 1);
            return score;
        }

        private static int Day8_TreeScenicScoreDir(ref int[,] treeData, int x, int y, int xOffset, int yOffset)
        {
            if (x == 0 || x == 98 || y == 0 || y == 98) return 1;
            int curX = x; int curY = y;
            int score = 0;
            while (true)
            {
                score++;
                curX += xOffset;
                curY += yOffset;
                if (treeData[curX, curY] >= treeData[x, y]) { break; }
                if (curX == 0 || curX == 98 || curY == 0 || curY == 98) { break; }
            }
            return score;
        }
        #endregion

        #region "Day 9"
        private static void Day9()
        {
            Point[] R = new Point[11];
            List<string> posTraveledA = new List<string>();
            List<string> posTraveledB = new List<string>();

            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day9.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    Day9_Move(ref R, lineText.Split(" ")[0], int.Parse(lineText.Split(" ")[1]), ref posTraveledA, ref posTraveledB);
                    lineText = data.ReadLine();
                }
            }

            Console.WriteLine(posTraveledA.Distinct().Count());
            Console.WriteLine(posTraveledB.Distinct().Count()); 
        }

        private static void Day9_Move(ref Point[] R, string dir, int spaces, ref List<string> tail1Pos, ref List<string> tail9Pos)
        {
            int xMove = 0; int yMove = 0;
            switch (dir)
            {
                case "R": xMove = 1; break;
                case "L": xMove = -1; break;
                case "U": yMove = 1; break;
                case "D": yMove = -1; break;
            }

            while (spaces > 0)
            {
                R[0].X += xMove;
                R[0].Y += yMove;
                spaces--;
                
                //Tail following
                for (int i = 1; i < 10; i++)
                {
                    int xOffset = R[i - 1].X - R[i].X;
                    int yOffset = R[i - 1].Y - R[i].Y;

                    if (Math.Abs(xOffset) > 1 && yOffset == 0) { R[i].X += (xOffset < 0 ? -1 : 1); }
                    else if (xOffset == 0 && Math.Abs(yOffset) > 1) { R[i].Y += (yOffset < 0 ? -1 : 1); }
                    else if (Math.Sqrt(Math.Pow(xOffset, 2) + Math.Pow(yOffset, 2)) > 1.5)
                    {
                        R[i].X += (xOffset < 0 ? -1 : 1);
                        R[i].Y += (yOffset < 0 ? -1 : 1);
                    }

                    if (i == 1) tail1Pos.Add($"{R[i].X},{R[i].Y}");
                    if (i == 9) tail9Pos.Add($"{R[i].X},{R[i].Y}");
                }
            }
        }
        #endregion 

        private static void Day10()
        {
            int register = 1;
            int tick = 1;
            int TotalPartA = 0;
            string PartBImage = "";

            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day10.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    Day10_DoTickLogic(register, tick, ref TotalPartA, ref PartBImage);
                    tick++;

                    if (lineText != "noop")
                    { 
                        Day10_DoTickLogic(register, tick, ref TotalPartA, ref PartBImage);
                        tick++;
                        register += int.Parse(lineText.Split(" ")[1]);
                    }

                    lineText = data.ReadLine();
                }
            }

            Console.WriteLine("Total part A = " + TotalPartA);
            Console.WriteLine("Part B \r\n" + PartBImage);

        }

        private static void Day10_DoTickLogic(int register, int tick, ref int PartAValue, ref string CRTOutput)
        {
            if (tick == 20 || (tick - 20) % 40 == 0)
            {
                PartAValue += (tick * register);
            }

            tick--; //Image is 0 based for col
            int col = tick % 40;
            if (col == 0) { CRTOutput += "\r\n"; }
            CRTOutput += ((register - 1) <= col && (register + 1) >= col ? "#" : ".");
        }
    }
}
