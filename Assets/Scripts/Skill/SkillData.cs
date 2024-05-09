using UnityEngine;

namespace HypercasualPrototype
{
    [CreateAssetMenu(menuName = "Data/Skill Data", fileName = "New Skill Data")]
    public class SkillData : ScriptableObject
    {
        public int Power;
        public SkillType Type;
        public int BaseAmount;
        public float Cooldown;
        public Sprite Icon;
    }

    public enum SkillType
    {
        SpeedUp,
        StrengthUp
    }
}
