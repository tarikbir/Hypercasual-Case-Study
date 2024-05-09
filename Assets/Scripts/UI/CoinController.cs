using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinCounter;
        
        private IGameManagerService _gameManager;
        private IAudioService _audioService;

        [Inject]
        public void Initialize(IGameManagerService gameManager, IAudioService audioService)
        {
            _gameManager = gameManager;
            _audioService = audioService;

            _gameManager.CoinCount.Subscribe(coin =>
            {
                _coinCounter.text = coin.ToString();
                _audioService.Play(AudioService.COIN_GRAB);
            });
        }
    }
}