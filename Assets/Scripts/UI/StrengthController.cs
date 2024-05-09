using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class StrengthController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _strCounter;

        private IPlayerService _player;

        [Inject]
        public void Initialize(IPlayerService player)
        {
            _player = player;

            _player.GetPowerupController().Strength.Subscribe(str =>
            {
                _strCounter.text = str.ToString();
            });
        }
    }
}
