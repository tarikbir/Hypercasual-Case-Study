using System.Collections.Generic;
using UniRx;

namespace HypercasualPrototype
{
    public interface IGameManagerService
    {
        void StartGame();
        void ReturnToMenu();
        void GameEndTrigger();
        void SaveGame();
        List<SkillData> PurchasedSkillData { get; set; }
        SkillData[] SelectedSkillData { get; set; }
        IntReactiveProperty CoinCount { get; set; }
    }
}
