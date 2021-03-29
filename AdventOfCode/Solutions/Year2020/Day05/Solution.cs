using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{
    // Instead of zones or groups, this airline uses !binary space partitioning! to seat people. A seat might be specified like FBFBBFFRLR, where F means "front", B means "back", L means "left", and R means "right".
    class Day05 : ASolution
    {
        // The first 7 characters will either be F or B;
        // these specify exactly one of the 128 rows on the plane (numbered 0 through 127).
        // Each letter tells you which half of a region the given seat is in.
        // Start with the whole list of rows;
        // the first letter indicates whether the seat is in the front (0 through 63) or the back (64 through 127).
        // The next letter indicates which half of that region the seat is in, and so on until you're left with exactly one row.
        struct BoardingPass
        {
            public int row;
            public int column;
            public int seatID;

            public BoardingPass(int row, int column, int seatID)
            {
                this.row = row;
                this.column = column;
                this.seatID = seatID;
            }

            public BoardingPass(int row, int column)
            {
                this.row = row;
                this.column = column;
                this.seatID = (row * 8) + column;
            }

            public BoardingPass(int seatID)
            {
                column = seatID % 8;
                row = seatID - column / 8;
                this.seatID = seatID;
            }
        }

        public Day05() : base(05, 2020, "")
        {
            //DebugInput =
            //    "BFFFBBFRRR\n"+
            //    "FFFBBBFRRR\n"+
            //    "BBFFBBFRLL";
            
        }

        string[] GetValidInput()
        {
            string[] input = Utilities.SplitByNewline(Input);
            Debug.Assert(input.All(s => s.Length == 10));
            Debug.Assert(input.All(s => Regex.IsMatch(s, "^([BF]{7})([RL]{3})$")), "Assert failed, Bad Input");

            return input;
        }

        BoardingPass[] GetBoardingPasses(string[] input)
        {
            BoardingPass[] passes = new BoardingPass[input.Length];

            foreach (var pass in input)
            {
                var p = DecodeBoardingPass(pass);
                passes[p.seatID] = p;
            }

            return passes;
        }

        IEnumerable<int> GetPassIDs(string[] input)
        {
            List<int> IDs = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                int id = DecodeBoardingPass(input[i]).seatID;
                IDs.Add(id);
            }
            IDs.Sort();
            return IDs;
        }

        BoardingPass DecodeBoardingPass(string pass)
        {

            int row = 0;
            // The first 7 (0-6) characters will either be F or B;
            // these specify exactly one of the 128 rows on the plane (numbered 0 through 127).
            for (int i = 0; i <= 6; i++)
            {
                if (pass[i] == 'B')
                {
                    // if F, the offset will remain 0, but the range (number of possible seats) will always be halved, so it will always be a product of the iterator
                    // this means BFFFBBF directly translates to the binary number 0b1000110(70), RRR to 0b111(7), and so on.
                    row += 1 << 6 - i; // same as 2^(6-i)
                }
            }
            int column = 0;
            for (int i = 1; i <= 3; i++)
            {
                column += pass[i + 6] == 'R' ? 1 << 3 - i : 0;
            }

            return new BoardingPass(row, column);
        }

        // private const string boardingPass_match = "^([BF]{7})([RL]{3})$";
        protected override string SolvePartOne()
        {
            string[] input = GetValidInput();
            
            int highestID = 0;
            foreach (string pass in input)
            {
                BoardingPass p = DecodeBoardingPass(pass);
                Console.WriteLine($" - {pass}: row {p.row}, column {p.column}, seat ID {p.seatID}");
                highestID = Math.Max(highestID, p.seatID);
            }

            return $"Highest Seat ID is: {highestID}";
        }

        protected override string SolvePartTwo()
        {
            //BoardingPass[] passes = GetBoardingPasses(GetValidInput());
            var IDs = GetPassIDs(GetValidInput());

            int lowestID = IDs.Min();
            int highestID = IDs.Max();


            for (int i = lowestID; i < highestID; i++)
            {
                if (IDs.Contains(i) == false)
                {
                    return $"Your seat ID is {i}";
                }
            }
            return $"Something went wrong, I can't find your seat ID!";
        }
    }
}
