namespace HypercasualPrototype
{
    public interface ISkillManagerService
    {
        Skill[] ActiveSkills { get; set; }
        Skill CreateAndGetSkill(int index);
    }
}
