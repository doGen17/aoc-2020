using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AdventOfCode.Solutions.Year2020
{

    class Day04 : ASolution
    {
    // The automatic passport scanners are slow because they're having trouble detecting which passports have all required fields. The expected fields are as follows:

    // byr(Birth Year)
    // iyr(Issue Year)
    // eyr(Expiration Year)
    // hgt(Height)
    // hcl(Hair Color)
    // ecl(Eye Color)
    // pid(Passport ID)
    // cid(Country ID)
        //public struct Passport
        //{
        //    public int byr;
        //    public int iyr;
        //    public int eyr;
        //    public int hgt;
        //    public int hcl;
        //    public int ecl;
        //    public int pid;
        //    public int cid;
        //}

        public Day04() : base(04, 2020, "Passport Processing")
        {
            //DebugInput =
            //    "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\n" +
            //    "byr:1937 iyr:017 cid:147 hgt:183cm\n" +
            //    "\n" +
            //    "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884\n" +
            //    "hcl:#cfa07d byr:1929\n" +
            //    "\n" +
            //    "hcl:#ae17e1 iyr:2013\n" +
            //    "eyr:2024\n" +
            //    "ecl:brn pid:760753108 byr:1931\n" +
            //    "hgt:179cm\n" +
            //    "\n" +
            //    "hcl:#cfa07d eyr:2025 pid:166559648\n" +
            //    "iyr:2011 ecl:brn hgt:59in";
                

        }

        protected override string SolvePartOne()
        {
            var passports = ReadPassports(Input);
            int validPassports = 0;
            foreach (var p in passports)
            {
                bool isValid = (
                    p.ContainsKey("byr") &&
                    p.ContainsKey("iyr") &&
                    p.ContainsKey("eyr") &&
                    p.ContainsKey("hgt") &&
                    p.ContainsKey("hcl") &&
                    p.ContainsKey("ecl") &&
                    p.ContainsKey("pid") //&&
                                         //p.ContainsKey("cid")
                    );
                if (isValid) validPassports++;
            }

            return $"out of {passports.Count} passports, {validPassports} are valid";
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

        bool ValidatePassport(Dictionary<string, string> passport)
        {
            return true;
        }

        public List<Dictionary<string, string>> ReadPassports(string input)
        {
            var passports_string = input.Split("\n\n");

            var passportList = new List<Dictionary<string, string>>();
            foreach (string p in passports_string)
            {
                var fields = p.Split(' ', '\n');
                var passport = new Dictionary<string, string>();
                foreach (string item in fields)
                {
                    string[] kvp = item.Split(':', 2);
                    (string key, string value) = (kvp[0], kvp[1]);
                    passport.Add(key, value);

                    // actually this question only cares if the field exists right?
                    //switch (key)
                    //{
                    //    case "byr":
                    //        Debug.Assert(int.TryParse(value, out passport.byr));
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                passportList.Add(passport);
            }
            return passportList;
        }
    }
}
