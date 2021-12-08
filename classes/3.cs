using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_3
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/3.txt");

            string[] columns = new string[lines[0].Length];

            int i = 0;
            int one_count = 0;
            int zero_count = 0;
            string gamma_binary = "";
            string epsilon_binary = "";

            // transpose data
            foreach (string line in lines)
            {
                i = 0;
                foreach (char digit in line)
                {
                    columns[i] += digit;
                    i++;
                }
            }

            // get rates
            foreach (string column in columns)
            {
                one_count = column.Split("1").Length - 1;
                zero_count = column.Split("0").Length - 1;

                if (one_count < zero_count){
                    gamma_binary += "0";
                    epsilon_binary += "1";
                }
                else if (one_count > zero_count)
                {
                    gamma_binary += "1";
                    epsilon_binary += "0";
                }
                else
                {
                    Console.WriteLine("Even counts.");
                }
            }

            // convert binary string to decimal
            int gamma_rate = Convert.ToInt32(gamma_binary, 2);
            int epsilon_rate = Convert.ToInt32(epsilon_binary, 2);

            Console.WriteLine(gamma_rate*epsilon_rate);
        }

        public static void run_part_2()
        {
            string[] numbers = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/3.txt");
            
            int life_support_rating = grab_rating(numbers, "life support");
            int C02_scrubber_rating = grab_rating(numbers, "C02 scrubber");

            Console.WriteLine(life_support_rating*C02_scrubber_rating);
            
        }

        public static string[] transpose_data(string[] numbers)
        {
            string[] columns = new string[numbers[0].Length];
            int i;
            foreach (string number in numbers)
            {
                i = 0;
                foreach (char digit in number)
                {
                    columns[i] += digit;
                    i++;
                }
            }
            return columns;
        }

        public static int grab_rating(string[] numbers, string criteria)
        {
            string digit_to_keep = "";
            int zero_count = 0;
            int one_count = 0;
            int length = numbers[0].Length;

            for (int i=0 ; i<length ; i++)
            {
                // find number of 1,0's in the ith column
                string[] transposed_numbers = transpose_data(numbers);
                one_count = transposed_numbers[i].Split("1").Length - 1;
                zero_count = transposed_numbers[i].Split("0").Length - 1;

                // determine digit to keep based on criteria
                if (criteria=="life support")
                {
                    if (one_count>=zero_count)
                    {
                        digit_to_keep = "1";
                    }
                    else
                    {
                        digit_to_keep = "0";
                    }
                }
                else if (criteria=="C02 scrubber")
                {
                    if (one_count>=zero_count)
                    {
                        digit_to_keep = "0";
                    }
                    else
                    {
                        digit_to_keep = "1";
                    }
                }
                else 
                {
                    Console.WriteLine("No criteria selected.");
                    return 0;
                }
                
                // remove rows
                numbers = numbers.Where(e => e[i] == Convert.ToChar(digit_to_keep)).ToArray();
                
                // if all rows have been removed stop
                if (numbers.Length==1)
                {
                    break;
                }
            }

            // convert to integer
            int rating = Convert.ToInt32(numbers[0], 2);
            return rating;
        }
    }
}