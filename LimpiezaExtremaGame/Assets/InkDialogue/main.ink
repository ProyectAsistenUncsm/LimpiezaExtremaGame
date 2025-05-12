// external functions
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

// quest ids (questId + "Id" for variable name)
VAR LimpiarAmericanDonutsQuestId = "LimpiarAmericanDonutsQuest"

// quest states (questId + "State" for variable name)
VAR LimpiarAmericanDonutsQuestState = "REQUIREMENTS_NOT_MET"

// ink files
INCLUDE limpiar_American_Donuts_npc.ink

// quest ids (questId + "Id" for variable name)
VAR LimpiarCineMarkQuestId = "LimpiarCineMarkQuest"

// quest states (questId + "State" for variable name)
VAR LimpiarCineMarkQuestState = "REQUIREMENTS_NOT_MET"

// ink files
INCLUDE limpiar_Cine_Mark_npc.ink

// quest ids (questId + "Id" for variable name)
VAR LimpiarFoodcourtQuestId = "LimpiarFoodcourtQuest"

// quest states (questId + "State" for variable name)
VAR LimpiarFoodcourtQuestState = "REQUIREMENTS_NOT_MET"

// ink files
INCLUDE limpiar_Foodcourt_npc.ink

// quest ids (questId + "Id" for variable name)
VAR LimpiarParqueoQuestId = "LimpiarParqueoQuest"

// quest states (questId + "State" for variable name)
VAR LimpiarParqueoQuestState = "REQUIREMENTS_NOT_MET"

// ink files
INCLUDE limpiar_Parqueo_npc.ink