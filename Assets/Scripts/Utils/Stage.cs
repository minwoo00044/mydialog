using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Branch
{
    public string branchName;
    public List<string> dialogs = new List<string>();
    
    public Branch(string _name="default")
    {
        branchName = _name;
    }
}

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] int branchCount;
    [SerializeField] string[] branchNames;
    [SerializeField] int stageNum;
    [SerializeField] Dictionary<string, Branch> branches = new Dictionary<string, Branch>();

    public List<string> GetDialogInStage() => branches[branchNames[branchCount]].dialogs;
    public void LoadDialogs()
    {
        TextAsset data = Resources.Load("Stage" + stageNum.ToString()) as TextAsset;
        string[] lines = data.text.Split('\n');

        branches.Clear();
        for (int i = 0; i < branchNames.Length; i++)
        {
            branches.Add(branchNames[i], new Branch(branchNames[i]));
        }
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            if (parts.Length < 3) continue; // ������ �κ��� ������� ������ �ǳʶ�

            var branchKeyPart = parts[0];
            var charaKeyPart = parts[1];
            var sentenceKeyPart = parts[2];

            if (branches.TryGetValue(branchKeyPart, out Branch currentBranch))
            {
                currentBranch = branches[branchKeyPart];
                string dialogEntry = charaKeyPart + '&' + sentenceKeyPart;
                currentBranch.dialogs.Add(dialogEntry);
            }
            else
            {
                Debug.LogWarning("Branch " + branchKeyPart + " not found.");
            }
        }
    }


}