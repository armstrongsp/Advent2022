using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.Numerics;

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
            //Day10();
            //Day11();
            //Day12();
            //Day13();
            //Day14();
            Day15();
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
                        switch (them)
                        {
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
                    for (int i = 0; i < partA.Length; i++)
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
                    if ((Amin >= Bmin && Amax <= Bmax) || (Bmin >= Amin && Bmax <= Amax))
                    {
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
            for (int x = 0; x < 9; x++)
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
            for (int c = 0; c < 9; c++)
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
                if (totalFolderSizes[folder] > spaceNeeded && totalFolderSizes[folder] < TotalPartB)
                {
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

        #region "Day 10"
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
        #endregion

        #region "Day 11"
        private static void Day11()
        {
            //Part A
            List<Day11_Monkey> monkeys = Day11_Load();

            for(int r = 1; r <= 20; r++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    Day11_ProcessMonkey(ref monkeys, m, 3);
                }
            }
            List<Day11_Monkey> topViewed = monkeys.OrderByDescending(m => m.ItemsViewed).Take(2).ToList();
            Console.WriteLine("Part A Total = " + (topViewed[0].ItemsViewed * topViewed[1].ItemsViewed));

            //Part B
            monkeys = Day11_Load();

            for (int r = 1; r <= 10000; r++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    Day11_ProcessMonkey(ref monkeys, m, 1);
                }
            }
            topViewed = monkeys.OrderByDescending(m => m.ItemsViewed).Take(2).ToList();
            Console.WriteLine("Part B Total = " + (topViewed[0].ItemsViewed * topViewed[1].ItemsViewed));
        }

        private static List<Day11_Monkey> Day11_Load()
        {
            List<Day11_Monkey> monkeys = new List<Day11_Monkey>();
            string[] fileText = new StreamReader(AppContext.BaseDirectory + "day11.txt").ReadToEnd().Split("\r\n");
            for(int fl = 0; fl < fileText.Length; fl += 7)
            {
                Day11_Monkey tmpMonkey = new Day11_Monkey();
                tmpMonkey.Items = new List<long>();

                string[] itemsText = fileText[fl + 1].Split(":")[1].Split(",");
                for (int i = 0; i < itemsText.Length; i++)
                {
                    tmpMonkey.Items.Add(int.Parse(itemsText[i]));
                }

                tmpMonkey.Opperation = fileText[fl + 2].Split("=")[1].Trim().Split(" ")[1];
                tmpMonkey.OpperationVal = fileText[fl + 2].Split("=")[1].Trim().Split(" ")[2];
                tmpMonkey.DivisBy = int.Parse(fileText[fl + 3].Split("by")[1]);
                tmpMonkey.TrueDest = int.Parse(fileText[fl + 4].Split("monkey")[1]);
                tmpMonkey.FalseDest = int.Parse(fileText[fl + 5].Split("monkey")[1]);

                monkeys.Add(tmpMonkey);
            }

            return monkeys;  
        }

        private static void Day11_ProcessMonkey(ref List<Day11_Monkey> monkeys, int monkeyIndex, int worryDecrement)
        {
            while(monkeys[monkeyIndex].Items.Count > 0)
            {
                monkeys[monkeyIndex].ItemsViewed++;

                long itemVal = monkeys[monkeyIndex].Items[0] % 9699690;
                //9699690 explained: All possible "divisible by" numbered multipled together. This will keep the number below the long max value, but keep it's mod value the same
                // if we dont do this the itemVal will be way to big to hold in any kind of variable. 
                long OpVal = (monkeys[monkeyIndex].OpperationVal == "old" ? itemVal : int.Parse(monkeys[monkeyIndex].OpperationVal));
                if (monkeys[monkeyIndex].Opperation == "+") { itemVal += OpVal; }
                else if (monkeys[monkeyIndex].Opperation == "*") { itemVal *= OpVal; }

                itemVal /= worryDecrement;

                bool DivisibleBy = ((itemVal % monkeys[monkeyIndex].DivisBy) == 0);
                int destMonkeyIndex = (DivisibleBy ? monkeys[monkeyIndex].TrueDest : monkeys[monkeyIndex].FalseDest);
                monkeys[destMonkeyIndex].Items.Add(itemVal);

                monkeys[monkeyIndex].Items.RemoveAt(0);
            }
        }
        #endregion

        #region "Day 12"
        private static void Day12()
        {
            Point start = new Point();
            Point end = new Point();
            int[,] elevations = Day12_Load(ref start, ref end);
            int[,] distances = Day12_Path(elevations, start, end);
            Console.WriteLine("Path A Length " + distances[end.X, end.Y]);

            int minWalk = int.MaxValue;
            for(int x = 0; x < elevations.GetLength(0) - 1; x++)
            {
                for (int y = 0; y < elevations.GetLength(1) - 1; y++)
                {
                    if (elevations[x, y] == 0)
                    {
                        distances = Day12_Path(elevations, new Point(x, y), end);
                        if (distances[end.X, end.Y] < minWalk) { minWalk = distances[end.X, end.Y]; }
                    }
                }
            }
            Console.WriteLine("Path B Length " + minWalk);
        }

        private static int[,] Day12_Load(ref Point startPos, ref Point endPos)
        { 
            int[,] elevations = new int[66, 41];
            string lineText; 
            int y = 0;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day12.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    for(int x = 0; x < lineText.Length; x++)
                    {
                        elevations[x, y] = lineText[x] - 97;
                        if (lineText[x] == 'S')
                        {
                            elevations[x, y] = 0;
                            startPos = new Point(x, y);
                        }
                        else if (lineText[x] == 'E')
                        {
                            elevations[x, y] = 25;
                            endPos = new Point(x, y);
                        }
                    }

                    y++;
                    lineText = data.ReadLine();
                }
            }

            return elevations;
        }

        private static int[,] Day12_Path(int[,] elevations, Point start, Point dest)
        {
            int[,] distances = new int[elevations.GetLength(0), elevations.GetLength(1)];
            bool[,] seen = new bool[elevations.GetLength(0), elevations.GetLength(1)];
            for (int x = 0; x < distances.GetLength(0); x++)
            {
                for (int y = 0; y < distances.GetLength(1); y++)
                {
                    distances[x, y] = int.MaxValue;
                    seen[x, y] = false;
                }
            }
            distances[start.X, start.Y] = 0;

            Point curPos = new Point(start.X, start.Y);
            while (curPos != dest)
            {
                int dist = distances[curPos.X, curPos.Y] + 1;
                if (curPos.X > 0 && !seen[curPos.X - 1, curPos.Y] && elevations[curPos.X - 1, curPos.Y] - elevations[curPos.X, curPos.Y] <= 1)
                {
                    if (dist < distances[curPos.X - 1, curPos.Y]) { distances[curPos.X - 1, curPos.Y] = dist; }
                }
                if (curPos.X < distances.GetLength(0) - 1 && !seen[curPos.X + 1, curPos.Y] && elevations[curPos.X + 1, curPos.Y] - elevations[curPos.X, curPos.Y] <= 1)
                {
                    if (dist < distances[curPos.X + 1, curPos.Y]) { distances[curPos.X + 1, curPos.Y] = dist; }
                }
                if (curPos.Y > 0 && !seen[curPos.X, curPos.Y - 1] && elevations[curPos.X, curPos.Y - 1] - elevations[curPos.X, curPos.Y] <= 1)
                {
                    if (dist < distances[curPos.X, curPos.Y - 1]) { distances[curPos.X, curPos.Y - 1] = dist; }
                }
                if (curPos.Y < distances.GetLength(1) - 1 && !seen[curPos.X, curPos.Y + 1] && elevations[curPos.X, curPos.Y + 1] - elevations[curPos.X, curPos.Y] <= 1)
                {
                    if (dist < distances[curPos.X, curPos.Y + 1]) { distances[curPos.X, curPos.Y + 1] = dist; }
                }
                seen[curPos.X, curPos.Y] = true;
                //Day12_DrawState(distances);

                //find smallest unseen dist
                curPos = new Point(-1, -1);
                int smallestDist = int.MaxValue;
                for (int x = 0; x < distances.GetLength(0); x++)
                {
                    for (int y = 0; y < distances.GetLength(1); y++)
                    {
                        if (!seen[x, y] && distances[x, y] < smallestDist)
                        {
                            smallestDist = distances[x, y];
                            curPos = new Point(x, y);
                        }
                    }
                }
                if (curPos.X == -1 && curPos.Y == -1)
                {
                    //Day12_DrawState(distances);
                    return distances;
                }
            }

            return distances;
        }


        private static void Day12_DrawState(int[,] values)
        {
            StringBuilder output = new StringBuilder();
            for (int y = 0; y < values.GetLength(1); y++)
            {
                for (int x = 0; x < values.GetLength(0); x++)
                {
                    if (values[x,y] == int.MaxValue)
                    {
                        output.Append(".".PadRight(4));
                    }
                    else
                    {
                        output.Append($"{values[x, y]}".PadRight(4));
                    }
                }
                output.Append("\r\n");
            }

            using (StreamWriter o = new StreamWriter("c:\\Repos\\Advent2022\\output.txt"))
            {
                o.Write(output.ToString());
            }
        }

        #endregion

        #region "Day 13"
        private static void Day13()
        {
            Day13_Data line1Data;
            Day13_Data line2Data;

            //Read file, remove empty lines
            List<string> dataText = new StreamReader(AppContext.BaseDirectory + "day13.txt").ReadToEnd().Split("\r\n").ToList();
            while(dataText.Find(m => m == "") != null) dataText.Remove("");

            //Part A
            int PartATotal = 0;
            for (int setIndex = 0; setIndex < (dataText.Count / 2); setIndex++)
            {
                line1Data = new Day13_Data(dataText[(setIndex * 2)]);
                line2Data = new Day13_Data(dataText[(setIndex * 2) + 1]);
                if (Day13_Compare(line1Data, line2Data) == Day13_ReturnState.Good) PartATotal += (setIndex + 1);
            }
            Console.WriteLine("Part A Total = " + PartATotal);


            //Part B
            dataText.Add("[[2]]");
            dataText.Add("[[6]]");
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < dataText.Count - 1; i++)
                {
                    line1Data = new Day13_Data(dataText[i]);
                    line2Data = new Day13_Data(dataText[i + 1]);
                    if (Day13_Compare(line1Data, line2Data) == Day13_ReturnState.Bad)
                    {
                        string tmp = dataText[i];
                        dataText[i] = dataText[i + 1];
                        dataText[i + 1] = tmp;
                        sorted = false;
                    }
                }
            }
            int PartBTotal = (dataText.FindIndex(m => m == "[[2]]") + 1) * (dataText.FindIndex(m => m == "[[6]]") + 1);
            Console.WriteLine("Part B Total = " + PartBTotal);
        }

        private static Day13_ReturnState Day13_Compare(Day13_Data list1, Day13_Data list2)
        {
            if (list1.number >= 0 && list2.number >= 0)
            {
                if (list1.number < list2.number) return Day13_ReturnState.Good;
                else if (list1.number == list2.number) return Day13_ReturnState.Neutral;
                else if (list1.number > list2.number) return Day13_ReturnState.Bad;
            }
            else if (list1.number >= 0 && list2.number < 0)
            {
                list1 = new Day13_Data($"[{list1.number}]");
            }
            else if (list1.number < 0 && list2.number >= 0)
            {
                list2 = new Day13_Data($"[{list2.number}]");
            }
            
            int index = 0;
            if (list1.empty && !list2.empty) return Day13_ReturnState.Good;
            if (!list1.empty && list2.empty) return Day13_ReturnState.Bad;
            if (list1.empty && list2.empty) return Day13_ReturnState.Neutral;
            while (index < Math.Max(list1.values.Count, list2.values.Count))
            {
                if (index >= list1.values.Count) return Day13_ReturnState.Good;
                if (index >= list2.values.Count) return Day13_ReturnState.Bad;

                Day13_ReturnState childTest = Day13_Compare(list1.values[index], list2.values[index]);
                if (childTest != Day13_ReturnState.Neutral) return childTest;

                index++;
            }

            return Day13_ReturnState.Neutral;
        }
        #endregion

        #region "Day 14"
        private enum Day14_Cell
        {
            Nothing = 0,
            Rock = 1,
            Sand = 2
        }

        private static void Day14()
        {
            Day14_Cell[,] cellData = Day14_Load();

            int PartATotal = 1;
            Point entryPoint = new Point(500, 0);
            Day14_DropSand(ref cellData, entryPoint);
            while (!Day14_DropSand(ref cellData, entryPoint)) { PartATotal++; }
            Console.WriteLine("Part A Total " + PartATotal);

            //Find floor for part b
            int maxY = 0;
            for (int y = 0; y < cellData.GetLength(1); y++)
            {
                for (int x = 0; x < cellData.GetLength(0); x++)
                {
                    if (cellData[x, y] != Day14_Cell.Nothing) if (y > maxY) maxY = y;
                }
            }
            maxY += 2;
            for (int x = 0; x < cellData.GetLength(0); x++)
            {
                cellData[x, maxY] = Day14_Cell.Rock;
            }

            //Part B, go until sand can't fall anymore
            int PartBTotal = PartATotal;
            while (!Day14_DropSand(ref cellData, entryPoint)) { PartBTotal++; }
            Console.WriteLine("Part B Total " + PartBTotal);
        }

        private static Day14_Cell[,] Day14_Load()
        {
            Day14_Cell[,] cellData = new Day14_Cell[1000,200];

            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day14.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    string[] pointsText = lineText.Split(" -> ");
                    Point a = new Point();
                    Point b = new Point();
                    for(int p = 0; p < pointsText.Length; p++)
                    {
                        b = new Point(int.Parse(pointsText[p].Split(",")[0]), int.Parse(pointsText[p].Split(",")[1]));
                        if (p > 0)
                        {
                            int xOffset = (a.X < b.X ? 1 : (a.X > b.X ? -1 : 0));
                            int yOffset = (a.Y < b.Y ? 1 : (a.Y > b.Y ? -1 : 0));
                            Point tmp = a;
                            cellData[tmp.X, tmp.Y] = Day14_Cell.Rock;
                            while (tmp != b)
                            {
                                tmp.X += xOffset;
                                tmp.Y += yOffset;
                                cellData[tmp.X, tmp.Y] = Day14_Cell.Rock;
                            }
                        }
                        a = b;
                    }

                    lineText = data.ReadLine();
                }
            }

            return cellData;
        }

        private static void Day14_Draw(Day14_Cell[,] cellData)
        {
            StringBuilder outImage = new StringBuilder();
            for(int y = 0; y < cellData.GetLength(1); y++)
            {
                for (int x = 0; x < cellData.GetLength(0); x++)
                {
                    if (cellData[x, y] == Day14_Cell.Rock) outImage.Append("X");
                    else if (cellData[x, y] == Day14_Cell.Sand) outImage.Append("O"); 
                    else outImage.Append(".");
                }
                outImage.AppendLine("");
            }

            new StreamWriter("c:\\Repos\\Advent2022\\day14output.txt").WriteLine(outImage.ToString());
        }

        //returns true if the sand fell out of the world
        private static bool Day14_DropSand(ref Day14_Cell[,] cellData, Point entryPoint)
        {
            Point curSandPos = entryPoint;

            if (cellData[curSandPos.X, curSandPos.Y] == Day14_Cell.Sand) return true;

            while ((curSandPos.Y + 1) < cellData.GetLength(1))
            {
                if (cellData[curSandPos.X, curSandPos.Y + 1] == Day14_Cell.Nothing)
                {
                    curSandPos.Y++;
                }
                else if (cellData[curSandPos.X - 1, curSandPos.Y + 1] == Day14_Cell.Nothing)
                {
                    curSandPos.X--;
                    curSandPos.Y++;
                }
                else if (cellData[curSandPos.X + 1, curSandPos.Y + 1] == Day14_Cell.Nothing)
                {
                    curSandPos.X++;
                    curSandPos.Y++;
                }
                else break;
            }

            if (curSandPos.Y >= cellData.GetLength(1) - 1) return true; 
            cellData[curSandPos.X, curSandPos.Y] = Day14_Cell.Sand;
            return false;
        }
        #endregion 

        #region "Day 15"
        private static void Day15()
        {
            List<Day15_Scan> dataPoints = Day15_Load();

            int PartATotal = 0;
            for (int x = -1000000; x < 5000000; x++)
            {
                if (Day15_PointScanned(ref dataPoints, new Point(x, 2000000))) PartATotal++;
            }
            Console.WriteLine($"Total Part A = " + (PartATotal - 1)); 


            Point PartBPoint = new Point();
            foreach (Day15_Scan scan in dataPoints)
            {
                Point testPoint = new Point(scan.scanPoint.X - (scan.dist + 1), scan.scanPoint.Y);
                bool Found = Day15_ScanEdge(ref dataPoints, ref testPoint, new Point(scan.scanPoint.X, scan.scanPoint.Y - (scan.dist + 1)), 1, -1);
                if (!Found) Found = Day15_ScanEdge(ref dataPoints, ref testPoint, new Point(scan.scanPoint.X + scan.dist + 1, scan.scanPoint.Y), 1, 1);
                if (!Found) Found = Day15_ScanEdge(ref dataPoints, ref testPoint, new Point(scan.scanPoint.X, scan.scanPoint.Y + (scan.dist + 1)), -1, 1);
                if (!Found) Found = Day15_ScanEdge(ref dataPoints, ref testPoint, new Point(scan.scanPoint.X - (scan.dist + 1), scan.scanPoint.Y), -1, -1);
                if (Found && testPoint.X >= 0 && testPoint.X <= 4000000 && testPoint.Y >= 0 && testPoint.Y <= 4000000)
                {
                    PartBPoint = testPoint;
                    Console.WriteLine($"Found {testPoint.X},{testPoint.Y}");
                    break;
                }
            }
            
            Console.WriteLine($"Part B Answer = {((long)PartBPoint.X * (long)4000000) + (long)PartBPoint.Y}");
        }

        private static List<Day15_Scan> Day15_Load()
        {
            List<Day15_Scan> dataPoints = new List<Day15_Scan>();

            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "day15.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    string tmpText = lineText.Split(":")[0];
                    int x = int.Parse(tmpText.Split("=")[1].Split(",")[0]);
                    int y = int.Parse(tmpText.Split("=")[2]);
                    Point key = new Point(x, y);
                    
                    tmpText = lineText.Split(":")[1];
                    x = int.Parse(tmpText.Split("=")[1].Split(",")[0]);
                    y = int.Parse(tmpText.Split("=")[2]);
                    Point beacon = new Point(x, y);

                    dataPoints.Add(new Day15_Scan(key, beacon));

                    lineText = data.ReadLine();
                }
            }

            return dataPoints;
        }

        private static bool Day15_PointScanned(ref List<Day15_Scan> dataPoints, Point target)
        {
            foreach (Day15_Scan scan in dataPoints)
            {
                int sensorToTargetDist = Day15_Dist(scan.scanPoint, target);
                if (sensorToTargetDist <= scan.dist) return true;
            }

            return false;
        }

        private static bool Day15_ScanEdge(ref List<Day15_Scan> dataPoints, ref Point testPoint, Point destPoint, int xOff, int yOff)
        {
            int cellsTested = 0;
            while (testPoint.X != destPoint.X && testPoint.Y != destPoint.Y)
            {
                if (!Day15_PointScanned(ref dataPoints, testPoint)) return true; 
                testPoint.X += xOff;
                testPoint.Y += yOff;
                cellsTested++;
            }

            return false;
        }

        private static int Day15_Dist(Point a, Point b)
        {
            long xDist = Math.Abs(a.X - b.X);
            long yDist = Math.Abs(a.Y - b.Y);
            return (int)xDist + (int)yDist;
        }
        #endregion



        #region "Day X"
        private static void DayStub()
        {
            string lineText;
            using (StreamReader data = new StreamReader(AppContext.BaseDirectory + "dayXXX.txt"))
            {
                lineText = data.ReadLine();
                while (lineText != null)
                {
                    lineText = data.ReadLine();
                }
            }
        }
        #endregion
    }
}
