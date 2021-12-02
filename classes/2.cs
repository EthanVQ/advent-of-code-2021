using System;
using System.Collections.Generic;

namespace _2021
{
    public class run_2
    {
        public static void run_part_1()
        {
            // read input data
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/2.txt");

            // x, y
            int[] position = new int[] { 0, 0 };

            foreach (string command in lines)
            {
                string[] words = command.Split(" ");
                string direction = words[0];
                int value = Int32.Parse(words[1]);

                if (direction == "forward")
                {
                    position[0] += value;
                }
                else if (direction == "down")
                {
                    position[1] += value;
                }
                else if (direction == "up")
                {
                    position[1] -= value;
                }
                else {
                    Console.WriteLine("Error with script");
                }
            }
            Console.WriteLine(position[0]*position[1]);
        }

        public static void run_part_2(){
            // read input data
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/2.txt");

            // x, y, aim
            int[] position = new int[] { 0, 0, 0 };

            foreach (string command in lines)
            {
                string[] words = command.Split(" ");
                string direction = words[0];
                int value = Int32.Parse(words[1]);

                if (direction == "forward")
                {
                    position[0] += value;
                    position[1] += value*position[2];
                }
                else if (direction == "down")
                {
                    position[2] += value;
                }
                else if (direction == "up")
                {
                    position[2] += -value;
                }
                else {
                    Console.WriteLine("Error with script");
                }

                // check depth hasn't exceeded below 0
                if (position[2]<0){
                    position[2] = 0;
                }
            }
            Console.WriteLine(position[0]*position[1]);

        }
    }
}