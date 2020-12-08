using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AdventOfCode2020.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day08
    {
        public static void Part1()
        {
            List<Instruction> instructions = new List<Instruction>();
            int lineNumber = 0;
            foreach (string line in File.ReadAllLines("Inputs/Day08.txt"))
            {
                instructions.Add(Instruction.GetInstruction(line, lineNumber++));
            }

            AOCProgram program = new AOCProgram(instructions);
            program.ExecuteUntilLoopOrEnd(0);
            Console.WriteLine($"Accumulator value before loop sets in: {program.acc}");
        }

        public static void Part2()
        {
            List<Instruction> instructions = new List<Instruction>();
            int lineNumber = 0;
            foreach (string line in File.ReadAllLines("Inputs/Day08.txt"))
            {
                instructions.Add(Instruction.GetInstruction(line, lineNumber++));
            }

            AOCProgram program = new AOCProgram(instructions);
            program.MakeProgramEnd();
            
            Console.WriteLine($"Accumulator value after modifying to end: {program.acc}");

        }
    }

    internal class Instruction
    {
        private Instruction() { }
        private int _num;
        private string _opName;
        private int _lineNumber;

        private static Regex regex = new Regex($"^(acc|jmp|nop) ([+-][0-9]+)$");
        public static Instruction GetInstruction(string line, int lineNumber)
        {
            Instruction newInstruction = new Instruction();
            MatchCollection mc = regex.Matches(line);
            newInstruction._opName = mc[0].Groups[1].Value;
            newInstruction._num = int.Parse(mc[0].Groups[2].Value);
            newInstruction._lineNumber = lineNumber;

            switch (newInstruction._opName)
            {
                case "nop":
                    newInstruction.Op = newInstruction.Nop;
                    newInstruction.ChangedOp = newInstruction.Jmp;
                    break;
                case "acc":
                    newInstruction.Op = newInstruction.Acc;
                    break;
                case "jmp":
                    newInstruction.Op = newInstruction.Jmp;
                    newInstruction.ChangedOp = newInstruction.Nop;
                    break;
                default:
                    throw new NotImplementedException("bad op code");
            }
            return newInstruction;
        }

        internal delegate int Operation(ref int v);
        internal Operation Op;
        internal Operation ChangedOp;

        public bool IsNopOrJmp => _opName != "acc";

        private int Nop(ref int _)
        {
            return 1;
        }

        private int Jmp(ref int _)
        {
            return _num;
        }

        private int Acc(ref int acc) 
        {
            acc += _num; 
            return 1;
        }
    }

    internal class AOCProgram
    {
        public int currentLine = 0;
        public int acc = 0;
        List<Instruction> Instructions;
        HashSet<int> ends;
        HashSet<int> visited;
        public AOCProgram(List<Instruction> instructions)
        {
            Instructions = instructions;
        }

        public bool ExecuteUntilLoopOrEnd(int lineNumber, int lineToChange = -1)
        {
            currentLine = lineNumber;
            acc = 0;
            visited = new HashSet<int>();
            do
            {
                visited.Add(currentLine);
                if(currentLine == lineToChange && Instructions[currentLine].IsNopOrJmp)
                    currentLine += Instructions[currentLine].ChangedOp(ref acc);
                else
                    currentLine += Instructions[currentLine].Op(ref acc);
            } while (!visited.Contains(currentLine) && currentLine < Instructions.Count);
            
            if (currentLine >= Instructions.Count) 
                return true;
            else 
                return false;
        }

        public void MakeProgramEnd()
        {
            FindTerminalLocations();
            int endLine = ends.Single();
            ExecuteUntilLoopOrEnd(0, endLine);

        }

        public void FindTerminalLocations()
        {
            ExecuteUntilLoopOrEnd(0);

            ends = new HashSet<int>();
            bool terminated = false;
            
            foreach(int lineToTry in visited)
            {
                terminated = ExecuteUntilLoopOrEnd(lineToTry, lineToTry);
                if (terminated)
                {
                    ends.Add(lineToTry);
                    break;
                }
            }
        }
    }
}
