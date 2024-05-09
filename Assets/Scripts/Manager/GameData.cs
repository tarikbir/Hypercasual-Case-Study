using System;
using System.Collections.Generic;

namespace HypercasualPrototype
{
    [Serializable]
    public struct GameData
    {
        public int CoinCount;
        public List<string> PurchasedSkillData;
        public string[] SelectedSkillData;
    }
}