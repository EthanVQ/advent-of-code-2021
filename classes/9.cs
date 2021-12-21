using System;
using System.Collections.Generic;
using System.Linq;

namespace _2021
{
    public class run_9
    {
        public static void run_part_1()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/9.txt");

            // parse and format data
            int[,] heat_map = new int[lines[0].Length, lines.Length];
            for(int i=0; i<lines.Length; i++)
            {
                for(int j=0; j<lines[0].Length; j++)
                {
                    heat_map[j, i] = Convert.ToInt32(Convert.ToString(lines[i][j]));
                }
            }

            int final_number = 0;
            for(int i=0; i<heat_map.GetLength(0); i++)
            {
                for(int j=0; j<heat_map.GetLength(1); j++)
                {
                    //check adjacent values
                    int low_point = 1;

                    //check above
                    if(j!=0)
                    {
                        if(heat_map[i, j-1]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check below
                    if(j!=heat_map.GetLength(1)-1)
                    {
                       if(heat_map[i, j+1]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check left
                    if(i!=0)
                    {
                        if(heat_map[i-1, j]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check right
                    if (i!=heat_map.GetLength(0)-1)
                    {
                        if(heat_map[i+1, j]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    // check if still low point, add to final number
                    if (low_point==1)
                    {
                        final_number += heat_map[i, j] + 1;
                    }
                }
            }
            // display_grid(heat_map);
            Console.WriteLine(final_number);
        }

        public static void display_grid(int[,] heat_map)
        {
            for(int i=0; i<heat_map.GetLength(1); i++)
            {
                string line = "";
                for(int j=0; j<heat_map.GetLength(0); j++)
                {
                    line +=  Convert.ToString(heat_map[j, i]);
                }
                Console.WriteLine(line);
            }
        }


        public static void run_part_2()
        {
            string[] lines = System.IO.File.ReadAllLines("/Users/ethanjones/Documents/Advent of Code/2021/puzzle_input/9.txt");

            // parse and format data
            int[,] heat_map = new int[lines[0].Length, lines.Length];
            for(int i=0; i<lines.Length; i++)
            {
                for(int j=0; j<lines[0].Length; j++)
                {
                    heat_map[j, i] = Convert.ToInt32(Convert.ToString(lines[i][j]));
                }
            }
            
            //points not in a basin
            int[,] points_left = heat_map.Clone() as int[,];
            //remove 9s, set to -1
            for(int i=0; i<heat_map.GetLength(0); i++)
            {
                for(int j=0; j<heat_map.GetLength(1); j++)
                {
                    if(points_left[i, j]==9)
                    {
                        points_left[i, j] = -1;
                    }
                }
            }

            //grab lowpoints
            var low_points = grab_low_points(heat_map);
            int low_point_iterator = 0;
            
            //track basin counts
            List<int> basin_counts = new List<int>();
            
            //iterate basins
            foreach (int[] low_point in low_points)
            {
                //grab next unassigned point (no basin)
                int i = low_point[0];
                int j = low_point[1];
                List<int[]> points_to_check = new List<int[]>();
                points_to_check.Add(new int[]{i, j});
                List<int[]> basin = new List<int[]>();
                basin.Add(new int[]{i, j});
                low_point_iterator++;
                

                //iterate out from starting point
                while(true)
                {
                    //grab neighbours 
                    var neighbours = grab_flowing_neighbours(points_to_check, points_left);

                    if(neighbours.Count==0)
                    {  
                        //no more neighbours
                        break;
                    }

                    //remove points part of basin, and add neighbours to current basin
                    foreach(int[] neighbour in neighbours)
                    {
                        if(!basin.Contains(neighbour))
                        {
                            basin.Add(neighbour);
                            points_left[neighbour[0], neighbour[1]] = -1;
                        }
                    }

                    //next points to grow out will be the nieghbours
                    points_to_check = neighbours;
                }

                //display basin
                // display_basin(basin, heat_map);

                //add basin to list
                basin = get_distinct_list(basin);
                basin_counts.Add(basin.Count);
            }

            basin_counts.Sort();
            basin_counts.Reverse();
            var final_basin_counts = basin_counts.Take(3);
            Console.WriteLine(final_basin_counts.Aggregate(1, (acc, val) => acc * val));
        }

        public static void display_basin(List<int[]> basin, int[,] heat_map)
        {
            for(int i=0; i<heat_map.GetLength(1); i++)
            {
                string line = "";
                for(int j=0; j<heat_map.GetLength(0); j++)
                {
                    int condition = 0;
                    foreach(int[] pair in basin)
                    {
                        if(pair[0]==j && pair[1]==i)
                        {
                            line+=Convert.ToString(heat_map[j, i]);
                            condition = 1;
                            break;
                        }
                    }

                    if (condition==0)
                    {
                        line+="T";
                    }
                }
                Console.WriteLine(line);
            }
        }
        public static List<int[]> grab_low_points(int[,] heat_map)
        {
            List<int[]> return_list = new List<int[]>();
            for(int i=0; i<heat_map.GetLength(0); i++)
            {
                for(int j=0; j<heat_map.GetLength(1); j++)
                {
                    //check adjacent values
                    int low_point = 1;

                    //check above
                    if(j!=0)
                    {
                        if(heat_map[i, j-1]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check below
                    if(j!=heat_map.GetLength(1)-1)
                    {
                       if(heat_map[i, j+1]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check left
                    if(i!=0)
                    {
                        if(heat_map[i-1, j]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    //check right
                    if (i!=heat_map.GetLength(0)-1)
                    {
                        if(heat_map[i+1, j]<=heat_map[i, j])
                        {
                            low_point=0;
                        }
                    }

                    // check if still low point, add to final number
                    if (low_point==1)
                    {
                        int[] position = new int[] {i, j};
                        return_list.Add(position);
                    }
                }
            }
            return return_list;
        }
    
        public static List<int[]> grab_flowing_neighbours(List<int[]> points_to_check, int[,] points_left)
        {
            List<int[]> return_list = new List<int[]>();

            foreach(int[] point in points_to_check)
            {
                int i = point[0];
                int j = point[1];
                
                //check above
                if(j!=0)
                {
                    if(points_left[i, j-1]>points_left[i, j])
                    {
                        return_list.Add(new int[]{i, j-1});
                    }
                }

                //check below
                if(j!=points_left.GetLength(1)-1)
                {
                    if(points_left[i, j+1]>points_left[i, j])
                    {
                        return_list.Add(new int[]{i, j+1});
                    }
                }

                //check left
                if(i!=0)
                {
                    if(points_left[i-1, j]>points_left[i, j])
                    {
                        return_list.Add(new int[]{i-1, j});
                    }
                }

                //check right
                if (i!=points_left.GetLength(0)-1)
                {
                    if(points_left[i+1, j]>points_left[i, j])
                    {
                        return_list.Add(new int[]{i+1, j});
                    }
                }
            }

            // get distinct neighbours
            return_list = get_distinct_list(return_list);
            return return_list;
        }

        public static List<int[]> get_distinct_list(List<int[]> return_list)
        {
            List<int[]> distinct_return_list = new List<int[]>();
            foreach(int[] bas in return_list)
            {
                int copy=0;
                foreach(int[] bas2 in distinct_return_list)
                {
                    if (bas[0]==bas2[0] && bas[1]==bas2[1])
                    {
                        copy = 1;
                        break;
                    }
                }
                if (copy==0)
                {
                    distinct_return_list.Add(bas);
                }
            }
            return distinct_return_list;
        }
    }
}