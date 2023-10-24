using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStateChange
{
    public string characterName;
    [Header("changeState�� ���� �ε��� ��ȣ�� changeStateAmount��ŭ ����")]
    public List<string> changeState= new List<string>();
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
        foreach(var currentaffectedChara in affectedCharacters)
        {

        }
    }
}

[CreateAssetMenu(fileName = "New Select", menuName = "Select")]
public class Select : ScriptableObject
{
    [SerializeField] List<Choice> choices = new List<Choice>();
}
