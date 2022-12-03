using System;
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
            Day3();
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
    }
}
