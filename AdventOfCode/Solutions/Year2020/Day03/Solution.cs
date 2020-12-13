using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        int height; int width;
        public Day03() : base(03, 2020, "Toboggan Trajectory")
        {

            //DebugInput =
            //    "..##.......\n" +
            //    "#...#...#..\n" +
            //    ".#....#..#.\n" +
            //    "..#.#...#.#\n" +
            //    ".#...##..#.\n" +
            //    "..#.##.....\n" +
            //    ".#.#.#....#\n" +
            //    ".#........#\n" +
            //    "#.##...#...\n" +
            //    "#...##....#\n" +
            //    ".#..#...#.#\n";

            var map = Input.SplitByNewline();
            height = map.Length;
            width = map[0].Length;
        }

        protected override string SolvePartOne()
        {
            // start by counting all the trees you would encounter for the slope right 3, down 1:
            int trees = CheckSlope(3, 1);

            // Starting at the top-left corner of your map and following a slope of right 3 and down 1, how many trees would you encounter?
            return "Following the slope: (right 3, down 1), you would encounter " + trees.ToString() +" trees.";
        }

        protected override string SolvePartTwo()
        {

            (int right, int down)[] slopes = { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

            string text = "";
            // don't bother with that
            var solutions = new Dictionary<(int right, int down), int>();
            //int[] solutions;
            
            foreach (var s in slopes)
            {
                int trees = CheckSlope(s.right, s.down);
                Debug.Print("Following the slope: " + s.ToString() + ", you would encounter [ " + trees.ToString() + " ] trees.");
                solutions[s] = trees;
                //if (solution != 0)
                //    solution *= trees;
                //else solution = trees;
            }


            text += "Trees for each slope are: " + solutions.Values.ToString() + "\n";

            // What do you get if you multiply together the number of trees encountered on each of the listed slopes?

            // can't get this to work for some reason
            // ok turns out the function is working fine, problem is: the answer is too big to fit into an int lmao
            //long solution = solutions.Values.Cast<long>().Aggregate(func: (a, b) => a * b);

            // switching to for-loop, aggregate too much hassle
            ulong solution = 1; // need a big number to fit the solution
            foreach (var s in solutions.Values)
            {
                solution *= (ulong)s;
            }

            

            text += "Final solution for Part 2 is: " + solution + ".";
            return "Final solution for Part 2 is: " + solution + ".";
        }

        int CheckSlope(int right, int down)
        {
            int trees = 0;
            string[] map = Input.SplitByNewline();
            int h = 0;
            int v = 0;
            while ((v += down) < height)
            {
                h += right;
                if(map[v][h % width] == '#')
                {
                    trees++;
                }
            }
            return trees;
        }
    }
}
