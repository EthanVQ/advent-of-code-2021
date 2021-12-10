using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_7
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/7.txt");
            string[] line = lines[0].Split(",");

            int[] positions = new int[line.Length];

            for (int i=0; i<line.Length ; i++)
            {
                positions[i] = Convert.ToInt32(line[i]);
            }
            
            // iterate through potential positions
            int min_fuel = -1;
            int min_position = 0;
            for (int i=0; i<=positions.Max(); i++)
            {
                int fuel = 0;
                foreach (int position in positions)
                {
                    fuel += Math.Abs(i-position);
                }

                if ((fuel <= min_fuel) || (min_fuel==-1))
                {
                    min_fuel = fuel;
                    min_position = i;
                }
            }

            Console.WriteLine(min_fuel);
        }
        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/7.txt");
            string[] line = lines[0].Split(",");

            int[] positions = new int[line.Length];

            for (int i=0; i<line.Length ; i++)
            {
                positions[i] = Convert.ToInt32(line[i]);
            }
            
            // iterate through potential positions
            int min_fuel = -1;
            int min_position = 0;
            for (int i=0; i<=positions.Max(); i++)
            {
                int fuel = 0;
                foreach (int position in positions)
                {   
                    for (int j=0; j<=Math.Abs(i-position); j++)
                    {
                        fuel += j;
                    }
                }

                if ((fuel <= min_fuel) || (min_fuel==-1))
                {
                    min_fuel = fuel;
                    min_position = i;
                }
            }

            Console.WriteLine(min_fuel);
        }
    }
}