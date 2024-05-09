using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class SkillPickerMenuController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private SkillData _skill;
        [SerializeField, Range(0, 1)] private int _index;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;

        private static Color _defaultColor = new Color(0.5660378f, 0.5599139f, 0.3230687f);
        private static Color _disabledColor = new Color(0.490566f, 0.490566f, 0.490566f);

        private bool _isEnabled = true;
        private IGameManagerService _gameManager;
        private IMenuControllerService _menuController;

        [Inject]
        public void Initialize(IMenuControllerService menuController, IGameManagerService gameManager)
        {
            _menuController = menuController;
            _gameManager = gameManager;

            UpdateFrame();
        }

        private void Awake()
        {
            _name.text = _skill.name;
            _icon.sprite = _skill.Icon;
        }

        private void OnEnable()
        {
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            if (_gameManager.SelectedSkillData.Contains(_skill) || _gameManager.PurchasedSkillData.Contains(_skill))
            {
                _frame.color = _defaultColor;
                _isEnabled = true;
            }
            else
            {
                _frame.color = _disabledColor;
                _isEnabled = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isEnabled)
            {
                _menuController.SetSelectedSkill(_index, _skill);
                UpdateFrame();
            }
        }
    }
}