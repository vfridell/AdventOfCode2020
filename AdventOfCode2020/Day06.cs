using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AdventOfCode2020.Models;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day06
    {
        public static void Part1()
        {
            Dictionary<int, List<Form>> groupForms = GetGroupForms();

            int totalYesses = 0;
            foreach (var kvp in groupForms)
            {
                int uniqueQuestions = kvp.Value.SelectMany(f => f.QuestionsAnswered).GroupBy(q => q).Count();
                totalYesses += uniqueQuestions;
            }

            Console.WriteLine($"Sum of 'yes' answers per group: {totalYesses}");
        }

        public static void Part2()
        {
            Dictionary<int, List<Form>> groupForms = GetGroupForms();

            int totalUnanimousYesses = 0;
            foreach (var kvp in groupForms)
            {
                HashSet<Question> unanimousQuestions = new HashSet<Question>(kvp.Value.SelectMany(f => f.QuestionsAnswered));
                foreach (var form in kvp.Value)
                {
                    unanimousQuestions.IntersectWith(form.QuestionsAnswered);
                }
                totalUnanimousYesses += unanimousQuestions.Count();
            }

            Console.WriteLine($"Sum of unanimous 'yes' answers per group: {totalUnanimousYesses}");
        }

        private static Dictionary<int, List<Form>> GetGroupForms()
        {
            int groupNum = 0;
            Dictionary<int, List<Form>> groupForms = new Dictionary<int, List<Form>>();
            foreach (string line in File.ReadAllLines("Inputs/Day06.txt"))
            {
                if (string.IsNullOrEmpty(line))
                {
                    groupNum++;
                }
                else
                {
                    Form form = new Form(line);
                    if (!groupForms.ContainsKey(groupNum)) groupForms.Add(groupNum, new List<Form>());
                    groupForms[groupNum].Add(form);
                }
            }

            return groupForms;
        }
    }

    public class Form
    {
        public Form(string line)
        {
            foreach (char c in line)
            {
                QuestionsAnswered.Add(new Question(c));
            }
        }

        public HashSet<Question> QuestionsAnswered { get; set; } = new HashSet<Question>();
    }

    public struct Question
    {
        public Question(char c)
        {
            ID = c;
        }
        public readonly char ID;
    }
}
