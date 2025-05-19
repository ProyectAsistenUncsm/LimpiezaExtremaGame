using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IDataPersistence
{
    [Header("Config")]
    [SerializeField] //private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    // quest start requirements

    private void Awake()
    {
        questMap = CreateQuestMap();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }
    private void Start()
    {

        foreach (Quest quest in questMap.Values)
        {
            //Initialize and loaded quest steps
            if(quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            //broadcast the initial state of all quests on startup 
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirements(Quest quest)
    {
        // start true and prove to be false
        bool meetsRequirements = true;

        // check quest prerequisites for completition
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
            {
                meetsRequirements = false;
                break;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // loop through ALL quests
        foreach(Quest quest in questMap.Values)
        {
            // if we're now meeting the requirements, switch over to the CAN_START state
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirements(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        //move on to the next step
        quest.MoveToNextStep();

        //if there are more steps, instantiate the next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        // if there are no more steps, then we've finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.goldEvents.GoldGained(quest.info.goldReward);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quest folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        //Create the quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning(("Duplicate ID found when creating quest map: ") + questInfo.id);
            }
            //idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap; 
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if( quest == null)
        {
            Debug.LogError("ID not found in the Quest Map: " + id);
        }
        return quest;
    }

    /*private void OnApplicationQuit()
    {
        foreach(Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }*/

    public void LoadData(GameData data)
    {
        questMap = CreateQuestMap();

        foreach(QuestData questData in data.activeQuests)
        {
            if(questMap.TryGetValue(questData.id, out Quest quest))
            {
                quest.LoadQuestData(questData);
            }
            else
            {
                Debug.LogWarning("Quest with id " + questData.id + " not found in questMap during LoadData.");
            }
        }

        GameEventsManager.instance.questEvents.QuestsLoaded();
    }

    public void SaveData(ref GameData data)
    {
        data.activeQuests.Clear();
        foreach (Quest quest in questMap.Values)
        {
            data.activeQuests.Add(quest.GetQuestData());
        }
    }

    public QuestState GetQuestStateById(string id)
    {
        if(questMap != null && questMap.TryGetValue(id, out Quest quest))
        {
            return quest.state;
        }
        else
        {
            Debug.LogWarning("Quest con ID '" + id + "' no encontrada al consultar estado.");
            return QuestState.REQUIREMENTS_NOT_MET;
        }
    }
}
