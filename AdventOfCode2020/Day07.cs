using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AdventOfCode2020.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day07
    {
        public static void Part1()
        {
            List<Rule> rules = new List<Rule>();
            foreach (string line in File.ReadAllLines("Inputs/Day07.txt"))
            {
                rules.Add(Rule.GetRule(line));
            }

            Queue<Rule> pending = new Queue<Rule>(rules.Where(r => r.ChildColorsAndAmounts.ContainsKey("shiny gold")));
            HashSet<string> visited = new HashSet<string>();

            while (pending.Any())
            {
                Rule currentRule = pending.Dequeue();
                if (!visited.Contains(currentRule.ParentColor))
                {
                    visited.Add(currentRule.ParentColor);

                    foreach (Rule validContainer in rules.Where(r => r.ChildColorsAndAmounts.ContainsKey(currentRule.ParentColor)))
                    {
                        pending.Enqueue(validContainer);
                    }
                }
            }

            Console.WriteLine($"There are {visited.Count} bag colors that can eventually contain at least one shiny gold bag");
        }

        public static void Part2()
        {
            List<Rule> rules = new List<Rule>();
            foreach (string line in File.ReadAllLines("Inputs/Day07.txt"))
            {
                rules.Add(Rule.GetRule(line));
            }
            int totalSubBags = GetSubtreeTotal(1, "shiny gold", rules);

            Console.WriteLine($"A single shiny gold bag must contain {totalSubBags - 1} other bags");
        }

        public static int GetSubtreeTotal(int factor, string bagName, List<Rule> rules)
        {
            var myRules = rules.Where(r => r.ParentColor == bagName);
            if (myRules.Any())
            {
                int sum = 0;
                foreach (Rule r in myRules)
                {
                    sum += factor;
                    foreach (var kvp in r.ChildColorsAndAmounts)
                    {
                        sum += GetSubtreeTotal(factor * kvp.Value, kvp.Key, rules);
                    }
                }
                return sum;
            }
            else
            {
                return factor;
            }
        }

    }

    public class Rule
    {
        private static Regex regex = new Regex("^([a-z]+ [a-z]+) bags? contain ");
        private static Regex childrenRegex = new Regex("(?:([1-9]) ([a-z]+ [a-z]+) bags?[.,])");
        private static Regex emptyBagRegex = new Regex("^([a-z]+ [a-z]+) bags contain no other bags.");

        private Rule() { }

        public static Rule GetRule(string line)
        {
            Rule rule = new Rule();

            rule.ChildColorsAndAmounts = new Dictionary<string, int>();

            MatchCollection mc = emptyBagRegex.Matches(line);
            if (mc.Count > 0)
            {
                rule.ParentColor = mc[0].Groups[1].Value;
            }
            else
            {
                mc = regex.Matches(line);
                rule.ParentColor = mc[0].Groups[1].Value;


                mc = childrenRegex.Matches(line.Replace(mc[0].Value, ""));
                foreach(Match match in mc)
                {
                    rule.ChildColorsAndAmounts.Add(match.Groups[2].Value, int.Parse(match.Groups[1].Value));
                }
            }
            return rule;
        }

        public string ParentColor { get; set; }
        public Dictionary<string, int> ChildColorsAndAmounts { get; set; }
    }
}
