using AIRLab.Mathematics;
using Pudge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    static class PudgeExtentions
    {
        private static double? targetLastX = null;
        private static double? targetLastY = null;
        public static bool HookToWithCorrection(this PudgeController pudge, double x, double y, double enemyVelocity)
        {
            if (pudge.SensorsData.IsDead)
            {
                return false;
            }

            if (!pudge.IsHookReady())
            {
                return false;
            }

            double tX, tY;
            if ((targetLastX == null && targetLastY == null)
             || (Math.Abs(targetLastX.Value - x) < 0.001 && Math.Abs(targetLastY.Value - y) < 0.001))
            {
                tX = x;
                tY = y;
                targetLastX = x;
                targetLastY = y;
            }
            else
            {
                var enemyDir = (new Point2D(x - targetLastX.Value, y - targetLastY.Value)).Normalize();
                var hookTime = pudge.GetHookToTime(x, y);

                tX = x + enemyDir.X * (hookTime * enemyVelocity);
                tY = y + enemyDir.Y * (hookTime * enemyVelocity);
                targetLastX = x;
                targetLastY = y;
            }

            if (pudge.HookTo(tX, tY))
            {
                targetLastX = null;
                targetLastY = null;
                return true;
            }
            targetLastX = x;
            targetLastY = y;
            return false;
        }


        public static double GetHookToTime(this PudgeController pudge, double x, double y)
        {
            var targetAngle = pudge.GetTargetAngle(x, y);
            var rotateTime = pudge.GetRotateToTime(targetAngle);
            var distance = Math.Sqrt(Helper.SqrDist(x, y, pudge.SensorsData.SelfLocation.X, pudge.SensorsData.SelfLocation.Y));
            var hookTime = distance / PudgeRules.Current.HookVelocity;
            return rotateTime + hookTime;
        }


        public static double GetRotateToTime(this PudgeController pudge, double angle)
        {
            var angleDif = Helper.GetAngleDif(pudge.SensorsData.SelfLocation.Angle, angle);
            return Math.Abs(angleDif) / PudgeRules.Current.RotationVelocity;
        }
    }
}
