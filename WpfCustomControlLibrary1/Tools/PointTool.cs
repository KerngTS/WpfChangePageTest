using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCustomControlLibrary1.Tools
{
    public static class PointTool
    {
        public static Point GetMiddlePoint(Point p1,Point p2)
        {
            return new Point((p2.X + p1.X) / 2.0, (p2.Y + p1.Y) / 2.0);
        }

        public static Point Move(this Point pt, Vector vec,double dist)
        {
            vec.Normalize();
            return new Point(pt.X + vec.X * dist, pt.Y + vec.Y * dist);
        }

        public static double DistTo(this Point pt, Point pt1)
        {
            return Math.Sqrt(Math.Pow(pt1.X - pt.X, 2.0) + Math.Pow(pt1.Y - pt.Y, 2.0));
        }

        public static double DistTo(this Point pt, Point pt1,Vector vec)
        {
            vec.Normalize();
            var distVec = new Vector(pt1.X-pt.X,pt1.Y-pt.Y);
            return distVec.Dot(vec);
        }

        public static double Dot(this Vector v1,Vector v2)
        {
            return v1.X*v2.X+ v1.Y*v2.Y;
        }

        /// <summary>
        /// 角度轉弧度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double ToRadian(this double v)
        {
            return v * Math.PI / 180.0;
        }

        /// <summary>
        /// 弧度轉角度
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static double ToDegree(this double v)
        {
            return v * 180.0 / Math.PI;
        }
    }
}
