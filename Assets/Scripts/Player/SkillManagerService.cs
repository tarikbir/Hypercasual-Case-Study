using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class SkillManagerService : MonoBehaviour, ISkillManagerService
    {
        public Skill[] ActiveSkills { get; set; } = new Skill[2];

        private IGameManagerService _gameManager;
        private DiContainer _container;

        [Inject]
        public void Initialize(IGameManagerService gameManager, DiContainer container)
        {
            _gameManager = gameManager;
            _container = container;
        }

        private void Update()
        {
            if (ActiveSkills[0] != null)
            {
                ActiveSkills[0].UpdateTimer(Time.deltaTime);
            }
            if (ActiveSkills[1] != null)
            {
                ActiveSkills[1].UpdateTimer(Time.deltaTime);
            }
        }

        public Skill CreateAndGetSkill(int index)
        {
            if (_gameManager == null) Debug.LogError("this is being called before injection...");
            var skillData = _gameManager.SelectedSkillData[index];
            var skill = SkillFactory.CreateSkill(skillData);
            _container.Inject(skill); //This is normally not needed as Bound Factories are a thing, but I've never used one and I haven't understand their usage.
            ActiveSkills[index] = skill;
            return skill;
        }
    }
}