using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace HypercasualPrototype.UI
{
    public class SkillButtonController : MonoBehaviour, IPointerDownHandler
    {
        private Skill _skill;
        [SerializeField] private int _skillIndex;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldown;
        [SerializeField] private Slider _slider;

        private ISkillManagerService _skillManagerService;

        [Inject]
        public void Initialize(ISkillManagerService skillManagerService)
        {
            _skillManagerService = skillManagerService;
            GetSkill();
        }

        public void ActivateSkill() //No MVVM, but time constraints..
        {
            if (_skill.CanActivateSkill())
            {
                _skill.ActivateSkill();
                _slider.value = GetSliderAmount(_skill.UseAmount);
                _cooldown.fillAmount = 1;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ActivateSkill();
        }

        private void GetSkill()
        {
            _skill = _skillManagerService.CreateAndGetSkill(_skillIndex);
            _icon.sprite = _skill.Icon;
            _slider.value = GetSliderAmount(_skill.UseAmount);
            _skill.CooldownTimer.Subscribe(timer =>
            {
                _cooldown.fillAmount = timer / _skill.Cooldown;
            });
        }

        private float GetSliderAmount(int useAmount) => Mathf.Clamp(useAmount, 0, 4) * 0.25f;
    }
}
