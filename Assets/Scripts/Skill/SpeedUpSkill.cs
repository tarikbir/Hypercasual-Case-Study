using Zenject;

namespace HypercasualPrototype
{
    public class SpeedUpSkill : Skill
    {
        public int SpeedUpMultiplier { get; private set; }

        private IPlayerService _player;

        public SpeedUpSkill(SkillData data) : base(data)
        {
            SpeedUpMultiplier = data.Power;
        }

        [Inject]
        public void Initialize(IPlayerService player)
        {
            _player = player;
        }

        public override void ActivateSkill()
        {
            base.ActivateSkill();
            _player.GetPowerupController().GainSpeedPowerUp(SpeedUpMultiplier, Cooldown);
        }
    }
}
