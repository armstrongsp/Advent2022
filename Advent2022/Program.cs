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
            Day2();
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
    }
}
