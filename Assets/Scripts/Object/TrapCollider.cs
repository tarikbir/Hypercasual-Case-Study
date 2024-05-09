using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class TrapCollider : MonoBehaviour
    {
        private IPlayerService _player;

        [Inject]
        public void Initialize(IPlayerService player)
        {
            _player = player;
        }

        public void ApplyToPlayer()
        {
            _player.GetPowerupController().GainStrength(2, OpType.Subtraction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PlayerTag))
            {
                ApplyToPlayer();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
