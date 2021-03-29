using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day06 : ASolution
    {

        public Day06() : base(06, 2020, "")
        {
            //DebugInput = "abc\n" +
            //             "   \n" +
            //             "a  \n" +
            //             "b  \n" +
            //             "c  \n" +
            //             "   \n" +
            //             "ab \n" +
            //             "ac \n" +
            //             "   \n" +
            //             "a  \n" +
            //             "a  \n" +
            //             "a  \n" +
            //             "a  \n" +
            //             "   \n" +
            //             "b";
        }

        protected override string SolvePartOne()
        {
            // split by blank lines
            var groups = Input.SplitByEmptyLine();

            int sum = 0;
            foreach (var group in groups)
            {
                // gets a collection of all the distinct letters in the group
                var answers = group.Except("\n").Distinct();
                // add the answers to the total sum
                sum += answers.Count();
            }
            return $"the sum of all answer counts is [{sum}] { (DebugInput != null ? "(DEBUG)\n(Should be 11)" : string.Empty) }";
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
