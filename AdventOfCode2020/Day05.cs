using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AdventOfCode2020.Models;

namespace AdventOfCode2020
{
    public static class Day05
    {
        public static void Part1()
        {
            string input = "FBFBBFFRLR";
            Coordinate coord = GetSeatCoordinate(input);
            int seatNum = SeatNumber(coord);
            int maxSeatNum = 0;
            foreach (string seatString in File.ReadAllLines("Inputs/Day05.txt"))
            {
                maxSeatNum = Math.Max(SeatNumber(GetSeatCoordinate(seatString)), maxSeatNum);
            }

            Console.WriteLine($"Max seat number from inputs: {maxSeatNum}");
        }

        public static void Part2()
        {
            HashSet<Coordinate> ticketedSeats = new HashSet<Coordinate>();
            foreach (string seatString in File.ReadAllLines("Inputs/Day05.txt"))
            {
                ticketedSeats.Add(GetSeatCoordinate(seatString));
            }
            Rect rect = new Rect(0, 0, 8, 128);
            HashSet<Coordinate> allSeats = new HashSet<Coordinate>(rect.GetCoordinates());
            allSeats.ExceptWith(ticketedSeats);
            allSeats.RemoveWhere(new Predicate<Coordinate>(c => c.Row >= 109 || c.Row <= 5));
            foreach (Coordinate c in allSeats)
            {
                Console.WriteLine($"{c} => {SeatNumber(c)}");
            }
        }

        public static Coordinate GetSeatCoordinate(string input)
        {
            string rowString = input.Substring(0, 7);
            string columnString = input.Substring(7, 3);
            string binaryRowString = rowString.Replace('F', '0').Replace('B', '1');
            string binaryColString = columnString.Replace('L', '0').Replace('R', '1');
            return new Coordinate(Convert.ToInt32(binaryRowString, 2), Convert.ToInt32(binaryColString, 2));
        }

        public static int SeatNumber(Coordinate coordinate) => coordinate.Row * 8 + coordinate.Column;

    }
}
