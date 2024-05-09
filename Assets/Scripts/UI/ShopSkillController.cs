using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class ShopSkillController : MonoBehaviour
    {
        [SerializeField] private SkillData _skill;
        [SerializeField] private int _price;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Image _frame;
        [SerializeField] private Image _icon;

        private static Color _defaultColor = Color.white;
        private static Color _disabledColor = new Color(0.490566f, 0.490566f, 0.490566f);

        private bool _isEnabled = true;
        private IGameManagerService _gameManager; //Logic shouldn't be here (MVC/MVVM), but I'm trying to make this in 3 days.

        [Inject]
        public void Initialize(IGameManagerService gameManager)
        {
            _gameManager = gameManager;

            UpdateFrame();
        }

        private void Awake()
        {
            _name.text = _skill.name;
            _icon.sprite = _skill.Icon;
            _priceText.text = _price.ToString();
        }

        private void OnEnable()
        {
            UpdateFrame();
        }

        public void BuyItem()
        {
            if (_isEnabled && _gameManager.CoinCount.Value >= _price)
            {
                _gameManager.CoinCount.Value = _gameManager.CoinCount.Value - _price;
                _gameManager.PurchasedSkillData.Add(_skill);
                _gameManager.SaveGame();
                UpdateFrame();
            }
        }

        private void UpdateFrame()
        {
            if (!_gameManager.PurchasedSkillData.Contains(_skill))
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
    }
}
