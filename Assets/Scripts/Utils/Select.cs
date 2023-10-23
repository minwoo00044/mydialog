using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string choiceTxt;
    public string nextBranchName;
}
[CreateAssetMenu(fileName = "New Select", menuName = "Select")]
public class Select : ScriptableObject
{
    [SerializeField] List<Choice> choices = new List<Choice>();
}
