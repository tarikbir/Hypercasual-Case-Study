using UnityEngine;

namespace HypercasualPrototype
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        public const string COIN_GRAB = "coin-grab";

        [SerializeField] private AudioSource _source;

        [SerializeField] private AudioClip _coinGrab;

        public void Play(string audioName)
        {
            switch(audioName)
            {
                case COIN_GRAB:
                    _source.pitch = Random.Range(0.9f, 1.3f);
                    _source.PlayOneShot(_coinGrab);
                    break;
                default:
                    break;
            }
        }
    }
}
