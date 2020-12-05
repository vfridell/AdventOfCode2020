using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Models
{
    public class Rect
    {
        public Rect(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public Point Point => new Point(X, Y);

        public int SmallestSide => Math.Min(Width, Height);

        public readonly int X;
        public readonly int Y;
        public readonly int Width;
        public readonly int Height;

        internal bool Overlaps(Rect other)
        {
            if (X > other.X + other.Width) return false;
            if (other.X > X + Width) return false;
            if (Y > other.Y + other.Height) return false;
            if (other.Y > Y + Height) return false;
            return true;
        }

        internal bool Contains(Point point)
        {
            if (point.X < X || point.Y < Y || point.X > X + Width || point.Y > Y + Height) return false;
            return true;
        }

        /// <summary>
        /// Check if a point is off the grid
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if the given point is off the grid, false otherwise</returns>
        public bool IsOutOfBounds(Point point) => point.X < X || point.X > X + (Width - 1) || point.Y < Y || point.Y > Y + (Height - 1);

        public enum PointIterationDirection { LeftRight, UpDown };

        public List<Point> GetPoints(PointIterationDirection direction = PointIterationDirection.UpDown)
        {
            List<Point> resultList = new List<Point>();
            if (direction == PointIterationDirection.UpDown)
            {
                for (int x = X; x < X + Width; x++)
                {
                    for (int y = Y; y < Y + Height; y++)
                        resultList.Add(new Point(x, y));
                }
            }
            else
            {
                for (int y = Y; y < Y + Height; y++)
                {
                    for (int x = X; x < X + Width; x++)
                        resultList.Add(new Point(x, y));
                }
            }
            return resultList;
        }

        public List<Coordinate> GetCoordinates(PointIterationDirection direction = PointIterationDirection.UpDown)
        {
            List<Coordinate> resultList = new List<Coordinate>();
            if (direction == PointIterationDirection.UpDown)
            {
                for (int c = X; c < X + Width; c++)
                {
                    for (int r = Y; r < Y + Height; r++)
                        resultList.Add(new Coordinate(r, c));
                }
            }
            else
            {
                for (int r = Y; r < Y + Height; r++)
                {
                    for (int c = X; c < X + Width; c++)
                        resultList.Add(new Coordinate(r, c));
                }
            }
            return resultList;
        }

        public override bool Equals(object obj)
        {
            Rect other = obj as Rect;
            if (null == other) return false;
            return Equals(other);
        }

        public bool Equals(Rect other) => Point.Equals(other.Point) && Width == other.Width && Height == other.Height;

        public override int GetHashCode()
        {
            int hash = Point.GetHashCode();
            hash = (hash * 397) ^ Width.GetHashCode();
            hash = (hash * 397) ^ Height.GetHashCode();
            return hash;
        }

        public override string ToString() => $"{Point},{SmallestSide}";

    }


}
