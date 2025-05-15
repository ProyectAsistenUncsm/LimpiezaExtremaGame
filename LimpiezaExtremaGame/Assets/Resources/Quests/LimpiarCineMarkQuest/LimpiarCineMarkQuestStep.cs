using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpiarCineMarkQuestStep : QuestStep
{
    private int trashCollected = 0;
    private int trashToComplete = 10;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onTrashCollected += TrashCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onTrashCollected -= TrashCollected;
    }

    private void TrashCollected()
    {
        if (trashCollected < trashToComplete)
        {
            trashCollected++;
            UpdateState();
        }

        if (trashCollected >= trashToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = trashCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.trashCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
