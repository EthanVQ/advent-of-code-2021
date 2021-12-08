using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_4
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/4.txt");

            //seperate input numbers
            int[] numbers = lines[0].Split(",").Select(int.Parse).ToArray();

            // x - row, y - column, z - table
            lines = lines.Where((source, index) => index >= 2).ToArray();
            int[,,] boards = new int[5, 5, (lines.Length+1)/6];
            int x = 0;
            int y = 0;
            int z = 0;

            // assign tables to three dimnesional array
            foreach (string line in lines)
            {
                
                if (line == "")
                {
                    //iterate board increment
                    y = 0;
                    z++;
                    continue;
                }
                
                string[] line_array = line.Split(" ");
                line_array = line_array.Where((source, index) => source != "").ToArray();
                x = 0;
                foreach (string number in line_array)
                {
                    int int_number = Convert.ToInt32(number);

                    boards[x, y, z] = int_number;
                    x++;
                }
                y++;
            }
            
            //print boards
            // print_boards(boards);

            int[,,] winning_board = new int[5, 5, 1];
            int final_number = -1;
            foreach( int number in numbers )
            {
                // cross out numbers, set to -1
                for (int i=0; i<boards.GetLength(2); i++)
                {
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[k, j, i]==number)
                            {
                                boards[k, j , i] = -1;
                            }
                        }
                    }
                }

                
                // check for winner
                int winning_table = -1;

                // check a full row
                for (int i=0; i<boards.GetLength(2); i++)
                {   
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        int count = 0;
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[k, j, i]==-1)
                            {
                                count++;
                            }
                        }
                        if (count==5)
                        {
                            winning_table = i;
                        }
                    }
                }

                // check a full column
                for (int i=0; i<boards.GetLength(2); i++)
                {   
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        int count = 0;
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[k, j, i]==-1)
                            {
                                count++;
                            }
                        }
                        if (count==5)
                        {
                            winning_table = i;
                        }
                    }
                }

                if (winning_table >= 0)
                {
                    // assign winning board
                    for (int i=0; i<5; i++)
                    {   
                        for (int j=0; j<5; j++)
                        {
                            winning_board[i, j, 0] = boards[i, j, winning_table];
                        }
                    }
                    //last number
                    final_number = number;
                    break;
                }
            }

            // print_boards(winning_board);
            // sum final board
            int sum = 0;
            for (int i=0; i<5; i++)
            {   
                for (int j=0; j<5; j++)
                {
                    if (winning_board[i, j, 0]!=-1)
                    {
                        sum += winning_board[i, j, 0];
                    }
                }
            }

            Console.WriteLine(sum*final_number);
        }

        public static void print_boards(int[,,] matrix)
        {
            int xl = matrix.GetLength(0);
            int yl = matrix.GetLength(1);
            int zl = matrix.GetLength(2);

            for (int i=0; i<zl; i++)
            {
                for (int j=0; j<yl; j++)
                {
                    string line = "";
                    for (int k=0; k<xl; k++)
                    {
                        string num = Convert.ToString(matrix[k, j, i]);
                        line = line + num + " ";
                    }
                    Console.WriteLine(line);
                }
                Console.WriteLine(" ");
            }
        }

        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/4.txt");

            //seperate input numbers
            int[] numbers = lines[0].Split(",").Select(int.Parse).ToArray();

            // x - row, y - column, z - table
            lines = lines.Where((source, index) => index >= 2).ToArray();
            int[,,] boards = new int[5, 5, (lines.Length+1)/6];
            int x = 0;
            int y = 0;
            int z = 0;

            // assign tables to three dimnesional array
            foreach (string line in lines)
            {
                
                if (line == "")
                {
                    //iterate board increment
                    y = 0;
                    z++;
                    continue;
                }
                
                string[] line_array = line.Split(" ");
                line_array = line_array.Where((source, index) => source != "").ToArray();
                x = 0;
                foreach (string number in line_array)
                {
                    int int_number = Convert.ToInt32(number);

                    boards[x, y, z] = int_number;
                    x++;
                }
                y++;
            }
            
            //print boards
            // print_boards(boards);

            int[,,] winning_board = new int[5, 5, 1];
            int final_number = -1;
            int final_board_number = -1;

            // intiate board list
            IEnumerable<int> board_enumerable = Enumerable.Range(0, boards.GetLength(2));
            List<int> board_numbers = board_enumerable.ToList();

            foreach( int number in numbers )
            {
                // cross out numbers, set to -1
                for (int i=0; i<boards.GetLength(2); i++)
                {
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[k, j, i]==number)
                            {
                                boards[k, j , i] = -1;
                            }
                        }
                    }
                }

                // check a full row
                for (int i=0; i<boards.GetLength(2); i++)
                {   
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        int count = 0;
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[k, j, i]==-1)
                            {
                                count++;
                            }
                        }
                        if (count==5)
                        {
                            var itemToRemove = board_numbers.SingleOrDefault(r => r == i);
                            if (itemToRemove != null)
                            {
                                board_numbers.Remove(itemToRemove);
                            }
                        }
                    }
                }

                // check a full column
                for (int i=0; i<boards.GetLength(2); i++)
                {   
                    for (int j=0; j<boards.GetLength(1); j++)
                    {
                        int count = 0;
                        for (int k=0; k<boards.GetLength(0); k++)
                        {
                            if (boards[j, k, i]==-1)
                            {
                                count++;
                            }
                        }
                        if (count==5)
                        {
                            var itemToRemove = board_numbers.SingleOrDefault(r => r == i);
                            if (itemToRemove != null)
                            {
                                board_numbers.Remove(itemToRemove);
                            }
                        }
                    }
                }
                if (board_numbers.Count == 1)
                {
                    final_board_number = board_numbers[0];
                }

                if (board_numbers.Count == 0)
                {
                    // assign winning board
                    for (int i=0; i<5; i++)
                    {   
                        for (int j=0; j<5; j++)
                        {
                            winning_board[i, j, 0] = boards[i, j, final_board_number];
                        }
                    }
                    // print_boards(boards);
                    //last number
                    final_number = number;
                    break;
                }
            }

            // sum final board
            int sum = 0;
            for (int i=0; i<5; i++)
            {   
                for (int j=0; j<5; j++)
                {
                    if (winning_board[i, j, 0]!=-1)
                    {
                        sum += winning_board[i, j, 0];
                    }
                }
            }
            Console.WriteLine(sum*final_number);
        }
    }
}