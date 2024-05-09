namespace HypercasualPrototype
{
    public class SkillFactory
    {
        public static Skill CreateSkill(SkillData data)
        {
            return data.Type switch
            {
                SkillType.SpeedUp => new SpeedUpSkill(data),
                SkillType.StrengthUp => new StrengthSkill(data),
                _ => throw new System.Exception("Unknown skill generated!"),
            };
        }
    }
}
