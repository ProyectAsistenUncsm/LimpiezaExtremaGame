using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAreaBlocker : MonoBehaviour
{
    [SerializeField] private string questID;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            var quest = questManager.GetQuestStateById(questID);
            if (quest == QuestState.IN_PROGRESS || quest == QuestState.FINISHED || quest == QuestState.CAN_FINISH)
            {
                col.enabled = false;
                return;
            }
        }
        col.enabled = true;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += OnQuestStarted;
        GameEventsManager.instance.questEvents.onQuestsLoaded += OnQuestsLoaded;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= OnQuestStarted;
        GameEventsManager.instance.questEvents.onQuestsLoaded -= OnQuestsLoaded;
    }

    private void OnQuestStarted(string id)
    {
        if (id == questID)
        {
            col.enabled = false;
        }
    }

    private void OnQuestsLoaded()
    {
        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }

        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            var questState = questManager.GetQuestStateById(questID);
            if (questState == QuestState.IN_PROGRESS || questState == QuestState.FINISHED || questState == QuestState.CAN_FINISH)
            {
                col.enabled = false;
            }
        }
    }
}
