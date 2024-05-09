using UnityEngine;

namespace HypercasualPrototype.Object
{
    public abstract class PickUp : MonoBehaviour
    {
        public abstract void OnPickup(PlayerController playerController);
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PlayerTag))
            {
                //Removed pattern matching because player wasn't the one colliding 
                PlayerController playerController = other.GetComponentInParent<PlayerController>();
                OnPickup(playerController);
                Destroy(gameObject);
            }
        }
    }
}