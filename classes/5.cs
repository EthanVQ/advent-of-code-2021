using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_5
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/5.txt");
            int[,,] data = new int [lines.Length, 2, 2];

            // format data
            int dcount = 0;
            int pcount = 0;
            int ccount = 0;
            int maxx = 0;
            int maxy = 0;
            foreach (string line in lines)
            {       
                string[] parts = line.Trim().Split("->");
                pcount = 0;
                foreach (string part in parts)
                {
                    string[] xy_parts = part.Split(",");
                    ccount = 0;

                    if (Convert.ToInt32(xy_parts[0]) > maxx)
                    {
                        maxx = Convert.ToInt32(xy_parts[0]);
                    }
                    if (Convert.ToInt32(xy_parts[1]) > maxy)
                    {
                        maxy = Convert.ToInt32(xy_parts[1]);
                    }
                    foreach (string xy_part in xy_parts)
                    {
                        data[dcount, pcount, ccount] = Convert.ToInt32(xy_part);
                        ccount++;
                    }
                    pcount++;
                }
                dcount++;
            }
            // print_boards(data);

            // initiliase coordinates
            int[,] plane = new int [maxx+1, maxy+1];

            for (int i=0 ; i<data.GetLength(0) ; i++)
            {
                int xpos = data[i, 0, 0];
                int ypos = data[i, 0, 1];
                int xdif = data[i, 1, 0] - xpos;
                int ydif = data[i, 1, 1] - ypos;
                int step = 0;
                int dif = 0;
                int xy = 0;

                if (xdif==0)
                {
                    dif = Math.Abs(ydif);
                    xy = 1;
                    if (ydif>0)
                    {
                        step = 1;
                    }
                    else
                    {
                        step = -1;
                    }
                }
                else if (ydif==0)
                {
                    dif = Math.Abs(xdif);
                    xy = 0;
                    if (xdif>0)
                    {
                        step = 1;
                    }
                    else
                    {
                        step = -1;
                    }
                }
                else
                {
                    continue;
                }

                for (int j=0 ; j<dif+1 ; j++)
                {   
                    plane[xpos, ypos] += 1;
                    if (xy==0)
                    {
                        xpos += 1*step;
                    }
                    if (xy==1)
                    {
                        ypos += 1*step;
                    }
                }
            }

            //check for coordinates that are greater than 2
            int final_sum = 0;
            for (int j=0 ; j<plane.GetLength(1) ; j++)
            {
                string line = "";
                for (int i=0 ; i<plane.GetLength(0) ; i++)
                {
                    line += Convert.ToString(plane[i, j]) + " ";
                    if (plane[i, j]>=2)
                    {
                        final_sum+=1;
                    }
                }
                // Console.WriteLine(line);
            }

            Console.WriteLine(final_sum);
        }

        public static void print_boards(int[,,] matrix)
        {
            int xl = matrix.GetLength(0);
            int yl = matrix.GetLength(1);
            int zl = matrix.GetLength(2);

            for (int i=0; i<xl; i++)
            {
                for (int j=0; j<yl; j++)
                {
                    string line = "";
                    for (int k=0; k<zl; k++)
                    {
                        string num = Convert.ToString(matrix[i, j, k]);
                        line = line + num + " ";
                    }
                    Console.WriteLine(line);
                }
                Console.WriteLine(" ");
            }
        }

        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/5.txt");
            int[,,] data = new int [lines.Length, 2, 2];

            // format data
            int dcount = 0;
            int pcount = 0;
            int ccount = 0;
            int maxx = 0;
            int maxy = 0;
            foreach (string line in lines)
            {       
                string[] parts = line.Trim().Split("->");
                pcount = 0;
                foreach (string part in parts)
                {
                    string[] xy_parts = part.Split(",");
                    ccount = 0;

                    if (Convert.ToInt32(xy_parts[0]) > maxx)
                    {
                        maxx = Convert.ToInt32(xy_parts[0]);
                    }
                    if (Convert.ToInt32(xy_parts[1]) > maxy)
                    {
                        maxy = Convert.ToInt32(xy_parts[1]);
                    }
                    foreach (string xy_part in xy_parts)
                    {
                        data[dcount, pcount, ccount] = Convert.ToInt32(xy_part);
                        ccount++;
                    }
                    pcount++;
                }
                dcount++;
            }
            // print_boards(data);

            // initiliase coordinates
            int[,] plane = new int [maxx+1, maxy+1];

            for (int i=0 ; i<data.GetLength(0) ; i++)
            {
                int xpos = data[i, 0, 0];
                int ypos = data[i, 0, 1];

                int xdif = data[i, 1, 0] - xpos;
                int ydif = data[i, 1, 1] - ypos;
                int dif = 0;

                int xstep = 0;
                int ystep = 0;

                //figure out whether diagnoal, vertical, horizontal
                if ((xdif==0 ^ ydif==0) || (Math.Abs(xdif)==Math.Abs(ydif) & xdif!=0 & ydif!=0))
                {
                    //figure out x,y direction
                    if (xdif==0)
                    {
                        xstep = 0;
                    }
                    else if (xdif>0)
                    {
                        xstep = 1;
                    }
                    else if (xdif<0)
                    {
                        xstep = -1;
                    }

                    if (ydif==0)
                    {
                        ystep = 0;
                    }
                    else if (ydif>0)
                    {
                        ystep = 1;
                    }
                    else if (ydif<0)
                    {
                        ystep = -1;
                    }
                }
                else
                {
                    continue;
                }

                //get abs max dif
                int[] diffs = new int[] {Math.Abs(xdif), Math.Abs(ydif)};
                int max_dif = diffs.Max();

                for (int j=0 ; j<max_dif+1 ; j++)
                {   
                    plane[xpos, ypos] += 1;
                    xpos += 1*xstep;
                    ypos += 1*ystep;
                }
            }

            //check for coordinates that are greater than 2
            int final_sum = 0;
            for (int j=0 ; j<plane.GetLength(1) ; j++)
            {
                string line = "";
                for (int i=0 ; i<plane.GetLength(0) ; i++)
                {
                    line += Convert.ToString(plane[i, j]) + " ";
                    if (plane[i, j]>=2)
                    {
                        final_sum+=1;
                    }
                }
                // Console.WriteLine(line);
            }

            Console.WriteLine(final_sum);
        }
    }
}