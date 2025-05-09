// external functions
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

// quest ids (questId + "Id" for variable name)
VAR CollectTrashQuestId = "CollectTrashQuest"

// quest states (questId + "State" for variable name)
VAR CollectTrashQuestState = "REQUIREMENTS_NOT_MET"

=== collectTrashStart ===
{ CollectTrashQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
// not possible for this quest, but putting something here anyways
Regresa luego.
-> END

= canStart
Puedes recolectar 5 basura y dársela a mi amigo que esta a mi lado?
* [Si]
    ~ StartQuest("CollectTrashQuest")
    Excelente!
* [No]
    Está bien. Regresa si cambias de opinion.
- -> END

= inProgress
Cómo va la recolección de basura?
-> END

= canFinish
Ah, ya recolectaste la basura? Dásela a mi amigo y el te dará una recompensa!
-> END

= finished
Gracias por recolectar la basura!
-> END
