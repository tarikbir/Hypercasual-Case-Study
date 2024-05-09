using HypercasualPrototype.Object;
using UnityEngine;
using Zenject;

namespace HypercasualPrototype
{
    public class Coin : PickUp
    {
        [SerializeField] private float _rotationSpeed = 2f;

        private IGameManagerService _gameManager;

        [Inject]
        public void Initialize(IGameManagerService gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            transform.eulerAngles = new Vector3(0, Random.Range(-180, 180), 90);
        }

        private void Update()
        {
            transform.Rotate(Vector3.right, _rotationSpeed * Time.deltaTime);
        }

        public override void OnPickup(PlayerController playerController)
        {
            _gameManager.CoinCount.Value += 1;
        }
    }
}