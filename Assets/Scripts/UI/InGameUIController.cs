using UnityEngine;

namespace HypercasualPrototype.UI
{
    public class InGameUIController : MonoBehaviour, IInGameUIController
    {
        [SerializeField] private GameObject _victorySplash;
        [SerializeField] private GameObject _defeatSplash;

        public void ShowVictorySplash()
        {
            _victorySplash.SetActive(true);
        }

        public void ShowDefeatSplash()
        {
            _defeatSplash.SetActive(true);
        }
    }
}
