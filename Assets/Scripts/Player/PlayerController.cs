using HypercasualPrototype.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class PlayerController : MonoBehaviour, IPlayerService
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerPathFollower _playerPathFollower;
        [SerializeField] private PlayerPowerupController _playerPowerupController;
        [SerializeField] private float _xLimit;
        [SerializeField] private float _horizontalSpeed = 2f;
        [SerializeField] private float _baseSpeed = 7f;

        private IGameManagerService _gameManager;
        private IInGameUIController _inGameUI;

        [Inject]
        public void Initialize(IGameManagerService gameManager, IInGameUIController inGameUIController)
        {
            _gameManager = gameManager;
            _inGameUI = inGameUIController;


            _playerPowerupController.Strength.Subscribe(str =>
            {
                if (str <= 0)
                {
                    _playerPathFollower.Stop();
                    _inGameUI.ShowDefeatSplash();
                    _gameManager.GameEndTrigger();
                }
            });
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
        }

        public PlayerPowerupController GetPowerupController()
        {
            return _playerPowerupController;
        }

        public void ResetSpeed()
        {
            _playerPathFollower.Speed = _baseSpeed;
        }

        public void SetSpeedTimes(float amount)
        {
            _playerPathFollower.Speed *= amount;
        }

        public void WinConditionTriggered()
        {
            _playerPathFollower.Stop();
            _inGameUI.ShowVictorySplash();
            _gameManager.GameEndTrigger();
        }

        private void MovePlayer()
        {
            Vector2 inputPosition = Input.mousePosition;
            float midScreenX = Screen.width / 2;
            float screenLimitY = 2* Screen.height / 3;
            float x = (inputPosition.x - midScreenX) / midScreenX;
            if (inputPosition.y > screenLimitY) return;
            float position = Mathf.Clamp(x * _xLimit, -_xLimit, _xLimit);

            _playerTransform.localPosition = Vector3.Lerp(_playerTransform.localPosition, new Vector3(position, 0, 0), Time.deltaTime * _horizontalSpeed);
        }
    }
}