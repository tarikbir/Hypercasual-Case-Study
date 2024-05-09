using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class SkillSlotMenuController : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private int _index;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _icon;

        private IGameManagerService _gameManager;

        [Inject]
        public void Initialize(IGameManagerService gameManager)
        {
            _gameManager = gameManager;
            AssignSkill(_gameManager.SelectedSkillData[_index], true);
        }

        public void AssignSkill(SkillData data, bool initialize = false)
        {
            _name.text = data.name;
            _icon.sprite = data.Icon;

            if (!initialize)
            {
                _gameManager.SelectedSkillData[_index] = data;
                _gameManager.SaveGame();
            }
        }
    }
}