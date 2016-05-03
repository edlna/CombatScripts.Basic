using System;
using System.Threading;
using Viper.Scripting.Core.Classes;
using Viper.Scripting.Core.Interfaces;
// ReSharper disable All

namespace SykotikSorcOverride
{
    class SykotikSorc : CCombat
    {
        ///////////////////////////////// INITIALIZE SKILLS AND TIMERS /////////////////////////////////

        ISpell ABYSSAL_FLAME_ID = null;
        ISpell BEAT_KICK_ID = null;
        ISpell BLACK_WAVE_ID = null;
        ISpell BLOODY_CALAMITY_ID = null;
        ISpell CLAWS_OF_DARKNESS_ID = null;
        ISpell CROW_FLARE_ID = null;
        ISpell DARK_FLAME_ID = null;
        ISpell DREAM_OF_DOOM_ID = null;
        ISpell FLOW_OF_DARKNESS_ID = null;
        ISpell IMMINENT_DOOM_ID = null;
        ISpell MIDNIGHT_STINGER_ID = null;
        ISpell NIGHT_CROW_ID = null;
        ISpell RUSHING_CROW_ID = null;
        ISpell SHARD_EXPLOSION_ID = null;
        ISpell SHARDS_OF_DARKNESS_ID = null;
        ISpell SHIELD_OF_DARKNESS_ID = null;
        ISpell STORMING_CROW_ID = null;
        ISpell ULTIMATE_DARK_FLAME_ID = null;

        VipTimer RUSHING_CROW_CD = new VipTimer();
        VipTimer SHIELD_OF_DARKNESS_CD = new VipTimer();
        VipTimer SHARDS_OF_DARKNESS_CD = new VipTimer();
        VipTimer NIGHT_CROW_CD = new VipTimer();
        VipTimer MIDNIGHT_STINGER_CD = new VipTimer();
        VipTimer CROW_FLARE_CD = new VipTimer();
        VipTimer BLOODY_CALAMITY_CD = new VipTimer();
        VipTimer BLACK_WAVE_CD = new VipTimer();
        VipTimer BEAT_KICK_CD = new VipTimer();
        VipTimer ABYSSAL_FLAME_CD = new VipTimer();
        VipTimer DREAM_OF_DOOM_CD = new VipTimer();
        VipTimer AutoBuffsScript = new VipTimer();

        // Access to bot API
        public IGame MyHelper = null;

        public override string Name
        {
            get { return "Sykotik BASIC Sorcerer"; }
        }

        public override string Author
        {
            get { return "Sykotik"; }
        }

        public override string Description
        {
            get { return "Sykotik BASIC Sorc Combat Script"; }
        }

        public override float Version
        {
            get { return 2.0f; }
        }

        // Optional: Web URL to Script Page
        public override string Url
        {
            get { return "http://mmoviper.com"; }
        }

        public ISpell GetKnownSkillId(string ids)
        {
            ISpell aSpell = MyHelper.Spells.GetMaxLevel(ids);
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

        public override void OnPulse()
        {
        }

        public override void OnBotStart(IGame PluginHelper)
        {
            // Save this to get bot API
            MyHelper = PluginHelper;
            MyHelper.Log.WriteLine("Initialize Spells...");

            ///////////////////////////////// INITIALIZE SKILLS IDS /////////////////////////////////

            ABYSSAL_FLAME_ID = GetKnownSkillId("1054, 1199, 1200");
            BEAT_KICK_ID = GetKnownSkillId("167, 168");
            BLACK_WAVE_ID = GetKnownSkillId("585, 586, 587, 588");
            BLOODY_CALAMITY_ID = GetKnownSkillId("348");
            CLAWS_OF_DARKNESS_ID = GetKnownSkillId("1056, 1202, 1203, 583");
            CROW_FLARE_ID = GetKnownSkillId("164, 165, 166, 95");
            DARK_FLAME_ID = GetKnownSkillId("1204, 1205");
            DREAM_OF_DOOM_ID = GetKnownSkillId("1052, 1195, 93");
            FLOW_OF_DARKNESS_ID = GetKnownSkillId("1411, 1413");
            IMMINENT_DOOM_ID = GetKnownSkillId("1209");
            MIDNIGHT_STINGER_ID = GetKnownSkillId("380, 171, 378");
            NIGHT_CROW_ID = GetKnownSkillId("1055, 1201, 379");
            RUSHING_CROW_ID = GetKnownSkillId("1417, 1418");
            SHARD_EXPLOSION_ID = GetKnownSkillId("1356");
            SHARDS_OF_DARKNESS_ID = GetKnownSkillId("1048, 1183, 1184");
            SHIELD_OF_DARKNESS_ID = GetKnownSkillId("310, 311, 312");
            STORMING_CROW_ID = GetKnownSkillId("1060, 582");
            ULTIMATE_DARK_FLAME_ID = GetKnownSkillId("94");
            MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");

            ///////////////////////////////// END INITIALIZE SKILLS IDS /////////////////////////////////
        }

        public override void OnBotStop()
        {
        }

        ///////////////////////////////// END OF INITIALIZE SKILLS AND TIMERS /////////////////////////////////

        public override void OnAttack()
        {
            // Player Info
            IPlayer selfPlayer = MyHelper.Player;

            // MOB INFO
            IMob mob = MyHelper.Target;
            float actorPosition = mob.DistanceTo(selfPlayer);

            if (AutoBuffsScript.ElapsedMilliseconds > 20)
            {
                MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
                AutoBuffsScript.Reset();
            }

            // MobCount
            int mobCount = MyHelper.GetAttackers.Count;

            // Buff our defenses! //
            if (SHIELD_OF_DARKNESS_ID != null &&
                SHIELD_OF_DARKNESS_CD.ElapsedMilliseconds > SHIELD_OF_DARKNESS_ID.Cooldown)
            {
                MyHelper.Log.WriteLine("SHIELDING UP!");
                UseSkill("{SHIFT}{Q}", 1000, false);
                SHIELD_OF_DARKNESS_CD.Reset();
                return;
            }

            // Buff Damage and Mana Management! //
            if (SHARDS_OF_DARKNESS_ID != null && SHARDS_OF_DARKNESS_CD.ElapsedSeconds > 5
                && selfPlayer.MP < 85)
            {
                MyHelper.Log.WriteLine("Mana UP!");
                UseSkill("{Q}", 100, false);
                SHARDS_OF_DARKNESS_CD.Reset();
                return;
            }

            if (mob.DistanceTo(selfPlayer) > 4)
            {
                // GapClosing Time!!! //
                if (RUSHING_CROW_ID != null && RUSHING_CROW_CD.ElapsedMilliseconds > RUSHING_CROW_ID.Cooldown &&
                    actorPosition > 4 && actorPosition > 8)

                {
                    MyHelper.Log.WriteLine("GapClose Rushing Crow!!");
                    UseSkill("{W}{RMB}", 1000, true);
                    RUSHING_CROW_CD.Reset();
                    return;
                }

                MyHelper.Navigation.MoveTo(mob, 4, true);
                return;
            }
            else
            {
                MyHelper.Navigation.Stop();

                // Black Wave //
                if (BLACK_WAVE_ID != null &&
                    BLACK_WAVE_CD.ElapsedMilliseconds > BLACK_WAVE_ID.Cooldown && selfPlayer.MP > 25
                    && actorPosition < 5)
                {
                    MyHelper.Log.WriteLine("Black Wave!");
                    MyHelper.Input.keysDown("{S}{RMB}");
                    Thread.Sleep(400);
                    UseSkill("{LMB}", 4000, true);

                    MyHelper.Input.keysUp("{S}{RMB}");
                    BLACK_WAVE_CD.Reset();
                    return;
                }

                // Claws of Darkness //
                if (CLAWS_OF_DARKNESS_ID != null && selfPlayer.MP > 15 && actorPosition < 4)
                {
                    MyHelper.Log.WriteLine("Claws of Darkness!");
                    UseSkill("{S}{LMB}", 3000, true);
                    return;
                }

                // Crow Flare and Beat Kick //
                if (CROW_FLARE_ID != null && CROW_FLARE_CD.ElapsedMilliseconds > CROW_FLARE_ID.Cooldown
                    && actorPosition < 4 && selfPlayer.MP > 25)
                {
                    MyHelper.Log.WriteLine("Crow Flare!!");
                    UseSkill("{E}", 100, true);

                    if (BEAT_KICK_ID != null && BEAT_KICK_CD.ElapsedMilliseconds > BEAT_KICK_ID.Cooldown
                        && mob.DistanceTo(selfPlayer) < 4)
                    {
                        MyHelper.Log.WriteLine("Beat Kick Combo!!!!");
                        UseSkill("{F}", 100, true);
                        BEAT_KICK_CD.Reset();
                        return;
                    }
                    CROW_FLARE_CD.Reset();
                    return;
                }

                // Flow Of Darkness: Buff Melee Evasion! //
                if (FLOW_OF_DARKNESS_ID != null && actorPosition < 4 && selfPlayer.CurSP > 150 &&
                    selfPlayer.MP < 25)
                {
                    Random rnd = new Random();
                    int Direction = rnd.Next(1, 3);

                    if (Direction == 1)
                    {
                        MyHelper.Log.WriteLine("Buff Melee Evasion!! To the Left!!");
                        UseSkill("{LMB}{A}", 500, true);
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("Buff Melee Evasion!! To the Right!!");
                        UseSkill("{LMB}{D}", 500, true);
                        return;
                    }

                }
                else
                {
                    // Low on mana? //
                    if (actorPosition < 4 && selfPlayer.MP < 25)
                    {
                        MyHelper.Log.WriteLine("LOW ON MANA!!");
                        UseSkill("{W}{LMB}", 3000, true);
                        return;
                    }
                }
            }
        }
    }
    public class VipTimer
    {
        private long ticks;

        public VipTimer()
        {
            ticks = DateTime.Now.Ticks;
        }

        public double ElapsedMilliseconds
        {
            get
            {
                return Convert.ToDouble(
                    new System.TimeSpan(DateTime.Now.Ticks).TotalMilliseconds
                    - new System.TimeSpan(ticks).TotalMilliseconds);
            }
        }

        public double ElapsedSeconds
        {
            get
            {
                return Convert.ToDouble(
                    new System.TimeSpan(DateTime.Now.Ticks).TotalSeconds
                    - new System.TimeSpan(ticks).TotalSeconds);
            }
        }

        public void Reset()
        {
            ticks = DateTime.Now.Ticks;
        }
    }
}