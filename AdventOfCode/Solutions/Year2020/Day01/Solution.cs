using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {
        
        public Day01() : base(01, 2020, "Report Repair")
        {
            //DebugInput = "1721\n" +
            //              "979\n" +
            //              "366\n" +
            //              "299\n" +
            //              "675\n" +
            //             "1456\n";
        }

        protected override string SolvePartOne()
        {
            // Trying to use this Advent of Code to get better at Linq
            // TBH I'm pretty sure the for-loops would've been more efficient than this mess

            int[] input = Input.ToIntArray("\n");
            var sumQuery =
                from A in input
                from B in input
                where A + B == 2020
                select A * B;
            return sumQuery.Any() ? sumQuery.First().ToString() : null;
            //foreach (var A in Input.ToIntArray("\n"))
            //{
            //    foreach (var B in Input.ToIntArray("\n"))
            //    {
            //        if (A + B == 2020)
            //            return (A * B).ToString();
            //    }
            //}

        }

        protected override string SolvePartTwo()
        {
            int[] input = Input.ToIntArray("\n");
            var sumQuery =
                from A in input
                from B in input
                from C in input
                where A + B + C == 2020
                select A * B * C;
            return sumQuery.Any() ? sumQuery.First().ToString() : null;
            //SearchForSum3(Input.ToIntArray(), 2020)
        }

        // giving up on functions

        // ok I'll be honest I don't actually know how to search for a variable amount of number
        //int[] SearchForSum2(int[] input, int sum)
        //{
        //    var sumQuery =
        //        from A in input
        //        from B in input
        //        where A + B == sum
        //        select A;

        //    return sumQuery.Distinct().ToArray();
        //}

        //int[] SearchForSum3(int[] input, int sum)
        //{
        //    var sumQuery =
        //        from A in input
        //        from B in input
        //        from C in input
        //        where A + B + C == sum
        //        select A;

        //    return sumQuery.Distinct().ToArray();
        //}
    }
}
