using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] int branchIndex;
    [SerializeField] string[] branchNames;
    [SerializeField] int stageNum;
    [SerializeField] Dictionary<string, Branch> branches = new Dictionary<string, Branch>();
    public Sprite image;

    public List<string> GetDialogInStage() => branches[branchNames[branchIndex]].dialogs;
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

            if (parts.Length < 4) continue; // 라인의 부분이 충분하지 않으면 건너뜀

            var branchKeyPart = parts[0];
            var charaKeyPart = parts[1];
            var imotionKeyPart = parts[2];
            var sentenceKeyPart = parts[3];

            if (branches.TryGetValue(branchKeyPart, out Branch currentBranch))
            {
                currentBranch = branches[branchKeyPart];
                string dialogEntry = charaKeyPart + '&' + sentenceKeyPart + '&' + imotionKeyPart;
                currentBranch.dialogs.Add(dialogEntry);
            }
            else
            {
                Debug.LogWarning("Branch " + branchKeyPart + " not found.");
            }
        }
    }


}
