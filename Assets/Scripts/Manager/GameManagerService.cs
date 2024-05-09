using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace HypercasualPrototype
{
    public class GameManagerService : MonoBehaviour, IGameManagerService
    {
        [SerializeField] private SkillData[] _defaultSelectedSkills;
        public List<SkillData> PurchasedSkillData { get; set; }
        public SkillData[] SelectedSkillData { get; set; }
        public IntReactiveProperty CoinCount { get; set; }

        private IDataService _dataService;

        [Inject]
        public void Initialize(IDataService dataService)
        {
            _dataService = dataService;

            if (!LoadGame())
            {
                SelectedSkillData = new SkillData[2];
                SelectedSkillData[0] = _defaultSelectedSkills[0];
                SelectedSkillData[1] = _defaultSelectedSkills[1];

                PurchasedSkillData = new List<SkillData>
                {
                    _defaultSelectedSkills[0],
                    _defaultSelectedSkills[1]
                };

                CoinCount = new IntReactiveProperty(0);
            }
            else
            {
                Debug.Log("Game loaded.");
            }

            // Normally this would be called after loading ProjectContext.
            FindObjectOfType<SceneContext>().PostResolve += LoadMenuAfterSceneInitialization;
        }

        public void GameEndTrigger()
        {
            StartCoroutine(EndGameAfter3Seconds());
        }

        public void StartGame()
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void SaveGame()
        {
            if (_dataService.SaveData<GameData>("/gameSave.json", new GameData()
            {
                CoinCount = CoinCount.Value,
                PurchasedSkillData = PurchasedSkillData.Select(sd => sd.name).ToList(),
                SelectedSkillData = SelectedSkillData.Select(sd => sd.name).ToArray()
            }))
            {
                Debug.Log("Game saved.");
            }
        }

        public bool LoadGame()
        {
            bool success;
            GameData loadedData;
            (success, loadedData) = _dataService.LoadData<GameData>("/gameSave.json");
            if (!success) return false;
            PurchasedSkillData = new List<SkillData>();
            foreach (string skillName in loadedData.PurchasedSkillData)
            {
                SkillData skillData = Resources.Load<SkillData>($"Skills/{skillName}");
                PurchasedSkillData.Add(skillData);
            }
            SelectedSkillData = new SkillData[2];
            SkillData selectedSkill0 = Resources.Load<SkillData>($"Skills/{loadedData.SelectedSkillData[0]}");
            SelectedSkillData[0] = selectedSkill0;
            SkillData selectedSkill1 = Resources.Load<SkillData>($"Skills/{loadedData.SelectedSkillData[1]}");
            SelectedSkillData[1] = selectedSkill1;
            CoinCount = new IntReactiveProperty(loadedData.CoinCount);
            return true;
        }

        private void LoadMenuAfterSceneInitialization()
        {
            FindObjectOfType<SceneContext>().PostResolve -= LoadMenuAfterSceneInitialization;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        private IEnumerator EndGameAfter3Seconds()
        {
            yield return new WaitForSeconds(3);
            SaveGame();
            ReturnToMenu();
        }
    }
}