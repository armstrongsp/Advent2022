using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Advent2022
{
    public class Day15_Scan
    {
        public Day15_Scan(Point source, Point beacon)
        {
            scanPoint = source;
            beaconPoint = beacon;
            dist = CalcDist(source, beacon);
        }

        public Point scanPoint { get; set; }
        public Point beaconPoint { get; set; }
        public int dist { get; set; }
      
        private int CalcDist(Point a, Point b)
        {
            int xDist = Math.Abs(a.X - b.X);
            int yDist = Math.Abs(a.Y - b.Y);
            return xDist + yDist;
        }
    }
}
