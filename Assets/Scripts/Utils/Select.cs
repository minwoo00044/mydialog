using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStateChange
{
    public NPCData targetChara;
    [Header("changeState�� ���� �ε��� ��ȣ�� changeStateAmount��ŭ ����")]
    public List<NpcState> changeState= new List<NpcState>();
    public List<int> changeStateAmount = new List<int>();
}
[System.Serializable]
public class Choice
{
    public string choiceTxt;
    public string nextBranchName;
    public List<CharacterStateChange> affectedCharacters = new List<CharacterStateChange>();

    public void Execute()
    {
        for (int i = 0; i < affectedCharacters.Count; i++)
        {
            CharacterStateChange currnet = affectedCharacters[i];
            StateManager.instance.SetNpcState(currnet.targetChara.NPCName, currnet.changeState, currnet.changeStateAmount);

        }
        if (!string.IsNullOrEmpty(nextBranchName))
        {
            StageManager.instance.NextStage(nextBranchName);

        }
        SelectManager.instance.ToggleSelectBtn();
    }
}

[CreateAssetMenu(fileName = "New Select", menuName = "Select")]
public class Select : ScriptableObject
{
    public Stage attachedStage;
    public string attachedBranchName;
    [SerializeField] List<Choice> _choices = new List<Choice>();
    public List<Choice> choices => _choices;
}
