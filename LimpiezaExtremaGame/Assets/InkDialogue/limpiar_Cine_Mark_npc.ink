=== LimpiarCineMarkStart ===
{ LimpiarCineMarkQuestState :
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
Puedes recolectar la basura dentro de CineMark y traermela?
* [Si]
    ~ StartQuest("LimpiarCineMarkQuest")
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
    ~ FinishQuest("LimpiarCineMarkQuest")
    Aqui esta tu recompensa.
* [No dar]
    Esta bien. Regresa si cambias de opinion.
- -> END

= finished
Gracias por recolectar la basura!
-> END
