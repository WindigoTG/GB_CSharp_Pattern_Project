using UnityEngine;

namespace ShmupProject
{
    public class BossFactory
    {
        public BossPart CreatePart(BossPartType type)
        {
            switch (type)
            {
                case BossPartType.SpreadGun:
                    return CreateSpreadGun();

                case BossPartType.RotaryGun:
                    return CreateRotaryGun();

                case BossPartType.Random:
                default:
                    {
                        int rnd = Random.Range(0, 2);
                        if (rnd == 0)
                            return CreateSpreadGun();
                        else
                            return CreateRotaryGun();
                    }
            }
        }

        private BossPart CreateSpreadGun()
        {
            return new BossSpreadgunWing();
        }

        private BossPart CreateRotaryGun()
        {
            return new BossRotaryWeaponPart();
        }
    }

    public enum BossPartType
    {
        SpreadGun,
        RotaryGun,
        Random
    }
}