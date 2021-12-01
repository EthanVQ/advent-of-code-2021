using System;
using System.Collections.Generic;

namespace _2021
{
    public class run_1
    {
        public static void run(){
            // read input data
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/1.txt");

            // intialise dictionary
            IDictionary<string, int> increase_dict = new Dictionary<string, int>();
            increase_dict.Add("increase", 0);
            increase_dict.Add("decrease", 0);
            increase_dict.Add("same", 0);

            for(int i = 3; i < lines.Length; i++)
            {
                int i1 = Int32.Parse(lines[i-3]);
                int i2 = Int32.Parse(lines[i-2]);
                int i3 = Int32.Parse(lines[i-1]);
                int i4 = Int32.Parse(lines[i]);
                int before = i1 + i2 + i3;
                int after = i2 + i3 + i4;

                if ( before < after )
                {
                    // increase
                    increase_dict["increase"] = increase_dict["increase"] + 1;
                }
                else if ( before >= after )
                {
                    // decrease
                    increase_dict["decrease"] = increase_dict["decrease"] + 1;
                }
                else if ( before == after)
                {
                    // remains the same
                    increase_dict["same"] = increase_dict["same"] + 1;
                }
                else {
                    //error
                    Console.WriteLine("Script is not working.");
                }
            }

            // print answer
            Console.WriteLine(increase_dict["increase"]);
        }
    }
}