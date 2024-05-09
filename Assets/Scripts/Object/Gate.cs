using TMPro;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private OpType _operation;
        [SerializeField] private TextMeshPro _text;

        private bool _isEnabled = true;
        private IPlayerService _player;

        [Inject]
        public void Initialize(IPlayerService player)
        {
            _player = player;
        }

        private void Awake()
        {
            string opText = _operation switch
            {
                OpType.Addition => "+",
                OpType.Subtraction => "-",
                OpType.Multiplication => "x",
                OpType.Division => "÷",
                _ => ""
            };

            _text.text = $"{opText}{_value}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isEnabled && other.CompareTag(Constants.PlayerTag))
            {
                ApplyToPlayer();
                Destroy(gameObject);
            }
        }

        public void ApplyToPlayer()
        {
            _player.GetPowerupController().GainStrength(_value, _operation);
            _isEnabled = false;
        }
    }
}
