using AIRLab.Mathematics;
using CVARC.V2;
using Pudge.ClientClasses;
using Pudge.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    static class SlardarHelper
    {

        private static double ForwardVisRadSqr = SlardarRules.Current.ForwardVisibilityRadius * SlardarRules.Current.ForwardVisibilityRadius + 1;
        private static double SideVisRadSqr = SlardarRules.Current.SideVisibilityRadius * SlardarRules.Current.SideVisibilityRadius + 1;


        public static bool IsUnderAttack(LocatorItem pudgeLocation, Point2D slardarCoords, double slardarAngle)
        {
            var dirAngle = Helper.GetMoveAngle(pudgeLocation.X - slardarCoords.X, pudgeLocation.Y - slardarCoords.Y);
            var aDif = Helper.GetAngleDif(slardarAngle, dirAngle);
            if (Math.Abs(aDif) <= 45)
            {
                return true;
                //return Helper.SqrDist(pudgeLocation.X, pudgeLocation.Y, slardarCoords.X, slardarCoords.Y) < ForwardVisRadSqr;
            }
            else
            {
                return Helper.SqrDist(pudgeLocation.X, pudgeLocation.Y, slardarCoords.X, slardarCoords.Y) < SideVisRadSqr;
            }

        }
    }
}
