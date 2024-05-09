using System.Globalization;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class MenuController : MonoBehaviour, IMenuControllerService
    {
        [SerializeField] private TextMeshProUGUI _coinCounter;
        [SerializeField] private SkillSlotMenuController[] _skillControllers;
        [SerializeField] private GameObject _backButton;
        [SerializeField] private GameObject _menuButtons;
        [SerializeField] private GameObject _shopMenu;
        [SerializeField] private GameObject _skillMenu;

        private IGameManagerService _gameManager;

        [Inject]
        public void Initialize(IGameManagerService gameManager)
        {
            _gameManager = gameManager;

            _gameManager.CoinCount.Subscribe(coin =>
            {
                _coinCounter.text = coin.ToString();
            });
        }

        public void BackButton()
        {
            if (_shopMenu.activeInHierarchy)
            {
                _shopMenu.SetActive(false);
            }
            else if (_skillMenu.activeInHierarchy)
            {
                _skillMenu.SetActive(false);
            }

            _menuButtons.SetActive(true);
            _backButton.SetActive(false);
        }

        public void StartGame()
        {
            _gameManager.StartGame();
        }

        public void OpenShop()
        {
            _shopMenu.SetActive(true);
            _backButton.SetActive(true);
            _menuButtons.SetActive(false);
        }

        public void OpenSkillSelection()
        {
            _skillMenu.SetActive(true);
            _backButton.SetActive(true);
            _menuButtons.SetActive(false);
        }

        public void SetSelectedSkill(int index, SkillData skill)
        {
            _skillControllers[index].AssignSkill(skill);
        }
    }
}
