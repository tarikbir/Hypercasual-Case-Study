using Zenject;

namespace HypercasualPrototype
{
    public class StrengthSkill : Skill
    {
        public int StrengthMultiplier { get; private set; }

        private IPlayerService _player;

        public StrengthSkill(SkillData data) : base(data)
        {
            StrengthMultiplier = data.Power;
        }

        [Inject]
        public void Initialize(IPlayerService player)
        {
            _player = player;
        }

        public override void ActivateSkill()
        {
            base.ActivateSkill();
            _player.GetPowerupController().GainStrength(StrengthMultiplier, OpType.Multiplication);
        }
    }
}