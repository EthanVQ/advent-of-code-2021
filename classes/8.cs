using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_8
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/8.txt");


            // extract and format data
            string[,] digits = new string[lines.Length, 4];
            string[,] codes = new string[lines.Length, 10];
            for (int i=0; i<lines.Length; i++)
            {
                string line = lines[i];
                string[] elements = line.Split("|");
                string[] temp_codes = elements[0].Trim().Split(" ");
                string[] temp_digits = elements[1].Trim().Split(" ");

                for (int j=0; j<temp_codes.Length; j++)
                {
                    codes[i, j] = temp_codes[j];
                }

                for (int j=0; j<temp_digits.Length; j++)
                {
                    digits[i, j] = temp_digits[j];
                }

            }

            int final_count = 0;
            for (int i=0; i<lines.Length; i++)
            {
                // // figure out encoding
                // IDictionary<string, string> encoding = new Dictionary<string, string>();
                // for (int j=0; j<10; j++)
                // {
                //     int length_of_digit = 
                // }

                // apply to digits
                for (int j=0; j<4; j++)
                {   
                    int digit_length = digits[i, j].Length;
                    if (digit_length==2 || digit_length==3 || digit_length==4 || digit_length==7)
                    {
                        final_count++;
                    }
                }
            }

            Console.WriteLine(final_count);
        }

        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/8.txt");

            // extract and format data
            string[,] digits = new string[lines.Length, 4];
            string[,] codes = new string[lines.Length, 10];
            for (int i=0; i<lines.Length; i++)
            {
                string line = lines[i];
                string[] elements = line.Split("|");
                string[] temp_codes = elements[0].Trim().Split(" ");
                string[] temp_digits = elements[1].Trim().Split(" ");

                for (int j=0; j<temp_codes.Length; j++)
                {
                    codes[i, j] = temp_codes[j];
                }

                for (int j=0; j<temp_digits.Length; j++)
                {
                    digits[i, j] = temp_digits[j];
                }

            }

            string[] decoded_digits = new string[] {"abcefg", "cf", "acdeg", "acdfg", "bdcf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg"};
            int result = 0;

            for (int i=0; i<lines.Length; i++)
            {
                // initialise encoding
                IDictionary<string, List<string>> encoding = new Dictionary<string, List<string>>();
                List<string> keys = new List<string> {"a", "b", "c", "d", "e", "f", "g"}; 
                foreach (string key in keys)
                {
                    encoding.Add(key, keys);
                }

                // apply 1, 4 and 7
                for (int j=0; j<10; j++)
                {   

                    // extract code and convert to list
                    List<string> code_list = new List<string>();
                    foreach(char character in codes[i, j])
                    {
                        code_list.Add(Convert.ToString(character));
                    }

                    // continue if not 1,4,7
                    int[] test_array = new int[] {2, 4, 3};
                    if (!test_array.Contains(code_list.Count))
                    {
                        continue;
                    }

                    // list to apply intersection to
                    List<string> intersections = new List<string>();

                    // apply 1, 4 and 7
                    if (code_list.Count==2)
                    {
                        //1: c,f
                        intersections.Add("c");
                        intersections.Add("f");
                    }
                    if (code_list.Count==4)
                    {
                        //4: c,f,b,d
                        intersections.Add("c");
                        intersections.Add("f");
                        intersections.Add("b");
                        intersections.Add("d");
                    }
                    if (code_list.Count==3)
                    {
                        //7: c,f,a
                        intersections.Add("c");
                        intersections.Add("f");
                        intersections.Add("a");
                    }

                    // apply intersection for 1, 4, 7
                    encoding = applyintersection(intersections, code_list, encoding);
                }

                // find number 5, 3
                List<string> five_code = new List<string>();
                List<string> three_code = new List<string>();
                for (int j=0; j<10; j++)
                {  
                    // extract code and convert to list
                    List<string> code_list = new List<string>();
                    foreach(char character in codes[i, j])
                    {
                        code_list.Add(Convert.ToString(character));
                    } 

                    //find 5
                    if(code_list.Contains(encoding["b"][0]) && code_list.Contains(encoding["b"][1]) && code_list.Count==5)
                    {
                        five_code = code_list;
                    }

                    //find 3
                    if(code_list.Contains(encoding["c"][0]) && code_list.Contains(encoding["c"][1]) && code_list.Count==5)
                    {
                        three_code = code_list;
                    }
                    
                }

                //set intersections
                List<string> five_intersections = new List<string>() {"a", "b", "d", "f", "g"};
                List<string> three_intersections = new List<string>() {"a", "c", "d", "f", "g"};



                //apply intersection for 5 and 3
                encoding = applyintersection(five_intersections, five_code, encoding);
                encoding = applyintersection(three_intersections, three_code, encoding);


                // transpose encoding so we can apply to decode
                IDictionary<string, string> applicable_encoding = new Dictionary<string, string>();
                foreach(KeyValuePair<string, List<string>> entry in encoding)
                {
                    applicable_encoding.Add(entry.Value[0], entry.Key);
                }
                
                //encoding found, now decode digits
                string final_number = "";
                for(int j=0; j<4; j++)
                {
                    string digit_code = digits[i, j];
                    string decoded_digit_code = "";

                    //decode digit code
                    foreach(char character in digit_code)
                    {
                        decoded_digit_code += applicable_encoding[Convert.ToString(character)];
                    }
                    // extract code and convert to list
                    List<string> decoded_digit_code_list = new List<string>();
                    foreach(char character in decoded_digit_code)
                    {
                        decoded_digit_code_list.Add(Convert.ToString(character));
                    }


                    //find number
                    string number = "";
                    for(int k=0; k<decoded_digits.Length; k++)
                    {
                        // extract code and convert to list
                        List<string> decoded_digit_list = new List<string>();
                        foreach(char character in decoded_digits[k])
                        {
                            decoded_digit_list.Add(Convert.ToString(character));
                        }
                        // var intersection = decoded_digit_code_list.Intersect(decoded_digit_list).ToList();
                        if (decoded_digit_code_list.All(decoded_digit_list.Contains) && decoded_digit_code_list.Count == decoded_digit_list.Count)
                        {
                            number=Convert.ToString(k);
                            break;
                        }
                    }
                    final_number += number;

                    // Console.WriteLine(decoded_digit_code + " - " + number);
                }
                // Console.WriteLine(final_number);
                result += Convert.ToInt32(final_number);
            }
            Console.WriteLine(result);
        }

        public static void display_encoding(IDictionary<string, List<string>> encoding)
        {
            foreach(KeyValuePair<string, List<string>> entry in encoding)
            {
                string write_line = "";
                foreach(string list_entry in entry.Value)
                {
                    write_line += list_entry;
                }
                Console.WriteLine(entry.Key+" - "+write_line);
            }
        }

        public static IDictionary<string, List<string>> applyintersection(List<string> intersections, List<string> code_list, IDictionary<string, List<string>> encoding)
        {
            IDictionary<string, List<string>> return_encoding = new Dictionary<string, List<string>>();
            foreach(KeyValuePair<string, List<string>> entry in encoding)
            {
                if(intersections.Contains(entry.Key))
                {
                    //intersect
                    return_encoding.Add(entry.Key, code_list.Intersect(entry.Value).ToList());
                }
                else
                {
                    //not intersect
                    List<string> new_entry = new List<string>(entry.Value);
                    foreach(string code_entry in code_list)
                    {
                        new_entry.Remove(code_entry);
                    }
                    return_encoding.Add(entry.Key, new_entry);
                }
            }
            return return_encoding;
        }
    }
}