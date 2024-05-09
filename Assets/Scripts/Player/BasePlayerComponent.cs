using UnityEngine;

namespace HypercasualPrototype
{
    public class BasePlayerComponent : MonoBehaviour
    {
        protected PlayerController _player;

        protected void Awake()
        {
            _player = GetComponent<PlayerController>();
        }
    }
}
