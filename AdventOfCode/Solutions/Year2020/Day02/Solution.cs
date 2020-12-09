using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;

namespace AdventOfCode.Solutions.Year2020
{

    class Day02 : ASolution
    {

        public Day02() : base(02, 2020, "Password Philosophy")
        {
            //DebugInput = "1-3 a: abcde\n1 - 3 b: cdefg\n2 - 9 c: ccccccccc\n";
        }

        protected override string SolvePartOne()
        {
            /*
             * list of passwords and the policy when that password was set
             * 
             * 1-3 a: abcde // valid: contains 2 'a's
             * 1-3 b: cdefg // not valid: needs at least 1 'b'
             * 2-9 c: ccccccccc // valid: contains exactly 9 'c's
             */

            // format: "[int]-[int] [char]: [string]"

            // format notes: only one letter per policy, numbers can be two-digit (can't get away with parsing a single char)
            string[] input = Input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            // counts the number of valid passwords in input
            int nValidPasswords = 0;
            // Each line gives the password policy and then the password.
            foreach (string line in input)
            {
                PasswordPolicy passwordPolicy = new PasswordPolicy(line.Split(new[] { "-", " ", ": " }, StringSplitOptions.RemoveEmptyEntries));
                if (passwordPolicy.IsValid())
                    nValidPasswords++;
            }




            // The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid.
            // For example, "1-3 a:" means that the password must contain 'a' at least 1 time and at most 3 times.

            // How many passwords are valid according to their policies?
            return nValidPasswords.ToString();
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        // weird struct
        struct PasswordPolicy
        {
            public int min;
            public int max;
            public char letter;
            public string password;

            public bool IsValid()
            {
                int count = CountLetter();
                return count >= min && count <= max;
                //if ( password.F )
            }

            public int CountLetter()
            {
                int count = 0;
                foreach (char c in password)
                {
                    if (c == letter)
                        count++;
                }
                return count;
            }


            /// <summary>
            /// very unsafe constructor
            /// </summary>
            /// <param name="cast">must be a set of strings that can be parsed as { int, int, char, string } </param>
            public PasswordPolicy(string[] cast)
            {
                min = int.Parse(cast[0]);
                max = int.Parse(cast[1]);
                letter = char.Parse(cast[2]);
                password = cast[3];
            }

            public PasswordPolicy(int min, int max, char delim, string password)
            {
                this.min = min;
                this.max = max;
                this.letter = delim;
                this.password = password;
            }
        }

    }
}
