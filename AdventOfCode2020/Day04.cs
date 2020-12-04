using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day04
    {
        public static void Part1()
        {
            //List<PassportData> data = ReadInputFile("Inputs/Day04_example.txt");
            List<PassportData> data = ReadInputFile("Inputs/Day04.txt");
            Console.WriteLine("There are " + data.Count(pd => pd.IsValidPart1) + " valid passports");
        }

        public static void Part2()
        {
            //List<PassportData> data = ReadInputFile("Inputs/Day04_example.txt");
            List<PassportData> data = ReadInputFile("Inputs/Day04.txt");
            //List<PassportData> valid1 = data.Where(pd => pd.IsValidPart1).ToList();

            //foreach (var pd in valid1.Where(pd => !pd.IsValidPart2())) Console.WriteLine(pd.ToString());

            Console.WriteLine("There are " + data.Count(pd => pd.IsValidPart2()) + " valid passports");
        }

        private static List<PassportData> ReadInputFile(string v)
        {
            List<PassportData> result = new List<PassportData>();
            StringBuilder dataAccumulator = new StringBuilder();
            foreach (string line in File.ReadLines(v))
            {

                if (string.IsNullOrEmpty(line))
                {
                    result.Add(PassportData.Parse(dataAccumulator));
                    dataAccumulator.Clear();
                }
                else
                {
                    dataAccumulator.Append($" {line}");
                }
            }

            if (dataAccumulator.Length > 0)
            {
                result.Add(PassportData.Parse(dataAccumulator));
            }

            return result;
        }
    }

    internal class PassportData
    {
        static Regex regex = new Regex("([a-z]{3}):([^ ]+)");
        static Dictionary<string, Regex> validationRegexDictionary = new Dictionary<string, Regex>()
        {
            { "byr", new Regex("19[2-9][0-9]|200[012]") },
            { "iyr", new Regex("201[0-9]|2020") },
            { "eyr", new Regex("202[0-9]|2030") },
            { "hgt", new Regex("([0-9]+cm|[0-9]+in)") },
            { "hcl", new Regex("#[0-9a-f]{6}") },
            { "ecl", new Regex("amb|blu|brn|gry|grn|hzl|oth") },
            { "pid", new Regex("[0-9]{9}") },
            { "cid", new Regex(".*") },
        };

        internal static PassportData Parse(StringBuilder dataAccumulator)
        {
            MatchCollection matches = regex.Matches(dataAccumulator.ToString());
            if (!matches.Any()) throw new ArgumentException("bad string data");
            PassportData passportData = new PassportData();
            foreach (Match match in matches)
            {
                passportData.Fields.Add(match.Groups[1].Value, match.Groups[2].Value);
            }

            return passportData;
        }

        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
        public bool IsValidPart1 => Fields.Count >= 8 || (Fields.Count == 7 && !Fields.ContainsKey("cid"));

        public bool IsValidPart2()
        {
            if (!IsValidPart1) return false;
            if (Fields.Any(kvp => !validationRegexDictionary[kvp.Key].IsMatch(kvp.Value))) return false;

            string heightField = Fields["hgt"].Replace("cm", "").Replace("in", "");
            int height = int.Parse(heightField);

            if (Fields["hgt"].Contains("cm") && (height < 150 || height > 193)) return false;
            else if (Fields["hgt"].Contains("in") && (height < 59 || height > 76)) return false;

            if (BirthYear < 1920 || BirthYear > 2002) return false;
            if (IssueYear < 2010 || IssueYear > 2020) return false;
            if (ExpirationYear < 2020 || ExpirationYear > 2030) return false;
            if (HairColor < 0) return false;
            if (EyeColor == EyeColor.zzz) return false;
            if (PassportId.Length != 9) return false;

            //Console.WriteLine(ToString());

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in Fields.OrderBy(kvp => kvp.Key))
            {
                sb.AppendLine($"{kvp.Key} => {kvp.Value}");
            }
            return sb.ToString();
        }


        public int BirthYear => int.Parse(Fields["byr"]);
        public int IssueYear => int.Parse(Fields["iyr"]);
        public int ExpirationYear => int.Parse(Fields["eyr"]);
        public string Height => Fields["hgt"];
        public EyeColor EyeColor => (EyeColor) Enum.Parse(typeof(EyeColor), Fields["ecl"]);
        public int HairColor => Convert.ToInt32(Fields["hcl"].Replace("#", "0x"), 16);
        public string PassportId => Fields["pid"];
        public string CountryId => Fields["cid"];

    }

    public enum EyeColor { amb, blu, brn, gry, grn, hzl, oth, zzz };
    }
