using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    class RetreatPathCache
    {
        public static double Eps = 10; 

        public double PudgeX;

        public double PudgeY;

        public double EnemyX;

        public double EnemyY;

        public PudgePath RetreatPath;

        public bool IsSuitable(double pudgeX, double pudgeY, double enemyX, double enemyY)
        {
            return (Math.Abs(pudgeX - PudgeX) < Eps)
                && (Math.Abs(pudgeY - PudgeY) < Eps)
                && (Math.Abs(enemyX - EnemyX) < Eps)
                && (Math.Abs(enemyY - EnemyY) < Eps);
        }
    }
}
