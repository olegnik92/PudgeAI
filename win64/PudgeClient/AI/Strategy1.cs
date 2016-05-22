using AIRLab.Mathematics;
using Pudge;
using Pudge.ClientClasses;
using Pudge.Player;
using Pudge.Sensors.Map;
using PudgeClient.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    class Strategy1: GameStrategy
    {
        private Stopwatch watch = new Stopwatch();
        private const double runesMinDistance = 10;

        private MapVertex currentTarget = null;
        private PudgePath currentPath = null;
        private List<double> verticesProfit;
        private List<double> verticesDanger;
        private List<int> importantTargetsList;
        private double lastProfitHash = -1000;


        private int runesRespawnTimes = 0;
        private double lastRuneUpdateTime;
        private double lastDangerUpdateTime;
        private MapVertex closestVertex = null;
        private MapVertex ClosestVertex
        {
            get
            {
                if(closestVertex == null)
                {
                    closestVertex = Helper.GetСlosestVertex(map, pudge.Location.X, pudge.Location.Y);;
                }
                return closestVertex;
            }
        }

        public Strategy1(PudgeController pudge, MapGraph map)
            :base(pudge, map)
        {
            verticesProfit = map.Vertices.Select(v => v.Profit).ToList();
            verticesDanger = map.Vertices.Select(v => v.Danger).ToList();
            importantTargetsList = Enumerable.Range(0, map.Vertices.Count).Where(v => verticesProfit[v] > 0).ToList();
        }

        public override void Run()
        {
            while (true)
            {
                LoopBegin();
                if (pudge.SensorsData.IsDead)
                {
                    currentPath = null;
                    pudge.Sleep();
                    continue;
                }


                if (pudge.IsHookThrown())
                {
                    pudge.Sleep();
                    continue;
                }

                //todo обработка негативных эффектов !!!!!!!
                if (pudge.SensorsData.Map.Heroes.Count > 0)
                {
                    var enemy = pudge.SensorsData.Map.Heroes.First();

                    if(enemy.Type == HeroType.Slardar)
                    {
                        if (SlardarStrategy(enemy))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (EnemyPudgeStrategy(enemy))
                        {
                            continue;
                        }
                    }
                }

                if (VipStrategy())
                {
                    continue;
                }



                bool isNeedUpdate = false;
                var bestPath = FindBestPath(out isNeedUpdate);
                if (isNeedUpdate)
                {
                    if (bestPath == null)
                    {
                        pudge.Sleep();
                        continue;
                    }

                    if (currentPath == null || !currentPath.IsEqualTo(bestPath))
                    {
                        ChangeCurrentPath(bestPath);
                    }
                }

                if (MakeMoveStep())
                {
                    if (currentPath.IsPathDone())
                    {
                        PeekTarget();
                    }

                    continue;
                }

                pudge.Sleep();
            }
        }

        private PudgePath FindBestPath(out bool isNeedUpdate, List<double> dangerMap = null)
        {
            var profitHash = importantTargetsList.Sum(v => verticesProfit[v]);
            if((Math.Abs(profitHash - lastProfitHash) < 0.0001) && (currentPath  != null))
            {
                isNeedUpdate = false;
                return null;
            }
            else
            {
                isNeedUpdate = true;
                lastProfitHash = profitHash;
            }

            double maxProfit = double.NegativeInfinity;
            PudgePath maxProfitPath = null;

            var candidates = importantTargetsList
                .Where(i => verticesProfit[i] - verticesDanger[i] > 0).ToList();

            var pathes = PudgePath.FindPath(candidates, map.IndexOf(ClosestVertex), map, dangerMap);

            for (int i = 0; i < candidates.Count; i++ )
            {
                if(pathes[i] == null)
                {
                    continue;
                }
                var id = candidates[i];
                var profit = (verticesProfit[id] - verticesDanger[id]) / pathes[i].GetLength(map); //при расчете профита учитывать опасность самого пути
                pathes[i].Vertices.ForEach(vIndex =>
                {
                    profit -= verticesDanger[vIndex];
                });
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    maxProfitPath = pathes[i];
                }
            }


            return maxProfitPath;
        }

        private void ChangeCurrentPath(PudgePath path)
        {
            currentPath = path;
        }


        private bool MakeMoveStep()
        {
            if(currentPath.IsPathDone())
            {
                return false;
            }

            var targetIndex = currentPath.GetCurrentTargetIndex();
            currentTarget = map.Vertices[targetIndex];
            if (pudge.MoveTo(currentTarget.X, currentTarget.Y))
            {
                currentPath.VertexReached(targetIndex);
                if (currentPath.IsPathDone())
                {
                    return false;
                }

                targetIndex = currentPath.GetCurrentTargetIndex();
                currentTarget = map.Vertices[targetIndex];
                pudge.MoveTo(currentTarget.X, currentTarget.Y);
            }
            return true;
        }

        private void PeekTarget()
        {
            var target = currentPath.GetCurrentTargetIndex();
            verticesProfit[target] = 0;
        }

        private void LoopBegin()
        {
            closestVertex = null;

            //обработка респавна рун
            if ((int)(pudge.SensorsData.WorldTime / PudgeRules.Current.RuneRespawnTime) > runesRespawnTimes)
            {
                verticesProfit = map.Vertices.Select(v => v.Profit).ToList();
                lastRuneUpdateTime = pudge.SensorsData.WorldTime;
                runesRespawnTimes++;
            }

            if ((pudge.SensorsData.WorldTime - lastDangerUpdateTime) > 5)
            {
                verticesDanger = map.Vertices.Select(v => v.Danger).ToList();
                lastDangerUpdateTime = pudge.SensorsData.WorldTime;
                retreatPathCahce = null;
            }

            if (!pudge.SensorsData.IsDead)
            {
                CorrectVisibleInfo();
            }

        }

        private void CorrectVisibleInfo()
        {
            var runeMinDistSqr = runesMinDistance * runesMinDistance;
            var visibilityRadSqr = PudgeRules.Current.VisibilityRadius * PudgeRules.Current.VisibilityRadius - runeMinDistSqr;
            importantTargetsList.ForEach(index =>
            {
                var vertex = map.Vertices[index];
                if ((Helper.SqrDist(vertex.X, vertex.Y, pudge.Location.X, pudge.Location.Y) < visibilityRadSqr)
                 && (verticesProfit[index] > 0)
                 && (pudge.SensorsData.Map.Runes.Count(r => Helper.SqrDist(vertex.X, vertex.Y, r.Location.X, r.Location.Y) < runeMinDistSqr) == 0))
                {
                    verticesProfit[index] = 0;
                }
            });
        }


        private MapVertex vipTarget = null;
        private bool VipStrategy()
        {
            if(vipTarget == null)
            {
                return false;
            }

            if (pudge.MoveTo(vipTarget.X, vipTarget.Y))
            {
                vipTarget = null;
            }
            return true;
        }


        private bool SlardarStrategy(HeroData slardar)
        {
            if (SlardarHelper.IsUnderAttack(pudge.Location, slardar.Location, slardar.Angle) && pudge.GetInvisibleTime() > 2)
            {
                if (Helper.SqrDist(pudge.Location.X, pudge.Location.Y, slardar.Location.X, slardar.Location.Y) < 200 && pudge.IsHookReady())
                {
                    pudge.HookToWithCorrection(slardar.Location.X, slardar.Location.Y, SlardarRules.Current.MovementVelocity);
                    return true;
                }
            }
            else if (pudge.IsHookReady())
            {
                pudge.HookToWithCorrection(slardar.Location.X, slardar.Location.Y, SlardarRules.Current.MovementVelocity);
                return true;
            }
            else
            {
                var angleDif = Helper.GetAngleDif(pudge.Location.Angle, pudge.GetTargetAngle(slardar.Location.X, slardar.Location.Y));
                if(Math.Abs(angleDif) < 60)
                {
                    Retreat(pudge.Location.X, pudge.Location.Y, slardar.Location.X, slardar.Location.Y);
                }
            }

            return false;
        }


        private bool EnemyPudgeStrategy(HeroData enemyPudge)
        {
            if(pudge.IsHookReady())
            {
                pudge.HookToWithCorrection(enemyPudge.Location.X, enemyPudge.Location.Y, PudgeRules.Current.MovementVelocity);
                return true;
            }
            else
            {
                Retreat(pudge.Location.X, pudge.Location.Y, enemyPudge.Location.X, enemyPudge.Location.Y);
            }

            return false;
        }

        private RetreatPathCache retreatPathCahce = null;
        private bool Retreat(double pudgeX, double pudgeY, double enemyX, double enemyY)
        {
            if (retreatPathCahce != null && retreatPathCahce.IsSuitable(pudgeX, pudgeY, enemyX, enemyY))
            {
                return false;
            }

            var curTargetInd = currentPath.GetCurrentTargetIndex();
            var prevIndex = currentPath.Vertices[Math.Max(currentPath.Vertices.IndexOf(curTargetInd) - 1, 0)];
            verticesDanger[curTargetInd] = 1000;
            lastDangerUpdateTime = pudge.SensorsData.WorldTime;

            bool isNeedUpdate;
            lastProfitHash = double.NegativeInfinity;
            closestVertex = map.Vertices[prevIndex];
            var newPath = FindBestPath(out isNeedUpdate, verticesDanger);
            ChangeCurrentPath(newPath);

            retreatPathCahce = new RetreatPathCache
            {
                PudgeX = pudgeX,
                PudgeY = pudgeY,
                EnemyX = enemyX,
                EnemyY = enemyY,
                RetreatPath = newPath
            };

            return true;
        }
    }
}
