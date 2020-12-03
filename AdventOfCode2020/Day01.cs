﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day01
    {
        public static HashSet<int> input = new HashSet<int>() { 2004,1823,1628,1867,1073,1951,1909,1761,1093,1992,1986,1106,1537,1905,1233,1961,1760,1562,1781,1329,1272,1660,1367,1248,1697,1515,1470,1980,1884,1784,1966,1778,1426,1255,1089,1748,1253,1870,1651,1131,1623,1595,1128,1014,1863,1855,1203,1395,1521,1365,1202,780,1560,1834,1494,1551,1398,1190,1975,1940,1217,1793,1310,1070,1865,1307,1735,1897,1410,1994,1541,1569,1731,1238,1193,1226,1435,1159,1642,1652,1908,1920,1930,1068,1914,1186,1795,1888,1634,1750,1950,1493,1353,1461,1658,1856,1301,1538,1948,1998,1847,1880,1657,1536,1457,1762,1706,1894,542,1991,1108,1072,1064,1511,1496,1480,1955,1604,1766,1983,1713,1234,1503,1583,1729,1140,1006,1600,1699,1280,1891,1996,1375,1167,1625,1129,1770,1497,1620,1267,1421,1399,1563,1636,1293,1506,1613,1958,1967,1182,1050,1947,1787,1774,1928,1896,1303,1826,1132,1254,1752,1510,1705,1229,1558,1989,1567,698,1738,1357,1587,1316,1838,1311,1057,1644,1135,1300,1134,1577,1381,1806,1176,1993,1769,1633,1450,1819,1973,1694,969,1987,1095,1717,1933,1593,1045,1355,1459,1619};
        //public static HashSet<int> input = new HashSet<int>() { 1721, 979, 366, 299, 675, 1456 };

        public static void Part1()
        {

            foreach (int i in input)
            {
                int find = 2020 - i;
                if (input.Contains(find))
                {
                    Console.WriteLine($"Found {i} + {find} = 2020");
                    Console.WriteLine($"Answer is {i} * {find} = {i * find}");
                    return;
                }
            }
            throw new Exception("No answer found");
        }

        public static void Part2()
        {
            List<int> orderedInput = input.OrderBy(i => i).ToList();
            int index1 = 0;
            int index2 = 1;
            for (index1 = 0; index1 < orderedInput.Count; index1++)
            {
                for (index2 = 1; index2 < orderedInput.Count; index2++)
                {
                    if (orderedInput[index1] + orderedInput[index2] >= 2020) break;
                    int find = 2020 - (orderedInput[index1] + orderedInput[index2]);
                    if (input.Contains(find))
                    {
                        Console.WriteLine($"Found {orderedInput[index1]} + {orderedInput[index2] } + {find} = 2020");
                        Console.WriteLine($"Answer is {orderedInput[index1]} * {orderedInput[index2] } * {find} =  {orderedInput[index1] * orderedInput[index2]  * find}");
                        return;
                    }
                }
            }
            throw new Exception("No answer found");
        }

    }
}
