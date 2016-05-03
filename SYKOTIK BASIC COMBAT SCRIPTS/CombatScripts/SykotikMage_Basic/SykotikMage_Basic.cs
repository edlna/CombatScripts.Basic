using System;
using System.Threading;
using Viper.Scripting.Core.Classes;
using Viper.Scripting.Core.Interfaces;

namespace SykotikMage
{
    internal class SykotikMage : CCombat
    {
        ///////////////////////////////////////////////////

        // SPELL TIMERS //
        private readonly VipTimer _blizzardCd = new VipTimer();

        // INITIALIZE SPELLS //
        private ISpell _blizzard;
        private ISpell _concentratedMagicArrow;
        private VipTimer _concentratedMagicArrowCd = new VipTimer();
        private ISpell _earthquake;
        private readonly VipTimer _earthquakeCd = new VipTimer();
        private ISpell _fireball;
        private readonly VipTimer _fireballCd = new VipTimer();
        private ISpell _fireballExplosion;
        private readonly VipTimer _fireballExplosionCd = new VipTimer();
        private ISpell _freeze;
        private readonly VipTimer _freezeCd = new VipTimer();
        private ISpell _frigidFog;
        private VipTimer _frigidFogCd = new VipTimer();
        private ISpell _healingAura;
        private readonly VipTimer _healingAuraCd = new VipTimer();
        private ISpell _healingLighthouse;
        private VipTimer _healingLighthouseCd = new VipTimer();
        private ISpell _lightning;
        private readonly VipTimer _lightningCd = new VipTimer();
        private ISpell _lightningChain;
        private ISpell _lightningStorm;
        private readonly VipTimer _lightningStormCd = new VipTimer();
        private ISpell _magicalShield;
        private readonly VipTimer _magicalShieldCd = new VipTimer();
        private ISpell _magicArrow;
        private ISpell _magicLighthouse;
        private VipTimer _magicLighthouseCd = new VipTimer();
        private ISpell _manaAbsorption;
        private readonly VipTimer _manaAbsorptionCd = new VipTimer();
        private ISpell _meteorShower;
        private readonly VipTimer _meteorShowerCd = new VipTimer();
        private ISpell _multipleMagicArrows;
        private readonly VipTimer _multipleMagicArrowsCd = new VipTimer();
        private ISpell _redidualLightning;
        private readonly VipTimer _residualLightningCd = new VipTimer();
        private ISpell _spellboundHeart;
        private VipTimer _spellboundHeartCd = new VipTimer();
        private ISpell _ultimateBlizzard = null;
        private VipTimer _ultimateBlizzardCd = new VipTimer();
        VipTimer AutoBuffsScript = new VipTimer();
        // BOT API ACCESS //
        public IGame MyHelper;

        ///////////////////////////////////////////////////////////

        public override string Name
        {
            get { return "Sykotik Mage BASIC"; }
        }

        public override string Author
        {
            get { return "Sykotik"; }
        }

        public override string Description
        {
            get { return "Sykotik BASIC Mage Combat Script"; }
        }

        public override float Version
        {
            get { return 1.0f; }
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

        public override void OnBotStart(IGame pluginHelper)
        {
            // Save this to get access to Bot API
            MyHelper = pluginHelper;

            MyHelper.Log.WriteLine("Initialize Spells...");
            _blizzard = GetKnownSkillIds("843, 844, 845, 846");
            _concentratedMagicArrow = GetKnownSkillIds("887, 888, 889");
            _earthquake = GetKnownSkillIds("786, 787, 788, 789");
            _fireballExplosion = GetKnownSkillIds("847, 848, 849");
            _fireball = GetKnownSkillIds("818, 819, 820, 821");
            _freeze = GetKnownSkillIds("834, 835, 836, 837, 838");
            _frigidFog = GetKnownSkillIds("839, 840, 841, 842");
            _healingAura = GetKnownSkillIds("899, 900, 901, 902, 903");
            _healingLighthouse = GetKnownSkillIds("793, 794, 795, 796");
            _lightningChain = GetKnownSkillIds("827, 828, 829, 830");
            _lightning = GetKnownSkillIds("822, 823, 824, 825, 826");
            _lightningStorm = GetKnownSkillIds("831, 832, 833");
            _magicArrow = GetKnownSkillIds("850, 851, 852, 853, 854");
            _magicLighthouse = GetKnownSkillIds("1620, 1621, 1622");
            _magicalShield = GetKnownSkillIds("868, 869, 870, 871");
            _manaAbsorption = GetKnownSkillIds("865, 866, 867");
            _meteorShower = GetKnownSkillIds("790, 791, 792");
            _multipleMagicArrows = GetKnownSkillIds("855");
            _redidualLightning = GetKnownSkillIds("856, 857, 858, 859");
            _spellboundHeart = GetKnownSkillIds("904, 905, 906, 907, 908");
            MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
        }

        public override void OnBotStop()
        {
        }

        public override void OnAttack()
        {
            // Player Info
            var selfPlayer = MyHelper.Player;
            // What mob is it?
            var mob = MyHelper.Target;
            var actorPosition = mob.DistanceTo(selfPlayer);

            // Mob Count
            var mobCount = MyHelper.GetAttackers.Count;

            if (AutoBuffsScript.ElapsedMilliseconds > 20)
            {
                MyHelper.BDOLua("runLua(\"scripts//autobuffs.lua\") ");
                AutoBuffsScript.Reset();
            }

            if (_magicalShield != null && _magicalShieldCd.ElapsedMilliseconds > _magicalShield.Cooldown &&
                selfPlayer.MP > 30)
            {
                MyHelper.Log.WriteLine("CASTING DEFENSES!!");
                UseSkill("{Q}", 1000, false);
                _magicalShieldCd.Reset();
                return;
            }

            if (actorPosition > 13)
            {
                MyHelper.Navigation.MoveTo(mob, 12, true);
            }
            else
            {
                MyHelper.Navigation.Stop();

                if (_manaAbsorption != null && _manaAbsorptionCd.ElapsedMilliseconds > _manaAbsorption.Cooldown &&
                    selfPlayer.MP < 50)
                {
                    MyHelper.Log.WriteLine("MANA LOW!! CASTING MANA ABSORPTION!!");
                    UseSkill("{SHIFT}{LMB}", 2500, true);
                    _manaAbsorptionCd.Reset();
                    return;
                }

                if (_fireball != null && _fireballCd.ElapsedSeconds > 12 && selfPlayer.MP > 20)
                {
                    if (_fireballExplosion != null &&
                        _fireballExplosionCd.ElapsedMilliseconds > _fireballExplosion.Cooldown && selfPlayer.MP > 20)
                    {
                        MyHelper.Log.WriteLine("FIREBALL COMBO!!");
                        UseSkill("{S}{LMB}", 1250, true);
                        UseSkill("{LMB}", 400, true);
                        UseSkill("{RMB}", 600, true);
                        _fireballCd.Reset();
                        _fireballExplosionCd.Reset();
                        return;
                    }
                    MyHelper.Log.WriteLine("CASTING FIREBALL!!");
                    UseSkill("{S}{LMB}", 1250, true);
                    UseSkill("{LMB}", 400, true);
                    _fireballCd.Reset();
                    return;
                }

                if (_lightning != null && _lightningCd.ElapsedMilliseconds > _lightning.Cooldown && selfPlayer.MP > 20)
                {
                    if (_redidualLightning != null &&
                        _residualLightningCd.ElapsedMilliseconds > _redidualLightning.Cooldown && selfPlayer.MP > 20)
                    {
                        MyHelper.Log.WriteLine("USING LIGHTNING COMBO!!");
                        UseSkill("{S}{F}", 1250, true);
                        UseSkill("{RMB}", 800, true);
                        _lightningCd.Reset();
                        _residualLightningCd.Reset();
                        return;
                    }
                    MyHelper.Log.WriteLine("CASTING LIGHTNING!!");
                    UseSkill("{S}{F}", 1250, true);
                    _lightningCd.Reset();
                    return;
                }

                if (_blizzard != null && _blizzardCd.ElapsedMilliseconds > _blizzard.Cooldown && mobCount >= 3 &&
                    selfPlayer.MP > 40)
                {
                    MyHelper.Log.WriteLine("CASTING BLIZZARD!!!");
                    UseSkill("{SHIFT}{LMB}{RMB}", 2500, true);
                    UseSkill("{LMB}", 4000, true);
                    _blizzardCd.Reset();
                    return;
                }

                if (_freeze != null && _freezeCd.ElapsedMilliseconds > _freeze.Cooldown && selfPlayer.MP > 20 &&
                    actorPosition < 8)
                {
                    MyHelper.Log.WriteLine("MOB TOO CLOSE!! FREEZE!");
                    UseSkill("{S}{E}", 1000, true);
                    _freezeCd.Reset();
                    return;
                }

                if (_lightningChain != null && mobCount >= 2 && selfPlayer.MP > 20)
                {
                    MyHelper.Log.WriteLine("USING CHAIN LIGHTNING!!");
                    UseSkill("{SHIFT}{RMB}", 2500, true);

                    if (_lightningStorm != null && _lightningStormCd.ElapsedMilliseconds > _lightningStorm.Cooldown &&
                        selfPlayer.MP > 20)
                    {
                        MyHelper.Log.WriteLine("CASTING LIGHTING STORM!!");
                        UseSkill("{LMB}{RMB}", 1500, true);
                        _lightningStormCd.Reset();
                        return;
                    }
                    return;
                }

                if (_manaAbsorption != null && _manaAbsorptionCd.ElapsedMilliseconds > _manaAbsorption.Cooldown &&
                    selfPlayer.MP < 50)
                {
                    MyHelper.Log.WriteLine("MANA LOW! CASTING MANA ABSORPTION!!");
                    UseSkill("{SHIFT}{RMB}", 2500, true);
                    _manaAbsorptionCd.Reset();
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
                return
                    Convert.ToDouble(new TimeSpan(DateTime.Now.Ticks).TotalMilliseconds -
                                     new TimeSpan(_ticks).TotalMilliseconds);
            }
        }

        public double ElapsedSeconds
        {
            get
            {
                return
                    Convert.ToDouble(new TimeSpan(DateTime.Now.Ticks).TotalSeconds - new TimeSpan(_ticks).TotalSeconds);
            }
        }

        public void Reset()
        {
            _ticks = DateTime.Now.Ticks;
        }
    }
}