using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Linq;

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
        public struct Passport
        {
            public string byr;
            public string iyr;
            public string eyr;
            public string hgt;
            public string hcl;
            public string ecl;
            public string pid;
            public string cid;
        }

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

        private static readonly string[] requiredKeys = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        private static bool HasAllKeys(Dictionary<string,string> p) => (
                    p.ContainsKey("byr") &&
                    p.ContainsKey("iyr") &&
                    p.ContainsKey("eyr") &&
                    p.ContainsKey("hgt") &&
                    p.ContainsKey("hcl") &&
                    p.ContainsKey("ecl") &&
                    p.ContainsKey("pid")
                    );
        protected override string SolvePartOne()
        {
            var passports = ReadPassports(Input);
            int validPassports = 0;
            foreach (var p in passports)
            {
                if (HasAllKeys(p)) validPassports++;
                requiredKeys.Intersect(p.Keys); // just checking
            }

            return $"out of {passports.Count} passports, {validPassports} are valid";
        }

        protected override string SolvePartTwo()
        {
            var passports = ReadPassports(Input);


            //// ah forget it 
            //passports.RemoveAll(InvalidPassport);

            int validPassports = 0;
            foreach (var p in passports.FindAll(HasAllKeys))
            {
                if (ValidatePassport(p)) validPassports++;
            }

            return $"out of {passports.Count} passports, {validPassports} are valid";
        }

        bool ValidatePassport(Dictionary<string, string> passport)
        {
            //Passport p = passport;
            Dictionary<string, string> p = passport;
            if (!ValidateYear(p["byr"], 1920, 2002))
            {
                Debug.Print("invalid byr: {0}", p["byr"]);
                return false;
            }
            // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
            if (!ValidateYear(p["iyr"], 2010, 2020))
            {
                Debug.Print("invalid iyr: {0}", p["iyr"]);
                return false;
            }
            // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
            bool v = ValidateYear(p["eyr"], 2020, 2030);
            if (!v)
            {
                Debug.Print("invalid eyr: {0}", p["eyr"]);
                return false;
            }
            // hgt(Height) - a number followed by either cm or in:
            var units = p["hgt"].TakeLast(2); //unused but I want to remind myself that TakeLast exists
            if (p["hgt"].EndsWith("cm"))
            {
            //      If cm, the number must be at least 150 and at most 193.
                if (!ValidateHeight(p["hgt"], "cm", 150, 193))
                {
                    Debug.Print("invalid hgt(cm): {0}", p["hgt"]);
                    return false;
                }
            }
            else if (p["hgt"].EndsWith("in"))
            {
            //      If in, the number must be at least 59 and at most 76.
                if (!ValidateHeight(p["hgt"], "in", 59, 76))
                {
                    Debug.Print("invalid hgt(in): {0}", p["hgt"]);
                    return false;
                }
            }
            else return false;
            
            
            // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            if (!ValidateHexcode(p["hcl"]))
            {
                Debug.Print("Failed hair colour(hex) check");
                return false;
            }
            // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            string[] ecl = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!(ecl.Contains(p["ecl"])))
            {
                Debug.Print("Failed eye colour(enum) check");
                return false;
            }

            // pid(Passport ID) - a nine - digit number, including leading zeroes.
            // 9 digits, all zeros
            string ID_pattern = "/d{9}";
            if (!Regex.IsMatch(p["pid"], ID_pattern))
            {
                Debug.Print("Failed personal ID check");
                return false;
            }
            // cid(Country ID) - ignored, missing or not.
            if (!p.ContainsKey("cid") || !Regex.IsMatch(p["cid"], ID_pattern))
            {
                Debug.Print("failed Country ID check, passport is still valid");
            }
            else Debug.Print("Passed all checks, passport is valid");

            return true;
        }

        bool ValidatePassport(Passport passport)
        {
            Passport p = passport;

            // byr(Birth Year) - four digits; at least 1920 and at most 2002.
            // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
            // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
            // hgt(Height) - a number followed by either cm or in:
            //      If cm, the number must be at least 150 and at most 193.
            //      If in, the number must be at least 59 and at most 76.
            // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            if (!ValidateYear(p.byr, 1920, 2002))
            {
                Debug.Print("Failed byr check");
                return false;
            }
            if (!ValidateYear(p.iyr, 2010, 2020)) 
            { 
                return false; 
            }

            if (!ValidateYear(p.eyr, 2020, 2030))
            {
                return false;
            }

            if (!ValidateHeight(p.hgt, "cm", 150, 193))
            {
                return false;
            }

            if (!ValidateHeight(p.hgt, "in", 59, 76))
            {
                return false;
            }

            if (!ValidateHexcode(p.hcl))
            {
                return false;
            }

            string[] ecl = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!(ecl.Contains(p.ecl))) {
                return false;
            }

            // pid(Passport ID) - a nine - digit number, including leading zeroes.
            // 9 digits, all zeros
            string ID_pattern = "/d{9}";
            if (!Regex.IsMatch(p.pid, ID_pattern)) {
                return false;
            }

            // cid(Country ID) - ignored, missing or not.
            //if (!Regex.IsMatch(p.pid, ID_pattern)) return false;

            return true;
        }

        bool ValidateYear(string year, int oldest, int youngest)
        {
            if (year.Length == 4 && int.TryParse(year.ToString(), out int n))
            {
                if (!(n >= oldest && n <= youngest))
                    return true;
            }
            return false;
        }


        bool ValidateHeight(string height, string units, int min, int max)
        {
            if (height.Contains(units) && int.TryParse(height.Remove(height.IndexOf(units)), out int n))
            {
                if (n >= min && n <= max)
                    return true;
            }
            return false;
        }

        bool ValidateHexcode(string color)
        {
            return Regex.IsMatch(color, "#([0-9a-f]{6})");
        }

        Passport CreatePassport(Dictionary<string, string> d)
        {
            Passport passport;
            d.TryGetValue("byr", out passport.byr);
            d.TryGetValue("iyr", out passport.iyr);
            d.TryGetValue("eyr", out passport.eyr);
            d.TryGetValue("hgt", out passport.hgt);
            d.TryGetValue("hcl", out passport.hcl);
            d.TryGetValue("ecl", out passport.ecl);
            d.TryGetValue("pid", out passport.pid);
            d.TryGetValue("cid", out passport.cid);
            return passport;
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
