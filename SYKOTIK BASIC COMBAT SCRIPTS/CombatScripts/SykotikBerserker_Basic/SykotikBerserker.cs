using System;
using System.Threading;
using Viper.Scripting.Core.Classes;
using Viper.Scripting.Core.Interfaces;

namespace SykotikValkOverride
{
    internal class SykotikBerserker : CCombat
    {
        /*
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||                                                  ||
        ||               Script Information                 ||
        ||                                                  ||
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                            */
        // Access to BOT API
        public IGame MyHelper;
        

        public override string Name
        {
            get { return "Sykotik Berserker BASIC"; }
        }

        public override string Author
        {
            get { return "Sykotik"; }
        }

        public override string Description
        {
            get { return "Sykotik Berserker BASIC Combat Script"; }
        }

        public override float Version
        {
            get { return 1.0f; }
        }

        public override string Url
        {
            get { return "http://mmoviper.com"; }
        }

        /* ~~~~~~~~~~~~~~~~~~~~~ END OF SCRIPT INFO ~~~~~~~~~~~~~~~~~~~~~~~ */

        /*
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||                                                  ||
        ||                 Custom Helpers                   ||
        ||                                                  ||
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                            */

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


        /*~~~~~~~~~~~~~~~~~~~~~~~~ END OF CUSTOM HELPERS ~~~~~~~~~~~~~~~~~~~~~~~~~~~ */

        /*
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||                                                  ||
        ||          INITIALIZING ABILITIES: FIRST           ||
        ||                                                  ||
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                            */

        private ISpell RAGING_THUNDER = null;
        private ISpell LAVA_PIERCER = null;
        private ISpell ELASTIC_FORCE = null;
        private ISpell SHAKE_OFF = null;
        private ISpell ULTIMATE_FEARSOME_TYRANT = null;
        private ISpell BEASTLY_WIND_SLASH = null;
        private ISpell FRENZIED_DESTROYER = null;
        private ISpell ATTACK = null;

        VipTimer CD_RAGING_THUNDER = new VipTimer();
        VipTimer CD_LAVA_PIERCER = new VipTimer();
        VipTimer CD_ELASTIC_FORCE = new VipTimer();
        VipTimer CD_SHAKE_OFF = new VipTimer();
        VipTimer CD_ULTIMATE_FEARSOME_TYRANT = new VipTimer();
        VipTimer CD_BEASTLY_WIND_SLASH = new VipTimer();
        VipTimer CD_FRENZIED_DESTROYER = new VipTimer();
        VipTimer AutoBuffsScript = new VipTimer();

        /*~~~~~~~~~~~~~~~~~~~~~~~~~ END: INITIALIZE ABILITIES: FIRST ~~~~~~~~~~~~~~~~~~~~~~~~~*/

        /*
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||                                                  ||
        ||            BOT FUNCTIONS: NON COMBAT             ||
        ||                                                  ||
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                            */

        public override void OnPulse()
        {

        }

        public override void OnBotStart(IGame pluginHelper)
        {
            MyHelper = pluginHelper;

            MyHelper.Log.WriteLine("Initializing Skills...");
            RAGING_THUNDER = GetKnownSkillIds("1044, 1175, 1176, 1177, 1178, 1179");
            LAVA_PIERCER = GetKnownSkillIds("213, 214, 215");
            ELASTIC_FORCE = GetKnownSkillIds("1057, 1180, 1181, 1290, 106, 102, 103, 104, 105, 107");
            SHAKE_OFF = GetKnownSkillIds("1040, 1162, 1293, 1294");
            ULTIMATE_FEARSOME_TYRANT = GetKnownSkillIds("1032, 1149, 1150");
            BEASTLY_WIND_SLASH = GetKnownSkillIds("314, 315, 316, 317");
            FRENZIED_DESTROYER = GetKnownSkillIds("1042, 1167, 1168, 1169, 1170, 1171");
            MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");

        }

        public override void OnBotStop()
        {

        }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END OF NON-COMBAT FUNCTIONS ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        /*
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||                                                  ||
        ||               BOT FUNCTION: COMBAT               ||
        ||                                                  ||
        ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                            */
        public override void OnAttack()
        {
            IPlayer selfPlayer = MyHelper.Player;
            IMob monsterActor = MyHelper.Target;
            float actorPosition = monsterActor.DistanceTo(selfPlayer);
            int mobCount = MyHelper.GetAttackers.Count;

            if (AutoBuffsScript.ElapsedMilliseconds > 20)
            {
                MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
                AutoBuffsScript.Reset();
            }

            if (actorPosition > 5)
            {
                MyHelper.Navigation.MoveTo(monsterActor, 4, true);
            }
            else
            {
                MyHelper.Navigation.Stop();

                if (RAGING_THUNDER != null)
                {

                    if (RAGING_THUNDER != null && CD_RAGING_THUNDER.ElapsedMilliseconds > RAGING_THUNDER.Cooldown &&
                        actorPosition < 4 && selfPlayer.MP > 40)
                    {
                        MyHelper.Log.WriteLine("SPINNNNNNN!!!!");
                        UseSkill("{LMB}{RMB}", 7000, true);
                        CD_RAGING_THUNDER.Reset();
                        return;
                    }


                    if (BEASTLY_WIND_SLASH != null && CD_RAGING_THUNDER.ElapsedMilliseconds < RAGING_THUNDER.Cooldown && selfPlayer.MP > 20)
                    {
                        MyHelper.Log.WriteLine("TIME FOR A BEASTLY STRIKE!!");
                        UseSkill("{S}{RMB}", 500, true);
                        return;
                    }


                    if (selfPlayer.CurSP > 15)
                    {
                        MyHelper.Log.WriteLine("MANA LOW!! TIME TO REPLENISH!!");
                        UseSkill("{W}{LMB}", 1250, true);
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("BOTH MP AND SP LOW!! AUTO ATTACK!");
                        UseSkill("{LMB}", 1500, true);
                        return;
                    }
                }
                else
                {

                    if (BEASTLY_WIND_SLASH != null && selfPlayer.MP > 20)
                    {
                        MyHelper.Log.WriteLine("TIME FOR A BEASTLY STRIKE!!");
                        UseSkill("{S}{RMB}", 500, true);
                        return;
                    }

                    if (selfPlayer.CurSP > 15)
                    {
                        MyHelper.Log.WriteLine("MANA LOW!! TIME TO REPLENISH!!");
                        UseSkill("{W}{LMB}", 1250, true);
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("BOTH MP AND SP LOW!! AUTO ATTACK!");
                        UseSkill("{LMB}", 1500, true);
                        return;
                    }
                }
            }
        }
    }

    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ END OF BOT FUNCTION: COMBAT ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    /*
    ||||||||||||||||||||||||||||||||||||||||||||||||||||||
    ||                                                  ||
    ||                BOT FUNCTION: TIMER               ||
    ||                                                  ||
    ||||||||||||||||||||||||||||||||||||||||||||||||||||||
                                                        */

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