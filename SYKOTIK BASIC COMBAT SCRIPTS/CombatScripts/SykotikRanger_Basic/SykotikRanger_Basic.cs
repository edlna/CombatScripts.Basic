using System;
using System.Threading;
using Viper.Scripting.Core.Classes;
using Viper.Scripting.Core.Interfaces;
// ReSharper disable All

namespace SykotikRangerOverride
{
    internal class SykotikRanger : CCombat
    {
        // Access to bot API
        public IGame MyHelper;

        private ISpell _blastingGustId;
        private VipTimer _blastingGust = new VipTimer();
        private ISpell _chargingWindId;
        private VipTimer _descendingCurrent = new VipTimer();
        private VipTimer _evasiveExplosiveShot = new VipTimer();
        private ISpell _evasiveExplosiveShotId;
        private VipTimer _evasiveShot = new VipTimer();
        private ISpell _evasiveShotId;
        private VipTimer _penetratingWind = new VipTimer();
        private ISpell _penetratingWindId;
        private VipTimer _pinpoint = new VipTimer();
        private ISpell _pinpointId;
        private readonly VipTimer _razorWind = new VipTimer();
        private ISpell _razorWindId;
        private ISpell _ultimateChargingWindId;
        private VipTimer _ultimateEvasiveShot = new VipTimer();
        private ISpell _ultimateEvasiveShotId;
        private readonly VipTimer _willOfTheWind = new VipTimer();
        private ISpell _willOfTheWindId;
        private readonly VipTimer _chargingWind = new VipTimer();
        private readonly VipTimer _ultimateChargingWind = new VipTimer();
        VipTimer AutoBuffsScript = new VipTimer();

        public override string Name
        {
            get { return "Sykotik BASIC Ranger"; }
        }

        public override string Author
        {
            get { return "Sykotik"; }
        }

        public override string Description
        {
            get { return "Sykotik BASIC Ranger Combat Script"; }
        }

        public override float Version
        {
            get { return 3.0f; }
        }

        // Optional Web URL for Plugin Description
        public override string Url
        {
            get { return "http://www.mmoviper.com"; }
        }

        public override void OnPulse()
        {
        }

        public ISpell GetKnownSkillIds(string ids)
        {
            var aSpell = MyHelper.Spells.GetMaxLevel(ids);
            if (aSpell != null)
                MyHelper.Log.WriteLine(aSpell.Name);

            return aSpell;
        }
        public void UseSkill(string key, int delay, bool bFace)
        {
            IMob mob = MyHelper.Target;
            VipTimer faceT = new VipTimer();
            MyHelper.Input.keysDown(key);
            while (faceT.ElapsedMilliseconds < delay)
            {
                if (bFace)
                {
                    MyHelper.Navigation.FaceMob(mob);
                }
                Thread.Sleep(10);
                if (mob.HP == 0)
                {
                    MyHelper.Input.keysUp(key);
                    return;
                }
            }
            MyHelper.Input.keysUp(key);
        }
        public override void OnBotStart(IGame pluginHelper)
        {
            // Save this to get access to Bot API
            MyHelper = pluginHelper;

            MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");

            MyHelper.Log.WriteLine("Initialize Spells...");
            _blastingGustId = GetKnownSkillIds("1077, 1125, 1126");
            _chargingWindId = GetKnownSkillIds("1006, 1091, 1092, 1093");
            _ultimateChargingWindId = GetKnownSkillIds("375");
            _evasiveExplosiveShotId = GetKnownSkillIds("1016, 1116, 1257");
            _evasiveShotId = GetKnownSkillIds("1012, 1107, 1253");
            _ultimateEvasiveShotId = GetKnownSkillIds("374");
            _penetratingWindId = GetKnownSkillIds("1009, 1103, 1104, 1105");
            _pinpointId = GetKnownSkillIds("322, 324");
            _razorWindId = GetKnownSkillIds("1015, 1113, 1114, 1115, 318, 1112");
            _willOfTheWindId = GetKnownSkillIds("1007, 1095, 1096, 1097, 1098");
        }
        public override void OnBotStop()
        {
        }
        public override void OnAttack()
        {
            // Player Info
            IPlayer selfPlayer = MyHelper.Player;

            // What mob is it?
            IMob mob = MyHelper.Target;
            float actorPosition = mob.DistanceTo(selfPlayer);

            if (AutoBuffsScript.ElapsedMilliseconds > 20)
            {
                MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
                AutoBuffsScript.Reset();
            }

            // Mob Count
            int mobCount = MyHelper.GetAttackers.Count;

            if (actorPosition < 15)
            {
                ///////////////////////////////////////////////////////////
                //                                                       //
                //          COMBAT ROUTINE: MOB IN DISTANCE!!!           //
                //                                                       //
                ///////////////////////////////////////////////////////////

                MyHelper.Navigation.Stop();
                // EVASIVE EXPLOSIVE SHOT //
                if (_evasiveExplosiveShotId != null && actorPosition < 5 && selfPlayer.MP > 10)
                {
                    Random rnd = new Random();
                    int Direction = rnd.Next(0, 3);

                    if (Direction == 0)
                    {
                        MyHelper.Log.WriteLine("EES RIGHT!!");
                        UseSkill("{D}{RMB}", 300, true);
                        UseSkill("{LMB}", 100, true);
                        return;
                    }
                    if (Direction == 1)
                    {
                        MyHelper.Log.WriteLine("EES LEFT!!");
                        UseSkill("{A}{RMB}", 300, true);
                        UseSkill("{LMB}", 100, true);
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("EES BACK!!");
                        UseSkill("{S}{RMB}", 300, true);
                        UseSkill("{LMB}", 100, true);
                        return;
                    }
                }

                // BLASTING GUST //
                if (_ultimateEvasiveShotId != null)
                {
                    if (_blastingGustId != null && actorPosition < 15 && selfPlayer.MP > 20)
                    {

                        Random rnd = new Random();
                        int Direction = rnd.Next(0, 2);

                        if (Direction == 0)
                        {
                            MyHelper.Log.WriteLine("BLASTING GUST RIGHT!!");
                            UseSkill("{Q}", 100, true);
                            UseSkill("{D}{LMB}", 200, true);
                            return;
                        }
                        else
                        {
                            MyHelper.Log.WriteLine("BLASTING GUST LEFT!!");
                            UseSkill("{Q}", 100, true);
                            UseSkill("{A}{LMB}", 200, true);
                            return;
                        }
                    }
                }
            
                // RAZOR WIND //
                if (_razorWindId != null && _razorWind.ElapsedMilliseconds < _razorWindId.Cooldown &&
                    selfPlayer.MP > 20 && actorPosition < 15)
                {
                    MyHelper.Log.WriteLine("Razor Wind!");
                    UseSkill("{E}", 400, true);
                    UseSkill("{LMB}", 1250, true);
                    _razorWind.Reset();
                    return;
                }
                if (_evasiveShotId != null && actorPosition < 15)
                {
                    Random rnd = new Random();
                    int Direction = rnd.Next(0, 2);

                    if (Direction == 0)
                    {
                        MyHelper.Log.WriteLine("EVASIVE SHOT RIGHT!!");
                        UseSkill("{LMB}{D}", 700, true);
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("EVASIVE SHOT LEFT!!");
                        UseSkill("{LMB}{A}", 700, true);
                        return;
                    }
                }
                else
                {
                    Random rnd = new Random();
                    int Direction = rnd.Next(0, 2);
                    VipTimer faceT = new VipTimer();

                    MyHelper.Log.WriteLine("We don't know Evasive Shot or PinPoint!!");

                    if (Direction == 0)
                    {
                        MyHelper.Input.keysDown("{LMB}");
                        Thread.Sleep(10);
                        MyHelper.Input.keysDown("{D}");

                        while (faceT.ElapsedMilliseconds < 1500)
                        {
                            MyHelper.Navigation.FaceMob(mob);
                            Thread.Sleep(10);

                            if (mob.HP == 0)
                            {
                                MyHelper.Input.keysUp("{LMB}{D}");
                                return;
                            }
                        }
                        MyHelper.Input.keysUp("{LMB}{D}");
                        return;
                    }
                    else
                    {
                        MyHelper.Input.keysDown("{LMB}");
                        Thread.Sleep(10);
                        MyHelper.Input.keysDown("{A}");

                        while (faceT.ElapsedMilliseconds < 1500)
                        {
                            MyHelper.Navigation.FaceMob(mob);
                            Thread.Sleep(10);

                            if (mob.HP == 0)
                            {
                                MyHelper.Input.keysUp("{LMB}{A}");
                                return;
                            }
                        }
                        MyHelper.Input.keysUp("{LMB}{A}");
                        return;
                    }
                }

            }
            else
            {
                MyHelper.Navigation.MoveTo(mob, 12, true);
            }
        }
    }

    public class VipTimer
    {
        private long _ticks;

        public VipTimer()
        {
            _ticks = DateTime.Now.Ticks;
        }

        public double ElapsedMilliseconds
        {
            get
            {
                return Convert.ToDouble(
                    new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds
                    - new TimeSpan(_ticks).TotalMilliseconds);
            }
        }

        public double ElapsedSeconds
        {
            get
            {
                return Convert.ToDouble(
                    new TimeSpan(DateTime.Now.Ticks).TotalSeconds
                    - new TimeSpan(_ticks).TotalSeconds);
            }
        }

        public void Reset()
        {
            _ticks = DateTime.Now.Ticks;
        }
    }
}
