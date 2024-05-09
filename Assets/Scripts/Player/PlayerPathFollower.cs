using PathCreation;
using System;
using UnityEngine;

namespace HypercasualPrototype
{
    public class PlayerPathFollower : BasePlayerComponent
    {
        public float Speed { get; set; } = 5;

        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction;

        private float _distanceTravelled;
        private bool _isMoving = true;

        private void Start()
        {
            if (_pathCreator != null)
            {
                _pathCreator.pathUpdated += OnPathChanged;
            }
        }

        private void Update()
        {
            if (_pathCreator != null && _isMoving)
            {
                _distanceTravelled += Speed * Time.deltaTime;
                transform.SetPositionAndRotation(_pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction), _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction));
                transform.eulerAngles += new Vector3(0, 0, 90); //This is to turn player 90 degrees upright.
                
                if (transform.position == _pathCreator.path.GetPoint(_pathCreator.path.NumPoints - 1))
                {
                    _player.WinConditionTriggered();
                }
            }
        }

        public void Stop()
        {
            _isMoving = false;
        }

        private void OnPathChanged()
        {
            _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}