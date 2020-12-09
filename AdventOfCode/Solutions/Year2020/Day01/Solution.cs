using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {

        public Day01() : base(01, 2020, "Report Repair")
        {

        }

        protected override string SolvePartOne()
        {
            foreach (var A in Input.ToIntArray("\n"))
            {
                foreach (var B in Input.ToIntArray("\n"))
                {
                    if (A + B == 2020)
                        return (A * B).ToString();
                }
            }
            return null;
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
