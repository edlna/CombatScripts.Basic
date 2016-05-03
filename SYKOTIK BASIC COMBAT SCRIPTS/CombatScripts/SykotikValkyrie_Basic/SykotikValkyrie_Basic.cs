using System;
using System.Threading;
using Viper.Scripting.Core.Classes;
using Viper.Scripting.Core.Interfaces;
// ReSharper disable All

namespace SykotikValkOverride
{
    internal class SykotikValk : CCombat
    {
        private readonly VipTimer _breathOfElionCd = new VipTimer();
        private readonly VipTimer _celestialSpearCd = new VipTimer();
        private readonly VipTimer _chargingSlashCd = new VipTimer();
        private readonly VipTimer _flowShieldThrowCd = new VipTimer();
        private readonly VipTimer _heavensEchoCd = new VipTimer();
        private readonly VipTimer _judgementOfLightCd = new VipTimer();
        private readonly VipTimer _justCounterCd = new VipTimer();
        private readonly VipTimer _righteousChargeCd = new VipTimer();
        private readonly VipTimer _severingLightCd = new VipTimer();
        private readonly VipTimer _shieldChaseCd = new VipTimer();
        private readonly VipTimer _shieldThrowCd = new VipTimer();

        private ISpell _breathOfElion;
        private ISpell _celestialSpear;
        private ISpell _chargingSlash;
        private ISpell _divinePower;
        private VipTimer _divinePowerCd = new VipTimer();
        private ISpell _flowShieldThrow;
        private ISpell _flurryOfKicks;
        private VipTimer _flurryOfKicksCd = new VipTimer();
        private ISpell _flyingKick;
        private VipTimer _flyingKickCd = new VipTimer();
        private ISpell _forwardSlash;
        private VipTimer _forwardSlashCd = new VipTimer();
        private ISpell _glaringSlash;
        private VipTimer _glaringSlashCd = new VipTimer();
        private ISpell _heavensEcho;
        private ISpell _judgementOfLight;
        private ISpell _justCounter;
        private ISpell _righteousCharge;
        private ISpell _severingLight;
        private ISpell _sharpLight;
        private VipTimer _sharpLightCd = new VipTimer();
        private ISpell _shieldChase;
        private ISpell _shieldStrike;
        private VipTimer _shieldStrikeCd = new VipTimer();
        private ISpell _shieldThrow;
        private ISpell _shiningDash;
        private VipTimer _shiningDashCd = new VipTimer();
        private ISpell _sidewaysCut;
        private ISpell _skywardStrike;
        private VipTimer _skywardStrikeCd = new VipTimer();
        private ISpell _swordOfJudgement;
        VipTimer AutoBuffsScript = new VipTimer();

        // Access to BOT API
        public IGame MyHelper;

        public override string Name
        {
            get { return "Sykotik Valkyrie"; }
        }

        public override string Author
        {
            get { return "Sykotik"; }
        }

        public override string Description
        {
            get { return "Sykotik Valkyrie Combat Script"; }
        }

        public override float Version
        {
            get { return 3.0f; }
        }

        // Optional: Web URL to Script Page
        public override string Url
        {
            get { return "http://mmoviper.com"; }
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
            var mob = MyHelper.Target;
            var faceT = new VipTimer();
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

        public override void OnBotStart(IGame pluginHelper)
        {
            // Save this to get bot API
            MyHelper = pluginHelper;
            MyHelper.Log.WriteLine("Initialize Spells...");

            _breathOfElion = GetKnownSkillIds("762, 763, 764");
            _celestialSpear = GetKnownSkillIds("765, 766, 767, 768, 775");
            _chargingSlash = GetKnownSkillIds("747, 748, 749");
            _divinePower = GetKnownSkillIds("739, 740, 741, 742");
            _flurryOfKicks = GetKnownSkillIds("727, 728");
            _flyingKick = GetKnownSkillIds("1476, 1477, 1478");
            _forwardSlash = GetKnownSkillIds("1476, 1477, 1478");
            _glaringSlash = GetKnownSkillIds("757, 758, 759, 760, 761");
            _heavensEcho = GetKnownSkillIds("744, 745, 746");
            _judgementOfLight = GetKnownSkillIds("772, 773, 774");
            _justCounter = GetKnownSkillIds("720, 721, 722");
            _righteousCharge = GetKnownSkillIds("750, 751, 752, 753");
            _severingLight = GetKnownSkillIds("1479, 1480, 1481, 1482, 1483");
            _sharpLight = GetKnownSkillIds("1490, 1491, 1492, 1493, 771");
            _shieldChase = GetKnownSkillIds("736, 737, 738");
            _shieldStrike = GetKnownSkillIds("1497, 1498, 1499");
            _shieldThrow = GetKnownSkillIds("1485, 1486");
            _shiningDash = GetKnownSkillIds("731");
            _sidewaysCut = GetKnownSkillIds("1487, 1488, 1489");
            _skywardStrike = GetKnownSkillIds("754, 755, 756");
            _swordOfJudgement = GetKnownSkillIds("732, 733, 734, 735, 770");
            _flowShieldThrow = GetKnownSkillIds("784");
            MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
        }

        public override void OnBotStop()
        {
        }

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

            // Buff Defense! Heavens Echo! //
            if (_heavensEcho != null && _heavensEchoCd.ElapsedSeconds > 20 && selfPlayer.MP >= 10)
            {
                MyHelper.Log.WriteLine("Buffing Defenses!");
                UseSkill("{SHIFT}{Q}", 200, false);
                _heavensEchoCd.Reset();
                return;
            }

            if (actorPosition > 5)
            {
                // CHARGE!! //
                if (_righteousCharge != null && actorPosition < 8 &&
                    _righteousChargeCd.ElapsedMilliseconds > _righteousCharge.Cooldown && selfPlayer.MP >= 15)
                {
                    MyHelper.Log.WriteLine("CHARGE!!");
                    UseSkill("{W}{F}", 1000, true);
                    _righteousChargeCd.Reset();
                }
                else if (_celestialSpear != null && _celestialSpearCd.ElapsedMilliseconds > _celestialSpear.Cooldown &&
                         actorPosition < 8 && actorPosition > 4 && selfPlayer.MP >= 15)
                {
                    MyHelper.Log.WriteLine("PULLING WITH CELESTIAL SPEAR INSTEAD!!");
                    UseSkill("{S}{E}", 600, true);
                    _celestialSpearCd.Reset();
                    return;
                }
                else
                {
                    MyHelper.Navigation.MoveTo(mob, 5, true);
                }
            }
            else
            {
                MyHelper.Navigation.Stop();

                if (_breathOfElion != null && _breathOfElionCd.ElapsedMilliseconds > _breathOfElion.Cooldown &&
                    selfPlayer.MP >= 10 && selfPlayer.HP < 50)
                {
                    MyHelper.Log.WriteLine("LOW HP, HEAL FIRST!");
                    UseSkill("{SHIFT}{E}", 1000, true);
                    _breathOfElionCd.Reset();
                    return;
                }

                // SHIELD THROW!! //
                if (_shieldThrow != null && _shieldThrowCd.ElapsedMilliseconds > _shieldThrow.Cooldown &&
                    actorPosition < 6 && selfPlayer.MP >= 15)
                {
                    if (_flowShieldThrow != null &&
                        _flowShieldThrowCd.ElapsedMilliseconds > _flowShieldThrow.Cooldown && selfPlayer.MP >= 15)
                    {
                        MyHelper.Log.WriteLine("FLOW: SHIELD THROW!!");
                        UseSkill("{S}{Q}", 400, true);
                        UseSkill("{RMB}", 1500, true);
                        _shieldThrowCd.Reset();
                        _flowShieldThrowCd.Reset();
                        return;
                    }
                    else
                    {
                        MyHelper.Log.WriteLine("SHIELD THROW!!");
                        UseSkill("{S}{Q}", 1000, true);
                        _shieldThrowCd.Reset();
                        return;
                    }
                }

                // CELESTIAL SPEAR!! //
                if (_celestialSpear != null && _celestialSpearCd.ElapsedMilliseconds > _celestialSpear.Cooldown &&
                    selfPlayer.MP > 20)
                {
                    MyHelper.Log.WriteLine("SPEAR THEM!!");
                    UseSkill("{S}{E}", 600, true);
                    _celestialSpearCd.Reset();
                    return;
                }

                // SWORD OF JUDGEMENT!! //
                if (_swordOfJudgement != null && selfPlayer.MP >= 20 && actorPosition < 4)
                {
                    MyHelper.Log.WriteLine("SOJ SPAM!!!");
                    UseSkill("{S}{RMB}", 1250, true);
                    return;
                }

                // MANA LOW!? //
                if (_forwardSlash != null && selfPlayer.MP < 30 && mob.DistanceTo(MyHelper.Player) < 4)
                {
                    MyHelper.Log.WriteLine("MANA LOW! FORWARD SLASH!!");
                    UseSkill("{W}{LMB}", 1250, true);
                    return;
                }
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