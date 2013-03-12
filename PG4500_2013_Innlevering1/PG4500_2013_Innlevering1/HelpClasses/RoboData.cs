using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Santom;

namespace PG4500_2013_Innlevering1
{
    class RoboData
    {
        public double energy;
        public Point2D position;
        public double ahead;
        public double back;
        public double rotationGunLeft;
        public double rotationGunRight;
        public double rotationRadarLeft;
        public double rotationRadarRight;
        public double rotationLeft;
        public double rotationRight;
        public double fire;
        public double heading;

        public RoboData()
        {
            position = new Point2D();
            clear();
        }

        public void clear()
        {
            energy = 0;
            ahead = 0;
            back = 0;
            rotationGunLeft = 0;
            rotationGunRight = 0;
            rotationRadarLeft = 0;
            rotationRadarRight = 0;
            rotationLeft = 0;
            rotationRight = 0;
            fire = 0;
        }
    }
}
