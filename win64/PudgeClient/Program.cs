using System;
using System.Linq;
using Pudge;
using Pudge.Player;
using Pudge.World;
using PudgeClient.AI;
using PudgeClient.Map;

namespace PudgeClient
{
    class Program
    {
        const string CvarcTag = "7508ea6f-bafe-45ab-90eb-fab333118ae2";

        static void Print(PudgeSensorsData data)
        {
            Console.WriteLine("---------------------------------");
            if (data.IsDead)
            {
                Console.WriteLine("Ooops, i'm dead :(");
                return;
            }
            Console.WriteLine("I'm here: " + data.SelfLocation);
            Console.WriteLine("My score now: {0}", data.SelfScores);
            Console.WriteLine("Current time: {0:F}", data.WorldTime);
            foreach (var rune in data.Map.Runes)
                Console.WriteLine("Rune! Type: {0}, Size = {1}, Location: {2}", rune.Type, rune.Size, rune.Location);
            foreach (var heroData in data.Map.Heroes)
                Console.WriteLine("Enemy! Type: {0}, Location: {1}, Angle: {2:F}", heroData.Type, heroData.Location, heroData.Angle);
            foreach (var eventData in data.Events)
                Console.WriteLine("I'm under effect: {0}, Duration: {1}", eventData.Event,
                    eventData.Duration - (data.WorldTime - eventData.Start));
            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            var seed = new Map.MapSeed();
            var map = seed.CreateMapGraph();
            PudgePath.BuildSearchMapCache(map);

            if (args.Length == 0)
                args = new[] { "127.0.0.1", "14000" };
            var ip = args[0];
            var port = int.Parse(args[1]);

            Console.WriteLine("IP: {0}", ip);
            Console.WriteLine("Port: {0}", port);

            var client = new PudgeClientLevel3();
            client.SensorDataReceived += Print;
            var sensorsData  = client.Configurate(ip, port, CvarcTag, isOnLeftSide: true, speedUp: true, operationalTimeLimit: 4000, seed: 217);
            var pudge = new AI.PudgeController(client, sensorsData);
            var strategy = new Strategy1(pudge, map);

            try
            {
                strategy.Run();
            } 
            catch(Exception exc)
            {
                Console.WriteLine("Program finished with exception:");
                Console.WriteLine(exc.Message);           
            }
            finally
            {
                client.Exit();
            }
            
        }
    }
}
