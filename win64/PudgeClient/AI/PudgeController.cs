using AIRLab.Mathematics;
using CVARC.V2;
using Pudge;
using Pudge.Player;
using Pudge.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudgeClient.AI
{
    class PudgeController
    {
        public const double PudgeSize = 20;
        public const double MoveEps = 1;
        public const double RotEps = 2;
        public const double MoveStep = 10;
        public const double RotStep = 15;


        private PudgeClient<PudgeSensorsData> client;
        private PudgeSensorsData sensorsData;

        public PudgeSensorsData SensorsData
        {
            get
            {
                return sensorsData;
            }
        }


        public PudgeController(PudgeClient<PudgeSensorsData> client, PudgeSensorsData initialData)
        {
            this.client = client;
            UpdateData(initialData);
            client.SensorDataReceived += UpdateData;
        }

        public bool MoveTo(double x, double y)
        {
            if (sensorsData.IsDead)
            {
                return false;
            }

            var sqrDist = Helper.SqrDist(x, y, sensorsData.SelfLocation.X, sensorsData.SelfLocation.Y);
            if (sqrDist < MoveEps)
            {
                return true;
            }

            var targetAngle = GetTargetAngle(x, y);
            if (RotateTo(targetAngle))
            {
                client.Move(Math.Min(Math.Sqrt(sqrDist), MoveStep));
            }
            return false;
        }


        public bool RotateTo(double angle)
        {
            if (sensorsData.IsDead)
            {
                return false;
            }

            angle = Helper.NormalizeAngle(angle);
            var curAngle = Helper.NormalizeAngle(sensorsData.SelfLocation.Angle);
            var dif = Helper.GetAngleDif(curAngle, angle);
            if (Math.Abs(dif) < RotEps)
            {
                return true;
            }


            client.Rotate(Math.Sign(dif) * Math.Min(Math.Abs(dif), RotStep));
            return false;
        }

        public bool HookTo(double x, double y)
        {
            if (sensorsData.IsDead)
            {
                return false;
            }

            if (!IsHookReady())
            {
                return false;
            }

            var targetAngle = GetTargetAngle(x, y);

            if (RotateTo(targetAngle))
            {
                client.Hook();
                return true;
            }
            
            return false;
        }

        public double GetTargetAngle(double x, double y)
        {
            return Helper.GetMoveAngle(x - sensorsData.SelfLocation.X, y - sensorsData.SelfLocation.Y);
        }

        public bool IsHookThrown()
        {
            if (sensorsData.IsDead)
            {
                return false;
            }

            return sensorsData.Events.Any(e => e.Event == PudgeEvent.HookThrown);
        }

        public bool IsHookReady()
        {
            if (sensorsData.IsDead)
            {
                return false;
            }

            return !sensorsData.Events.Any(e => e.Event == PudgeEvent.HookCooldown);
        }

        public double GetHookReadyTime()
        {
            if (sensorsData.IsDead)
            {
                return -1;
            }

            var hookEvent = sensorsData.Events.FirstOrDefault(e => e.Event == PudgeEvent.HookCooldown);
            if (hookEvent == null)
            {
                return 0;
            }

            return hookEvent.Duration - (sensorsData.WorldTime - hookEvent.Start);
        }

        public void Wait(double time = 0.1)
        {
            client.Wait(time);
        }

        public void Sleep()
        {
            Wait(0.1);
        }

        public LocatorItem Location
        {
            get
            {
                return sensorsData.SelfLocation;
            }
        }

        private void UpdateData(PudgeSensorsData newSensorsData)
        {
            this.sensorsData = newSensorsData;
        }

        public double GetInvisibleTime()
        {
            if(sensorsData.IsDead)
            {
                return 0;
            }
            var invisibleEffect = sensorsData.Events.FirstOrDefault(e => e.Event == PudgeEvent.Invisible);
            if(invisibleEffect == null)
            {
                return 0;
            }

            return invisibleEffect.Duration - (sensorsData.WorldTime - invisibleEffect.Start);
        }
    }
}
