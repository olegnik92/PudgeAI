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
    class Strategy1: GameStrategy
    {

        private const double runesMinDistance = 10;

        private MapVertex currentTarget = null;
        private PudgePath currentPath = null;
        private List<double> verticesProfit;
        private List<double> verticesDanger;
        private List<int> importantTargetsList;
        private double lastProfitHash = -1000;


        private double lastRuneUpdateTime;
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
                    if (pudge.IsHookReady())
                    {
                        pudge.HookTo(enemy.Location.X, enemy.Location.Y);
                        continue;
                    }
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

                MakeMoveStep();
                if (currentPath.IsPathDone())
                {
                    PeekTarget();
                }
            }
        }

        private PudgePath FindBestPath(out bool isNeedUpdate)
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

            double maxProfit = 0;
            PudgePath maxProfitPath = null;

            var candidates = importantTargetsList
                .Where(i => verticesProfit[i] - verticesDanger[i] > 0).ToList();

            var pathes = PudgePath.FindPath(candidates, map.IndexOf(ClosestVertex), map);

            for (int i = 0; i < candidates.Count; i++ )
            {
                if(pathes[i] == null)
                {
                    continue;
                }
                var id = candidates[i];
                var profit = (verticesProfit[id] - verticesDanger[id]) / pathes[i].Length; //при расчете профита учитывать опасность самого пути
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
            if(pudge.SensorsData.WorldTime - lastRuneUpdateTime > PudgeRules.Current.RuneRespawnTime)
            {
                verticesProfit = map.Vertices.Select(v => v.Profit).ToList();
                lastRuneUpdateTime = pudge.SensorsData.WorldTime;


                if(!pudge.SensorsData.IsDead)
                {
                    CorrectVisibleInfo();
                }
            }


            //обработка видимых рун
            //if(!pudge.SensorsData.IsDead)
            //{
            //    pudge.SensorsData.Map.Runes.ForEach(rune =>
            //    {
            //        var vertex = Helper.GetСlosestVertex(map, rune.Location.X, rune.Location.Y);
            //        verticesProfit[map.IndexOf(vertex)] = rune.Size == RuneSize.Normal ? 10 : 20;
            //    });
            //}
        }

        private void CorrectVisibleInfo()
        {
            importantTargetsList.ForEach(index =>
            {
                var vertex = map.Vertices[index];
                if ((Helper.SqrDist(vertex.X, vertex.Y, pudge.Location.X, pudge.Location.Y) < PudgeRules.Current.VisibilityRadius * PudgeRules.Current.VisibilityRadius - 1)
                 && (verticesProfit[index] > 0)
                 && (pudge.SensorsData.Map.Runes.Count(r => Helper.SqrDist(vertex.X, vertex.Y, pudge.Location.X, pudge.Location.Y) < runesMinDistance * runesMinDistance) == 0))
                {
                    verticesProfit[index] = 0;
                }
            });
        }
    }
}
