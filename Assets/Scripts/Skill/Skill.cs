using UniRx;
using UnityEngine;

namespace HypercasualPrototype
{
    public abstract class Skill
    {
        public int UseAmount;
        public Sprite Icon;
        public float Cooldown;

        public bool IsReady = true;
        public FloatReactiveProperty CooldownTimer = new FloatReactiveProperty(0);

        protected Skill(SkillData data)
        {
            UseAmount = data.BaseAmount;
            Icon = data.Icon;
            Cooldown = data.Cooldown;
        }

        public virtual bool CanActivateSkill()
        {
            return UseAmount > 0;
        }

        public virtual void ActivateSkill()
        {
            UseAmount -= 1;
            CooldownTimer.Value = Cooldown;
            IsReady = false;
        }

        public void UpdateTimer(float deltaTime)
        {
            if (!IsReady)
            {
                CooldownTimer.Value -= deltaTime;

                if (CooldownTimer.Value <= 0)
                {
                    IsReady = true;
                }
            }
        }
    }
}