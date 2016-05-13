using PudgeClient.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    abstract class GameStrategy
    {
        protected PudgeController pudge;

        protected MapGraph map;

        public GameStrategy(PudgeController pudge, MapGraph map)
        {
            this.pudge = pudge;
            this.map = map;
        }


        public abstract void Run();

    }
}
