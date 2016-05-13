using AIRLab.Mathematics;
using Pudge;
using Pudge.Player;
using PudgeClient.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    class Strategy0 : GameStrategy
    {

        public Strategy0(PudgeController pudge, MapGraph map)
            :base(pudge, map)
        {

        }

        public override void Run()
        {
            MapVertex prevVertex = null;
            MapVertex nextVertex = Helper.GetСlosestVertex(map, pudge.SensorsData.SelfLocation.X, pudge.SensorsData.SelfLocation.Y);
            var sl = SlardarRules.Current.ForwardVisibilityRadius;
            var sv = SlardarRules.Current.MovementVelocity;
            var pv = PudgeRules.Current.MovementVelocity;
            for (; ; )
            {
                if (pudge.SensorsData.IsDead)
                {
                    pudge.Wait();
                    continue;

                    if (pudge.SensorsData.Map.Heroes.Count > 0)
                    {
                        var enemy = pudge.SensorsData.Map.Heroes.First();
                        var isInDanger = IsSlardarDanger(enemy.Location.X, enemy.Location.Y, enemy.Angle, pudge.Location.X, pudge.Location.Y);

                        if (!isInDanger)
                        {
                            pudge.HookTo(enemy.Location.X, enemy.Location.Y);
                        }
                        else
                        {
                            var direction = new Point2D(pudge.Location.X - enemy.Location.X, pudge.Location.Y - enemy.Location.Y);
                            direction = direction.Normalize();
                            pudge.MoveTo(pudge.Location.X + 5 * direction.X, pudge.Location.Y + 5 * direction.Y);
                        }
                    }
                    else if (pudge.MoveTo(nextVertex.X, nextVertex.Y))
                    {
                        var vertices = map.AdjacencyList[map.IndexOf(nextVertex)];
                        var nextInd = vertices.First(i => map.Vertices[i] != prevVertex);
                        prevVertex = nextVertex;
                        nextVertex = map.Vertices[nextInd];
                    }


                }
            }
        }
        

        protected bool IsSlardarDanger(double sX, double sY, double sA, double pX, double pY)
        {
            var dirAngle = Helper.GetMoveAngle(pX - sX, pY - sY);
            var aDif = Helper.GetAngleDif(sA, dirAngle);
            return Math.Abs(aDif) < 45;
        }
    }
}
