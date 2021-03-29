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
        string[] groups;
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
            // split by blank lines
            groups = Input.SplitByEmptyLine();
        }

        // identify the questions to which anyone answered "yes"
        protected override string SolvePartOne()
        {
            // split by blank lines
            //var groups = Input.SplitByEmptyLine();

            int sum = 0;
            foreach (var group in groups)
            {
                // gets a collection of all the distinct letters in the group
                var answers = group.Except("\n").Distinct();
                // add the answers to the total sum
                sum += answers.Count();
            }
            return string.Format("there were [{0}]{1} answers that anyone in the group answered \"yes\" to.",
                sum,
                DebugInput != null ?
                    "(DEBUG: should be 11)" : string.Empty
            );
        }

        // identify the questions to which everyone answered "yes"!
        protected override string SolvePartTwo()
        {
            int sum = 0;
            foreach (var group in groups)
            {
                var people = group.SplitByNewline();
                char[] answers;
                answers = people.First().ToCharArray();

                // only contains chars when all people in group answered with them
                foreach (var p in people)
                {
                    answers = answers.Intersect(p).ToArray();
                }

                // add the answers to the total sum
                // DEBUG: sum should be 6
                sum += answers.Count();
            }

            return string.Format("there were [{0}]{1} answers that anyone in the group answered \"yes\" to.",
                sum,
                DebugInput != null ?
                    "(DEBUG: should be 6)" : string.Empty
            );
        }
    }
}
