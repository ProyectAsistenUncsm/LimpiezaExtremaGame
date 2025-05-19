=== LimpiarParqueoStart ===
{ LimpiarParqueoQuestState :
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
Puedes recolectar la basura dentro del parqueo y traermela?
* [Si]
    ~ StartQuest("LimpiarParqueoQuest")
    Excelente!
* [No]
    Esta bien. Regresa si cambias de opinion.
- -> END

= inProgress
Como va la recoleccion de basura?
-> END

= canFinish
Ah, ya recolectaste la basura? Damela y te dare una recompensa!
* [Dar basura]
    ~ FinishQuest("LimpiarParqueoQuest")
    Aqui esta tu recompensa.
* [No dar]
    Esta bien. Regresa si cambias de opinion.
- -> END

= finished
Gracias por recolectar la basura!
-> END
