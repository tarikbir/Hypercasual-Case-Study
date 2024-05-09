using System.Collections;
using UniRx;
using UnityEngine;

namespace HypercasualPrototype
{
    public class PlayerPowerupController : BasePlayerComponent
    {
        public IntReactiveProperty Strength { get; private set; } = new IntReactiveProperty(1);
        public bool SpedUp { get; private set; } = false;

        public void GainStrength(int amount, OpType operation)
        {
            switch (operation)
            {
                case OpType.Addition:
                    Strength.Value = Strength.Value + amount;
                    break;
                case OpType.Subtraction:
                    Strength.Value = Strength.Value - amount;
                    break;
                case OpType.Multiplication:
                    Strength.Value = Strength.Value * amount;
                    break;
                case OpType.Division:
                    Strength.Value = amount != 0 ? Strength.Value / amount : 0;
                    break;
                default:
                    break;
            }
        }

        public void GainSpeedPowerUp(float amount, float time)
        {
            if (!SpedUp)
            {
                StartCoroutine(SpeedPowerUp(amount, time));
            }
        }

        private IEnumerator SpeedPowerUp(float amount, float time)
        {
            _player.SetSpeedTimes(amount);
            SpedUp = true;
            yield return new WaitForSeconds(time);
            _player.ResetSpeed();
            SpedUp = false;
        }
    }
}