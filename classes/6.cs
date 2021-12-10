using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_6
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/6_example.txt");
            string[] line = lines[0].Split(",");
            List<int> timers = new List<int>();

            for (int i=0 ; i<line.Length ; i++)
            {
                timers.Add(Convert.ToInt32(line[i]));
                // Console.WriteLine(timers[i]);
            }

            for (int i=0 ; i<80 ; i++)
            {
                int length = timers.Count;

                for (int j=0 ; j<length ; j++)
                {
                    //check if any anglers need resetting
                    if (timers[j]==0)
                    {
                        timers.Add(8);
                        timers[j] = 6;
                    }
                    else
                    {
                        timers[j] -= 1;
                    }
                }
            }

            Console.WriteLine(timers.Count);
        }

        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/6.txt");
            string[] line = lines[0].Split(",");
            int start_anglers = line.Length;

            // int[] sum_children_previous - new int
            long[] parents = new long[257];
            for (int i=0; i<line.Length; i++)
            {
                int counter = 0;
                while (true)
                {
                    int index = Convert.ToInt32(line[i])+1+(counter*7);
                    if (index > 256)
                    {
                        break;
                    }
                    parents[Convert.ToInt32(line[i])+1+(counter*7)] += 1;
                    counter+=1;
                }
            }

            long total = line.Length;

            for (int i=0; i<=256 ; i++)
            {
                long number = parents[i];
                if (number==0)
                {
                    continue;
                }
                int index = i + 9;
                while (true)
                {
                    if (index > 256)
                    {
                        break;
                    }
                    else
                    {
                        parents[index]+=number;
                        index+=7;
                    }
                }
                total+=parents[i];
            }

            Console.WriteLine(total);

        }
    }
}