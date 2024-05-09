using UnityEngine;

namespace HypercasualPrototype
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private Transform _bladeObj;
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _rotationSpeed = 200;
        [SerializeField] private float _moveSpeed = 2f;

        private int _currentIndex;

        private void Awake()
        {
            _bladeObj.position = _points[0].position;
            _currentIndex = 0;
        }

        private void MoveToPoint(float deltaTime)
        {
            var nextIndex = GetNextIndex();
            _bladeObj.position = Vector3.Lerp(_bladeObj.position, _points[nextIndex].position, _moveSpeed * deltaTime);

            if (Vector3.Distance(_bladeObj.position, _points[nextIndex].position) < 0.2f)
            {
                _currentIndex = nextIndex;
            }
        }

        private void Update()
        {
            _bladeObj.Rotate(new Vector3(0, Time.deltaTime * _rotationSpeed, 0));

            MoveToPoint(Time.deltaTime);
        }

        private int GetNextIndex()
        {
            return (_currentIndex + 1) % 2;
        }
    }
}
